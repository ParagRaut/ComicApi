using System.Threading.Tasks;
using RandomComicApi.ComicsService.ComicSources.GarfieldComics.GarfieldService;

namespace RandomComicApi.ComicsService.ComicSources.GarfieldComics
{
    public class GarfieldComics : IGarfieldComics
    {
        public Task<string> GetGarfieldComicUri()
        {
            return GarfieldServiceApi.GetGarfieldComicsUrl();        
        }        
    }
}