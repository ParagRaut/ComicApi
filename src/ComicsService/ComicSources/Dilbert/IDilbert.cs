using System.Threading.Tasks;

namespace RandomComicApi.ComicsService.ComicSources.Dilbert
{
    public interface IDilbert
    {
        Task<string> GetDilbertComicUri();
    }
}