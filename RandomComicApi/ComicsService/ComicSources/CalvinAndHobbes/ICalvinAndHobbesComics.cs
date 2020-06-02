using System.Threading.Tasks;

namespace RandomComicApi.ComicsService.ComicSources.CalvinAndHobbes
{
    public interface ICalvinAndHobbesComics
    {
        Task<string> CalvinAndHobbesComicUri();
    }
}
