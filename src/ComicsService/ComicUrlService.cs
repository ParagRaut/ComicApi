using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RandomComicApi.ComicsService.ComicSources;
using RandomComicApi.ComicsService.ComicSources.CalvinAndHobbes;
using RandomComicApi.ComicsService.ComicSources.Dilbert;
using RandomComicApi.ComicsService.ComicSources.Garfield;
using RandomComicApi.ComicsService.ComicSources.Xkcd;

namespace RandomComicApi.ComicsService
{
    public class ComicUrlService : IComicUrlService
    {
        public ComicUrlService(
            [NotNull] IXkcdComic xkcdComic,
            [NotNull] IGarfield garfieldComics,
            [NotNull] IDilbert dilbertComics,
            [NotNull] ICalvinAndHobbes calvinAndHobbesComics,
            ILogger<ComicUrlService> logger)
        {
            _xkcdComic = xkcdComic;
            _garfieldComics = garfieldComics;
            _dilbertComics = dilbertComics;
            _calvinAndHobbesComics = calvinAndHobbesComics;
            _logger = logger;
        }

        private readonly IXkcdComic _xkcdComic;
        private readonly IGarfield _garfieldComics;
        private readonly IDilbert _dilbertComics;
        private readonly ICalvinAndHobbes _calvinAndHobbesComics;
        private readonly ILogger _logger;

        public Task<string> GetRandomComic()
        {
            ComicEnum comicName = ChooseRandomComicSource();

            return comicName switch
            {
                ComicEnum.Garfield => GetGarfieldComic(),
                ComicEnum.Xkcd => GetXkcdComic(),
                ComicEnum.Dilbert => GetDilbertComic(),
                ComicEnum.CalvinAndHobbes => GetCalvinAndHobbesComic(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static ComicEnum ChooseRandomComicSource()
        {
            var random = new Random();

            return (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
        }

        public async Task<string> GetDilbertComic()
        {
            _logger.LogInformation($"Returning Dilbert comic strip");

            return await _dilbertComics.GetDilbertComicUri();
        }

        public async Task<string> GetGarfieldComic()
        {
            _logger.LogInformation($"Returning Garfield comic strip");

            return await _garfieldComics.GetGarfieldComicUri();
        }

        public async Task<string> GetXkcdComic()
        {
            _logger.LogInformation($"Returning XKCD comic strip");

            return await _xkcdComic.GetXkcdComicUri();
        }

        public async Task<string> GetCalvinAndHobbesComic()
        {
            _logger.LogInformation($"Returning Calvin and Hobbes comic strip");

            return await _calvinAndHobbesComics.CalvinAndHobbesComicUri();
        }
    }
}
