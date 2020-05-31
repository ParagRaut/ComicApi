using System.Threading.Tasks;

namespace RandomComicApi.ComicsService.ComicSources.DilbertComics
{
    public interface IDilbertComics
    {
        Task<string> GetDilbertComicUri();
    }
}