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

        IComicService ComicService { get; set; }

        [HttpGet]
        public FileResult Get()
        {
            return this.ComicService.GetRandomComic();   
        }      

    }
}
