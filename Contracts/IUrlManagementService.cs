namespace UrlShortener.Contracts
{
    public interface IUrlManagementService
    {
        string ShortenUrl(string url);
    }
}
