using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicServices.ComicSources.DilbertComics
{
    public interface IDilbertComics
    {
        FileResult GetDilbertComic();
    }
}