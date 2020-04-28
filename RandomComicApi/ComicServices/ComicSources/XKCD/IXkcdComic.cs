using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicServices.ComicSources.XKCD
{
    public interface IXkcdComic
    {
        FileResult GetXkcdComic();
    }
}