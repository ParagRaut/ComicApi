using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicsService.ComicSources.GarfieldComics
{
    public interface IGarfieldComics
    {
        Task<FileResult> GetGarfieldComic();
        Task<string> GetGarfieldComicUri();
    }
}