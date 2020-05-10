using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomComicApi.ComicsService.ComicSources;
using RandomComicApi.ComicsService.ComicSources.DilbertComics;
using RandomComicApi.ComicsService.ComicSources.GarfieldComics;
using RandomComicApi.ComicsService.ComicSources.XKCD;

namespace RandomComicApi.ComicsService
{
    public class ComicService : IComicService
    {
        public ComicService([NotNull] IXkcdComic xkcdComic,
            [NotNull] IGarfieldComics garfieldComics,
            [NotNull] IDilbertComics gDilbertComics,
            ILogger<ComicService> logger)
        {
            this.XkcdComicsService = xkcdComic;
            this.GarfieldComicsService = garfieldComics;
            this.DilbertComicsService = gDilbertComics;
            this._logger = logger;
        }

        private IXkcdComic XkcdComicsService { get; }

        private IGarfieldComics GarfieldComicsService { get; }

        private IDilbertComics DilbertComicsService { get; }

        private Task<FileResult> ComicImage { get; set; }

        private readonly ILogger _logger;


        public Task<FileResult> GetRandomComic()
        {
            ComicEnum comicName = this.ChooseRandomComicSource();

            switch (comicName)
            {
                case ComicEnum.Garfield:
                    this.ComicImage = this.GetGarfieldComic();
                    break;
                case ComicEnum.Xkcd:
                    this.ComicImage = this.GetXkcdComic();
                    break;
                case ComicEnum.Dilbert:
                    this.ComicImage = this.GetDilbertComic();
                    break;
                default:
                    this._logger.LogInformation("Argument exception is thrown");
                    throw new ArgumentOutOfRangeException();
            }

            this._logger.LogInformation($"Returning {comicName} comic strip");

            return this.ComicImage;
        }

        private ComicEnum ChooseRandomComicSource()
        {
            var random = new Random();
            return (ComicEnum) random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
        }

        public Task<FileResult> GetXkcdComic()
        {
            this.ComicImage = this.XkcdComicsService.GetXkcdComic();
            return this.ComicImage;
        }

        public Task<FileResult> GetGarfieldComic()
        {
            this.ComicImage = this.GarfieldComicsService.GetGarfieldComic();
            return this.ComicImage;
        }

        public Task<FileResult> GetDilbertComic()
        {
            this.ComicImage = this.DilbertComicsService.GetDilbertComic();
            return this.ComicImage;
        }
    }
}