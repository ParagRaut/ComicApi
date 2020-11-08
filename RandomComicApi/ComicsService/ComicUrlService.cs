using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            this.XkcdComicsService = xkcdComic;
            this.GarfieldComicsService = garfieldComics;
            this.DilbertComicsService = dilbertComics;
            this.CalvinAndHobbesComicsService = calvinAndHobbesComics;
            this._logger = logger;
        }

        private IXkcdComic XkcdComicsService { get; }

        private IGarfieldComics GarfieldComicsService { get; }

        private IDilbertComics DilbertComicsService { get; }

        private ICalvinAndHobbesComics CalvinAndHobbesComicsService { get; }

        private Task<string> ComicImageUri { get; set; }

        private readonly ILogger _logger;

        public Task<string> GetRandomComic()
        {
            ComicEnum comicName = this.ChooseRandomComicSource();

            switch (comicName)
            {
                case ComicEnum.Garfield:
                    this.ComicImageUri = this.GetGarfieldComic();
                    break;
                case ComicEnum.Xkcd:
                    this.ComicImageUri = this.GetXkcdComic();
                    break;
                case ComicEnum.Dilbert:
                    this.ComicImageUri = this.GetDilbertComic();
                    break;
                case ComicEnum.CalvinAndHobbes:
                    this.ComicImageUri = this.GetCalvinAndHobbesComic();
                    break;
                default:
                    this._logger.LogInformation("Argument exception is thrown");
                    throw new ArgumentOutOfRangeException();
            }

            return this.ComicImageUri;
        }

        private ComicEnum ChooseRandomComicSource()
        {
            var random = new Random();
            return (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
        }

        public async Task<string> GetDilbertComic()
        {
            this._logger.LogInformation($"Returning Dilbert comic strip");

            return await this.DilbertComicsService.GetDilbertComicUri();
        }

        public async Task<string> GetGarfieldComic()
        {
            this._logger.LogInformation($"Returning Garfield comic strip");

            return await this.GarfieldComicsService.GetGarfieldComicUri();
        }

        public async Task<string> GetXkcdComic()
        {
            this._logger.LogInformation($"Returning XKCD comic strip");

            return await this.XkcdComicsService.GetXkcdComicUri();
        }

        public async Task<string> GetCalvinAndHobbesComic()
        {
            this._logger.LogInformation($"Returning Calvin and Hobbes comic strip");

            return await this.CalvinAndHobbesComicsService.CalvinAndHobbesComicUri();
        }
    }
}
