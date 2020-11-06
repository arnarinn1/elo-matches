namespace EloMatches.Query.Paging
{
    public class PagingResult<TProjection>
    {
        public int TotalCount { get; set; }
        public IPagingQuery Query { get; set; }
        public TProjection[] Projections { get; set; }

        public PagingResult(int totalCount, IPagingQuery query, TProjection[] projections)
        {
            TotalCount = totalCount;
            Query = query;
            Projections = projections;
        }
    }
}