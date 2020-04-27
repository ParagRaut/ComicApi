using Microsoft.AspNetCore.Mvc;
using RandomComicApi.ComicServices.ComicSources;
using RandomComicApi.ComicServices.ComicSources.DilbertComics;
using RandomComicApi.ComicServices.ComicSources.GarfieldComics;
using RandomComicApi.ComicServices.ComicSources.XKCD;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RandomComicApi.ComicServices
{
    public class ComicService : IComicService
    {
        public ComicService([NotNull] IGetXKCDComic getXKCDComic,
                            [NotNull] IGetGarfieldComics garfieldComics,
                            [NotNull] IGetGDilbertComics getGDilbertComics)
        {
            this.XKCDService = getXKCDComic;
            this.GarfieldComicsService = garfieldComics;
            this.DilbertComicsService = getGDilbertComics;
        }

        private IGetXKCDComic XKCDService { get; set; }

        private IGetGarfieldComics GarfieldComicsService { get; set; }

        private IGetGDilbertComics DilbertComicsService { get; set; }

        private FileResult ComicImage { get; set; }

        public FileResult GetRandomComic()
        {
            var comicName = this.ChooseRandomComicSource();

            switch (comicName)
            {
                case ComicEnum.Garfield:
                    this.ComicImage = this.GetGarfieldComic();
                    break;
                case ComicEnum.XKCD:
                    this.ComicImage = this.GetXkcdComic();
                    break;
                case ComicEnum.Dilbert:
                    this.ComicImage = this.GetDilbertComic();
                    break;
            }

            return ComicImage;
        }

        private ComicEnum ChooseRandomComicSource()
        {
            var random = new Random();
            return (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
        }

        private FileResult GetXkcdComic()
        {
            this.ComicImage = this.XKCDService.GetXkcdComic();
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
