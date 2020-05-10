using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicsService.ComicSources.XKCD
{
    public interface IXkcdComic
    {
        Task<FileResult> GetXkcdComic();
        Task<string> GetXkcdComicUri();
    }
}