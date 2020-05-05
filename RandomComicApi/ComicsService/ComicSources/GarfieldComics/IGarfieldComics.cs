using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicsService.ComicSources.GarfieldComics
{
    public interface IGarfieldComics
    {
        FileResult GetGarfieldComic();
        string GetGarfieldComicUri();
    }
}