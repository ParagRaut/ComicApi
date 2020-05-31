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
        public Task<string> GetRandomComicUri()
        {
            this._logger.LogInformation("Fetching random comic uri...");
            return this.ComicUrlService.GetRandomComic();
        }

        [HttpGet]
        [Route("[controller]/dilbert")]
        public Task<string> GetDilbertComicUri()
        {
            this._logger.LogInformation("Fetching Dilbert comic uri...");
            return this.ComicUrlService.GetDilbertComic();
        }

        [HttpGet]
        [Route("[controller]/garfield")]
        public Task<string> GetGarfieldComicUri()
        {
            this._logger.LogInformation("Fetching Garfield comic uri...");
            return this.ComicUrlService.GetGarfieldComic();
        }

        [HttpGet]
        [Route("[controller]/xkcd")]
        public Task<string> GetXkcdComicUri()
        {
            this._logger.LogInformation("Fetching XKCD comic uri...");
            return this.ComicUrlService.GetXkcdComic();
        }
    }
}