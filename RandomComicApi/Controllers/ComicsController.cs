using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomComicApi.ComicsService;

namespace RandomComicApi.Controllers
{
    [ApiController]    
    public class ComicsController : ControllerBase
    {
        public ComicsController(
            IComicUrlService comicUrlService,
            ILogger<ComicsController> logger)
        {
            this.ComicUrlService = comicUrlService;
            this._logger = logger;
        }

        private IComicUrlService ComicUrlService { get; }

        private readonly ILogger _logger;
        
        [HttpGet]
        [Route("[controller]/random")]
        public async Task<IActionResult> GetRandomComicUri()
        {
            this._logger.LogInformation("Fetching random comic uri...");

            return Ok(await this.ComicUrlService.GetRandomComic());
        }

        [HttpGet]
        [Route("[controller]/dilbert")]
        public async Task<IActionResult> GetDilbertComicUri()
        {
            this._logger.LogInformation("Fetching Dilbert comic uri...");

            return Ok(await this.ComicUrlService.GetDilbertComic());
        }

        [HttpGet]
        [Route("[controller]/garfield")]
        public async Task<IActionResult> GetGarfieldComicUri()
        {
            this._logger.LogInformation("Fetching Garfield comic uri...");

            return Ok(await this.ComicUrlService.GetGarfieldComic());
        }

        [HttpGet]
        [Route("[controller]/xkcd")]
        public async Task<IActionResult> GetXkcdComicUri()
        {
            this._logger.LogInformation("Fetching XKCD comic uri...");

            return Ok(await this.ComicUrlService.GetXkcdComic());
        }

        [HttpGet]
        [Route("[controller]/calvinandhobbes")]
        public async Task<IActionResult> GetCalvinAndHobbesComicUri()
        {
            this._logger.LogInformation("Fetching Calvin and Hobbes comic uri...");

            return Ok(await this.ComicUrlService.GetCalvinAndHobbesComic());
        }
    }
}