using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Handlers
{
    public static class ApiHandlers
    {
        public static async Task<IResult> FallbackHandler(AppDbContext dbContext, HttpContext ctx)
        {
            var path = ctx.Request.Path.ToUriComponent().Trim('/');

            var match = await dbContext.Urls.FirstOrDefaultAsync(x => x.ShortUrl.Trim() == path.Trim());

            return match == null ? Results.BadRequest(Constants.InvalidUrlProvided) : Results.Redirect(match.LongUrl);
        }
    }
}
