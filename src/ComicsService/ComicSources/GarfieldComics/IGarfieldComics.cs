using System.Threading.Tasks;

namespace RandomComicApi.ComicsService.ComicSources.GarfieldComics
{
    public interface IGarfieldComics
    {
        Task<string> GetGarfieldComicUri();
    }
}