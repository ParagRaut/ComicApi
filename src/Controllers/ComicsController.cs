using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        [Produces("application/json")]
        [ProducesResponseType(typeof(ComicModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [Route("[controller]/random")]
        public async Task<IActionResult> GetRandomComicUri()
        {
            try
            {
                this._logger.LogInformation("Fetching random comic uri...");

                return Ok(new { comicUrl = await this.ComicUrlService.GetRandomComic() });
            }
            catch (Exception exception)
            {
                this._logger.LogError("Error while processing request.", exception);

                return StatusCode(500, new { errorMessage = "Something went wrong" });
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ComicModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [Route("[controller]/dilbert")]
        public async Task<IActionResult> GetDilbertComicUri()
        {
            try
            {
                this._logger.LogInformation("Fetching Dilbert comic uri...");

                return Ok(new { comicUrl = await this.ComicUrlService.GetDilbertComic() });
            }
            catch (Exception exception)
            {
                this._logger.LogError("Error while processing request.", exception);

                return StatusCode(500, new { errorMessage = "Something went wrong" });
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ComicModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [Route("[controller]/garfield")]
        public async Task<IActionResult> GetGarfieldComicUri()
        {
            try
            {
                this._logger.LogInformation("Fetching Garfield comic uri...");

                return Ok(new { comicUrl = await this.ComicUrlService.GetGarfieldComic() });
            }
            catch (Exception exception)
            {
                this._logger.LogError("Error while processing request.", exception);

                return StatusCode(500, new { errorMessage = "Something went wrong" });
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ComicModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [Route("[controller]/xkcd")]
        public async Task<IActionResult> GetXkcdComicUri()
        {
            try
            {
                this._logger.LogInformation("Fetching XKCD comic uri...");

                return Ok(new { comicUrl = await this.ComicUrlService.GetXkcdComic() });
            }
            catch (Exception exception)
            {
                this._logger.LogError("Error while processing request.", exception);

                return StatusCode(500, new { errorMessage = "Something went wrong" });
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ComicModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [Route("[controller]/calvinandhobbes")]
        public async Task<IActionResult> GetCalvinAndHobbesComicUri()
        {
            try
            {
                this._logger.LogInformation("Fetching Calvin and Hobbes comic uri...");

                return Ok(new { comicUrl = await this.ComicUrlService.GetCalvinAndHobbesComic() });
            }
            catch (Exception exception)
            {
                this._logger.LogError("Error while processing request.", exception);

                return StatusCode(500, new { errorMessage = "Something went wrong" });
            }
        }
    }
}