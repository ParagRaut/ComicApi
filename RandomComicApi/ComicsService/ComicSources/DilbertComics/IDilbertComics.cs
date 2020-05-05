using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicsService.ComicSources.DilbertComics
{
    public interface IDilbertComics
    {
        FileResult GetDilbertComic();
        string GetDilbertComicUri();
    }
}