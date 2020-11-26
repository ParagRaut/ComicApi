using System.Threading.Tasks;

namespace RandomComicApi.ComicsService.ComicSources.CalvinAndHobbes
{
    public interface ICalvinAndHobbes
    {
        Task<string> CalvinAndHobbesComicUri();
    }
}
