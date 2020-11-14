using System;
using System.Threading.Tasks;
using EloMatches.Api.Application.Bus.EndpointSenders;
using EloMatches.Api.Application.Bus.EndpointSenders.Behaviors;
using EloMatches.Tests.Unit.VerifyExtensions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;


namespace EloMatches.Tests.Unit.Api.Application.Bus.Behaviors
{
    [TestFixture, Category("Unit")]
    public class RetryBehaviorForEndpointSenderTests
    {
        private Mock<IEndpointSender<TestCommand>> _sender;
        private Mock<ILogger<RetryBehaviorForEndpointSender<TestCommand>>> _logger;
        private RetryBehaviorForEndpointSender<TestCommand> _retryBehavior;

        [SetUp]
        public void SetUp()
        {
            _sender = new Mock<IEndpointSender<TestCommand>>();
            _logger = new Mock<ILogger<RetryBehaviorForEndpointSender<TestCommand>>>();
            
            _retryBehavior = new RetryBehaviorForEndpointSender<TestCommand>(_sender.Object, _logger.Object);
        }

        [Test]
        public async Task Send_ShouldNeverRetry_WhenNoExceptionIsThrown()
        {
            _sender.Setup(x => x.Send(It.IsAny<TestCommand>())).Returns(Task.CompletedTask);

            await _retryBehavior.Send(new TestCommand());

            _sender.Verify(x => x.Send(It.IsAny<TestCommand>()), Times.Once);
            _logger.VerifyLoggerWasNotCalled();
        }

        [Test]
        public async Task Send_ShouldRetryOnce_WhenExceptionOccursOnce()
        {
            _sender.SetupSequence(x => x.Send(It.IsAny<TestCommand>())).Throws<Exception>().Returns(Task.CompletedTask);
            
            await _retryBehavior.Send(new TestCommand());

            _sender.Verify(x => x.Send(It.IsAny<TestCommand>()), Times.Exactly(2));
            _logger.VerifyLoggerWasNotCalled();
        }

        [Test]
        public async Task Send_ShouldRetryTwice_WhenExceptionOccursTwice()
        {
            _sender.SetupSequence(x => x.Send(It.IsAny<TestCommand>())).Throws<Exception>().Throws<Exception>().Returns(Task.CompletedTask);

            await _retryBehavior.Send(new TestCommand());

            _sender.Verify(x => x.Send(It.IsAny<TestCommand>()), Times.Exactly(3));
            _logger.VerifyLoggerWasNotCalled();
        }

        [Test]
        public async Task Send_ShouldRetryThreeTimes_WhenExceptionOccursThreeTimes()
        {
            _sender.SetupSequence(x => x.Send(It.IsAny<TestCommand>())).Throws<Exception>().Throws<Exception>().Throws(new Exception("Bad error"));

            await _retryBehavior.Send(new TestCommand());

            _sender.Verify(x => x.Send(It.IsAny<TestCommand>()), Times.Exactly(3));
            _logger.VerifyErrorWasCalled("Error occurred while trying to send 'TestCommand' through bus");
        }
    }

    public class TestCommand { }
}