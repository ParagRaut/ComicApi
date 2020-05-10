using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomComicApi.ComicsService;

namespace RandomComicApi.Controllers
{
    [ApiController]    
    public class ComicsController : ControllerBase
    {
        public ComicsController(IComicService comicService,
            IComicUrlService comicUrlService,
            ILogger<ComicsController> logger)
        {
            this.ComicService = comicService;
            this.ComicUrlService = comicUrlService;
            this._logger = logger;
        }

        private IComicService ComicService { get; }
        private IComicUrlService ComicUrlService { get; }

        private readonly ILogger _logger;

        [HttpGet]
        [Route("[controller]/randomstrip")]
        public Task<FileResult> GetRandomComicImage()
        {
            this._logger.LogInformation("Fetching random comic image...");
            return this.ComicService.GetRandomComic();
        }

        [HttpGet]
        [Route("[controller]/dilbertstrip")]
        public Task<FileResult> GetDilbertComicImage()
        {
            this._logger.LogInformation("Fetching Dilbert comic image...");
            return this.ComicService.GetDilbertComic();
        }

        [HttpGet]
        [Route("[controller]/garfieldstrip")]
        public Task<FileResult> GetGarfieldComicImage()
        {
            this._logger.LogInformation("Fetching Garfield comic image...");
            return this.ComicService.GetGarfieldComic();
        }

        [HttpGet]
        [Route("[controller]/xkcdstrip")]
        public Task<FileResult> GetXkcdComicImage()
        {
            this._logger.LogInformation("Fetching XKCD comic image...");
            return this.ComicService.GetXkcdComic();
        }
        
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