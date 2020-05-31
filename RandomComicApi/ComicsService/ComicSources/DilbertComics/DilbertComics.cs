using System.Threading.Tasks;
using ComicsAppWasm.ComicsService.ComicSources.DilbertComics;
using RandomComicApi.ComicsService.ComicSources.DilbertComics.DilbertService;

namespace RandomComicApi.ComicsService.ComicSources.DilbertComics
{
    public class DilbertComics : IDilbertComics
    {
        public async Task<string> GetDilbertComicUri()
        {
            DilbertServiceApi dilbertServiceApi = new DilbertServiceApi();

            string comicStripUri = await dilbertServiceApi.GetDilbertComicsUrl();

            comicStripUri = $"https:{comicStripUri}.png";

            return comicStripUri;
        }
    }
}