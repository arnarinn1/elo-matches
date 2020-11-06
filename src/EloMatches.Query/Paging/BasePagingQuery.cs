namespace EloMatches.Query.Paging
{
    public abstract class BasePagingQuery : IPagingQuery
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string OrderByColumn { get; set; }
        public OrderByDirection OrderByDirection { get; set; }

        protected BasePagingQuery(int pageSize, int pageIndex, string orderByColumn, OrderByDirection orderByDirection)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            OrderByColumn = orderByColumn;
            OrderByDirection = orderByDirection;
        }
    }
}