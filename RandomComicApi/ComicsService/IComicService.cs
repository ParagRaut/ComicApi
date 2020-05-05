using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicsService
{
    public interface IComicService
    {
        FileResult GetRandomComic();
    }
}