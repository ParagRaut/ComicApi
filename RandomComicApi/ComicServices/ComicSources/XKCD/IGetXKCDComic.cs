using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicServices.ComicSources.XKCD
{
    public interface IGetXKCDComic
    {
        FileResult GetXkcdComic();
    }
}