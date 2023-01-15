using Microsoft.AspNetCore.Mvc;
using UrlShortener.Contracts;
using UrlShortener.Dtos;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlManagementService _urlManagementService;

        public UrlController(IUrlManagementService urlManagementService)
        {
            _urlManagementService = urlManagementService;
        }


        [HttpPost]
        public IActionResult ShortUrl(UrlDto urlDto)
        {
            if(!Uri.TryCreate(urlDto.Url, UriKind.Absolute, out var uri))
            {
                return BadRequest(Constants.InvalidUrlProvided);
            }

            var response = _urlManagementService.ShortenUrl(urlDto.Url);

            return Ok(new UrlShortResponseDto
            {
                Url = response
            });
        } 
    }
}
