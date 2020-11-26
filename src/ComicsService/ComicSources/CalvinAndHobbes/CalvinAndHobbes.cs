using System.Threading.Tasks;

namespace RandomComicApi.ComicsService.ComicSources.CalvinAndHobbes
{
    public class CalvinAndHobbes : ICalvinAndHobbes
    {
        public async Task<string> CalvinAndHobbesComicUri()
        {
            return await Service.GetComicUri();            
        }
    }
}
