using System.Threading.Tasks;

namespace RandomComicApi.ComicsService.ComicSources.Xkcd
{
    public interface IXkcdComic
    {
        Task<string> GetXkcdComicUri();
    }
}