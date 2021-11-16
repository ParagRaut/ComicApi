namespace RandomComicApi.ComicsService.XKCD;

public interface IXKCDService
{
    Task<string> GetComicUri();
}
