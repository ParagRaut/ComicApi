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
        public FileResult GetRandomComicImage()
        {
            this._logger.LogInformation("Fetching random comic image...");
            return this.ComicService.GetRandomComic();
        }

        [HttpGet]
        [Route("[controller]/random")]
        public string GetRandomComicUri()
        {
            this._logger.LogInformation("Fetching random comic uri...");
            return this.ComicUrlService.GetRandomComic();
        }

        [HttpGet]
        [Route("[controller]/dilbert")]
        public string GetDilbertComicUri()
        {
            this._logger.LogInformation("Fetching Dilbert comic uri...");
            return this.ComicUrlService.GetDilbertComic();
        }

        [HttpGet]
        [Route("[controller]/garfield")]
        public string GetGarfieldComicUri()
        {
            this._logger.LogInformation("Fetching Garfield comic uri...");
            return this.ComicUrlService.GetGarfieldComic();
        }

        [HttpGet]
        [Route("[controller]/xkcd")]
        public string GetXkcdComicUri()
        {
            this._logger.LogInformation("Fetching XKCD comic uri...");
            return this.ComicUrlService.GetXkcdComic();
        }
    }
}