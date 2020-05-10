using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicsService.ComicSources.DilbertComics
{
    public interface IDilbertComics
    {
        Task<FileResult> GetDilbertComic();
        Task<string> GetDilbertComicUri();
    }
}