using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicServices.ComicSources.GarfieldComics
{
    public interface IGetGarfieldComics
    {
        FileResult GetGarfieldComic();
    }
}