using System.Diagnostics;
using RandomComicApi.ComicsService.ComicSources.Xkcd.Models;

namespace RandomComicApi.ComicsService.ComicSources.Xkcd;

public class XkcdComic : IXkcdComic
{
    public XkcdComic(IXKCD xKcdComics)
    {
        XkcdService = xKcdComics;
    }

    private IXKCD XkcdService { get; }

    public async Task<string> GetXkcdComicUri()
    {
        int comicId = await GetRandomComicNumber();

        string comicImageUri = await GetImageUri(comicId);

        return comicImageUri;
    }

    private async Task<int> GetLatestComicId()
    {
        Comic response = await XkcdService.GetLatestComicAsync();
        Debug.Assert(response.Num != null, "response.Num != null");

        return (int)response.Num.Value;
    }

    private async Task<int> GetRandomComicNumber()
    {
        int maxId = await GetLatestComicId();
        var randomNumber = new Random();
        return randomNumber.Next(maxId);
    }

    private async Task<string> GetImageUri(int comicId)
    {
        Comic comicImage = await XkcdService.GetComicByIdAsync(comicId).ConfigureAwait(false);

        return comicImage.Img;
    }
}
