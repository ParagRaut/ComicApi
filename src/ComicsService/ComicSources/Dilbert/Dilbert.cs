using System.Threading.Tasks;

namespace RandomComicApi.ComicsService.ComicSources.Dilbert
{
    public class Dilbert : IDilbert
    {
        public async Task<string> GetDilbertComicUri()
        {
            return await Service.GetComicUrl();
        }
    }
}