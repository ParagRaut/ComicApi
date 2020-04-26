using Microsoft.AspNetCore.Mvc;
using RandomComicApi.ComicServices;

namespace XkcdComicsApi.Controllers
{
    
    [ApiController]
    [Route("[controller]/GetComics")]
    public class ComicsController : ControllerBase
    {
        public ComicsController(IComicService comicService)
        {
            this.ComicService = comicService;
        }

        IComicService ComicService { get; set; }

        [HttpGet]
        public FileResult Get()
        {
            return this.ComicService.GetRandomComic();   
        }      

    }
}
