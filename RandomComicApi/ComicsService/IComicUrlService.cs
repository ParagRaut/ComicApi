namespace RandomComicApi.ComicsService
{
    public interface IComicUrlService
    {
        string GetRandomComic();
        string GetDilbertComic();
        string GetGarfieldComic();
        string GetXkcdComic();
    }
}