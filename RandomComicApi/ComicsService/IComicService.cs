using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicsService
{
    public interface IComicService
    {
        Task<FileResult> GetRandomComic();
        Task<FileResult> GetDilbertComic();
        Task<FileResult> GetGarfieldComic();
        Task<FileResult> GetXkcdComic();
    }
}