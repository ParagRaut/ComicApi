using System.Threading.Tasks;
using RandomComicApi.ComicsService.ComicSources.DilbertComics.DilbertService;

namespace RandomComicApi.ComicsService.ComicSources.DilbertComics
{
    public class DilbertComics : IDilbertComics
    {
        public async Task<string> GetDilbertComicUri()
        {
            var dilbertServiceApi = new DilbertServiceApi();

            string comicStripUri = await dilbertServiceApi.GetDilbertComicsUrl();

            return comicStripUri;
        }
    }
}