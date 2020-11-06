using System.Reflection;
using EloMatches.Domain.AggregateModels.MatchesAggregate;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.Domain.AggregateModels.MatchesAggregate
{
    [TestFixture, Category("Unit")]
    public class MatchesAfterReferenceDateReflectionTests
    {
        [Test]
        public void EnforceThatThereOnlyExistsOnePrivateConstructorOnMatches()
        {
            var constructors = typeof(MatchesAfterReferenceDate).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            if (constructors.Length == 1)
                Assert.Pass();
            else
                Assert.Fail("MatchesAfterReferenceDate should only have one private constructor that the repository uses in order to create the instance of the aggregate.");
        }

        [Test]
        public void EnsureThatRegisteredMatchPropertyExists()
        {
            var property = typeof(MatchesAfterReferenceDate).GetProperty("RegisteredMatch", BindingFlags.Instance | BindingFlags.NonPublic);

            if (property != null)
                Assert.Pass();
            else
                Assert.Fail("Private property named = 'RegisteredMatch' not found on type = 'MatchesAfterReferenceDate'");
        }
    }
}