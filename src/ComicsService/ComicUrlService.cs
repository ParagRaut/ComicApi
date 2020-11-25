using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RandomComicApi.ComicsService.ComicSources;
using RandomComicApi.ComicsService.ComicSources.CalvinAndHobbes;
using RandomComicApi.ComicsService.ComicSources.DilbertComics;
using RandomComicApi.ComicsService.ComicSources.GarfieldComics;
using RandomComicApi.ComicsService.ComicSources.XKCD;

namespace RandomComicApi.ComicsService
{
    public class ComicUrlService : IComicUrlService
    {
        public ComicUrlService(
            [NotNull] IXkcdComic xkcdComic,
            [NotNull] IGarfieldComics garfieldComics,
            [NotNull] IDilbertComics dilbertComics,
            [NotNull] ICalvinAndHobbesComics calvinAndHobbesComics,
            ILogger<ComicUrlService> logger)
        {
            this._xkcdComic = xkcdComic;
            this._garfieldComics = garfieldComics;
            this._dilbertComics = dilbertComics;
            this._calvinAndHobbesComics = calvinAndHobbesComics;
            this._logger = logger;
        }

        private readonly IXkcdComic _xkcdComic;
        private readonly IGarfieldComics _garfieldComics;
        private readonly IDilbertComics _dilbertComics;
        private readonly ICalvinAndHobbesComics _calvinAndHobbesComics;
        private readonly ILogger _logger;

        public Task<string> GetRandomComic()
        {
            ComicEnum comicName = ChooseRandomComicSource();

            return comicName switch
            {
                ComicEnum.Garfield => this.GetGarfieldComic(),
                ComicEnum.Xkcd => this.GetXkcdComic(),
                ComicEnum.Dilbert => this.GetDilbertComic(),
                ComicEnum.CalvinAndHobbes => this.GetCalvinAndHobbesComic(),
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
            this._logger.LogInformation($"Returning Dilbert comic strip");

            return await this._dilbertComics.GetDilbertComicUri();
        }

        public async Task<string> GetGarfieldComic()
        {
            this._logger.LogInformation($"Returning Garfield comic strip");

            return await this._garfieldComics.GetGarfieldComicUri();
        }

        public async Task<string> GetXkcdComic()
        {
            this._logger.LogInformation($"Returning XKCD comic strip");

            return await this._xkcdComic.GetXkcdComicUri();
        }

        public async Task<string> GetCalvinAndHobbesComic()
        {
            this._logger.LogInformation($"Returning Calvin and Hobbes comic strip");

            return await this._calvinAndHobbesComics.CalvinAndHobbesComicUri();
        }
    }
}
