using System.Reflection;
using EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.Domain.PlayerLeaderBoardAggregate
{
    [TestFixture, Category("Unit")]
    public class PlayerLeaderBoardReflectionTests
    {
        [Test]
        public void EnforceThatThereOnlyExistsOnePrivateConstructorOnMatches()
        {
            var constructors = typeof(PlayerLeaderBoard).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            if (constructors.Length == 1)
                Assert.Pass();
            else
                Assert.Fail("PlayerLeaderBoard should only have one private constructor that the repository uses in order to create the instance of the aggregate.");
        }

        [Test]
        public void EnsureThatPlayersPropertyExists()
        {
            var property = typeof(PlayerLeaderBoard).GetProperty("Players", BindingFlags.Instance | BindingFlags.NonPublic);

            if (property != null)
                Assert.Pass();
            else
                Assert.Fail("Private property named = 'Players' not found on type = 'PlayerLeaderBoard'");
        }
    }
}