using UrlShortener.Contracts;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class UrlManagementService : IUrlManagementService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _ctx;

        public UrlManagementService(AppDbContext context, IHttpContextAccessor ctx)
        {
            _context = context;
            _ctx = ctx;
        }

        public string ShortenUrl(string url)
        {
            var random = new Random();

            var randomStr = new string(Enumerable.Repeat(Constants.UrlBuilderChars, 8).Select(x => x[random.Next(x.Length)]).ToArray());

            var sUrl = new UrlManagement
            {
                LongUrl = url,
                ShortUrl = randomStr
            };

            _context.Urls.Add(sUrl);

            _context.SaveChanges();

            string result = BuildUrl(sUrl);

            return result;
        }

        private string BuildUrl(UrlManagement sUrl)
        {
            return $"{_ctx.HttpContext?.Request.Scheme}://{_ctx.HttpContext?.Request.Host}/{sUrl.ShortUrl}";
        }
    }
}
