using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomComicApi.ComicServices;

namespace RandomComicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComicsController : ControllerBase
    {
        public ComicsController(IComicService comicService, ILogger<ComicsController> logger)
        {
            this.ComicService = comicService;
            this._logger = logger;
        }

        private IComicService ComicService { get; }
        private readonly ILogger _logger;

        [HttpGet]
        public FileResult Get()
        {
            this._logger.LogInformation("Fetching random comic...");
            return this.ComicService.GetRandomComic();
        }
    }
}