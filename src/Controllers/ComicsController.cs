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
            _comicUrlService = comicUrlService;
            _logger = logger;
        }

        private readonly IComicUrlService _comicUrlService;
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
                _logger.LogInformation("Fetching random comic uri...");

                return Ok(new ComicModel { ComicUrl = await _comicUrlService.GetRandomComic() });
            }
            catch (Exception exception)
            {
                _logger.LogError("Error while processing request.", exception);

                return StatusCode(500, new ErrorModel { ErrorMessage = "Something went wrong" });
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
                _logger.LogInformation("Fetching Dilbert comic uri...");

                return Ok(new ComicModel { ComicUrl = await _comicUrlService.GetDilbertComic() });
            }
            catch (Exception exception)
            {
                _logger.LogError("Error while processing request.", exception);

                return StatusCode(500, new ErrorModel { ErrorMessage = "Something went wrong" });
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
                _logger.LogInformation("Fetching Garfield comic uri...");

                return Ok(new ComicModel { ComicUrl = await _comicUrlService.GetGarfieldComic() });
            }
            catch (Exception exception)
            {
                _logger.LogError("Error while processing request.", exception);

                return StatusCode(500, new ErrorModel { ErrorMessage = "Something went wrong" });
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
                _logger.LogInformation("Fetching XKCD comic uri...");

                return Ok(new ComicModel { ComicUrl = await _comicUrlService.GetXkcdComic() });
            }
            catch (Exception exception)
            {
                _logger.LogError("Error while processing request.", exception);

                return StatusCode(500, new ErrorModel { ErrorMessage = "Something went wrong" });
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
                _logger.LogInformation("Fetching Calvin and Hobbes comic uri...");

                return Ok(new ComicModel { ComicUrl = await _comicUrlService.GetCalvinAndHobbesComic() });
            }
            catch (Exception exception)
            {
                _logger.LogError("Error while processing request.", exception);

                return StatusCode(500, new ErrorModel { ErrorMessage = "Something went wrong" });
            }
        }
    }
}