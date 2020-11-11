using System.Linq;
using EloMatches.Domain.SeedWork;
using EloMatches.Query.Paging;
using EloMatches.Query.Projections.DomainEventOccurrences;
using FluentAssertions;

namespace EloMatches.Tests.Integration.Features
{
    internal static class DomainEventOccurrenceAssertExtensionMethods
    {
        internal static void VerifyDomainEventCount(this PagingResult<DomainEventOccurrenceProjection> pagingResult, int expectedNumberOfEvents)
        {
            pagingResult.TotalCount.Should().Be(expectedNumberOfEvents);
        }

        internal static void VerifyThatSingleDomainEventOccurred<T>(this PagingResult<DomainEventOccurrenceProjection> pagingResult) where T : IDomainEvent
        {
            var domainEvent = pagingResult.Projections.SingleOrDefault(x => x.TypeName == typeof(T).Name);

            domainEvent.Should().NotBeNull();
        }
    }
}