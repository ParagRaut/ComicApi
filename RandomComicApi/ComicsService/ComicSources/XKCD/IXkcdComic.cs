using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicsService.ComicSources.XKCD
{
    public interface IXkcdComic
    {
        FileResult GetXkcdComic();
        string GetXkcdComicUri();
    }
}