using System.Threading.Tasks;

namespace RandomComicApi.ComicsService.ComicSources.Garfield
{
    public interface IGarfield
    {
        Task<string> GetGarfieldComicUri();
    }
}