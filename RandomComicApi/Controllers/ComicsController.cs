using Microsoft.AspNetCore.Mvc;
using RandomComicApi.ComicServices;

namespace RandomComicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComicsController : ControllerBase
    {
        public ComicsController(IComicService comicService)
        {
            this.ComicService = comicService;
        }

        private IComicService ComicService { get; }

        [HttpGet]
        public FileResult Get()
        {
            return this.ComicService.GetRandomComic();
        }
    }
}