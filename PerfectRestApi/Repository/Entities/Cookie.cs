namespace PerfectRestApi.Repository.Entities;

public class Cookie
{
    public Guid CookieId { get; set; }
    public required string Name { get; set; }
    public double Sugar { get; set; }
}
