namespace EloMatches.Query.Paging
{
    public interface IPagingQuery
    {
        int PageSize { get; set; }
        int PageIndex { get; set; }
        string OrderByColumn { get; set; }
        OrderByDirection OrderByDirection { get; set; }
    }
}