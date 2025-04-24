namespace SportZoneServer.Core.Pages;

public class Paginated<T>
    where T : class
{
    public IEnumerable<T>? Items { get; set; }

    public int TotalCount { get; set; }
}
