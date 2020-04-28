using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicServices
{
    public interface IComicService
    {
        FileResult GetRandomComic();
    }
}