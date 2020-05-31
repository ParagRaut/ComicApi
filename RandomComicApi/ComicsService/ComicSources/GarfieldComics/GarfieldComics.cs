using RandomComicApi.ComicsService.ComicSources.GarfieldComics.GarfieldService;

namespace RandomComicApi.ComicsService.ComicSources.GarfieldComics
{
    public class GarfieldComics : IGarfieldComics
    {
        public string GetGarfieldComicUri()
        {
            var garfieldServiceApi = new GarfieldServiceApi();
            return garfieldServiceApi.GetGarfieldComicsUrl();        
        }
    }
}