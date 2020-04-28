using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using RandomComicApi.ComicServices.ComicSources;
using RandomComicApi.ComicServices.ComicSources.DilbertComics;
using RandomComicApi.ComicServices.ComicSources.GarfieldComics;
using RandomComicApi.ComicServices.ComicSources.XKCD;

namespace RandomComicApi.ComicServices
{
    public class ComicService : IComicService
    {
        public ComicService([NotNull] IXkcdComic xkcdComic,
            [NotNull] IGarfieldComics garfieldComics,
            [NotNull] IDilbertComics gDilbertComics)
        {
            this.XkcdComicsService = xkcdComic;
            this.GarfieldComicsService = garfieldComics;
            this.DilbertComicsService = gDilbertComics;
        }

        private IXkcdComic XkcdComicsService { get; }

        private IGarfieldComics GarfieldComicsService { get; }

        private IDilbertComics DilbertComicsService { get; }

        private FileResult ComicImage { get; set; }

        public FileResult GetRandomComic()
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
                    throw new ArgumentOutOfRangeException();
            }

            return this.ComicImage;
        }

        private ComicEnum ChooseRandomComicSource()
        {
            var random = new Random();
            return (ComicEnum) random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
        }

        private FileResult GetXkcdComic()
        {
            this.ComicImage = this.XkcdComicsService.GetXkcdComic();
            return this.ComicImage;
        }

        private FileResult GetGarfieldComic()
        {
            this.ComicImage = this.GarfieldComicsService.GetGarfieldComic();
            return this.ComicImage;
        }

        private FileResult GetDilbertComic()
        {
            this.ComicImage = this.DilbertComicsService.GetDilbertComic();
            return this.ComicImage;
        }
    }
}