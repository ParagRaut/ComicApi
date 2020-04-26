using Microsoft.AspNetCore.Mvc;
using RandomComicApi.ComicServices.ComicSources;
using RandomComicApi.ComicServices.ComicSources.GarfieldComics;
using RandomComicApi.ComicServices.ComicSources.XKCD;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RandomComicApi.ComicServices
{
    public class ComicService : IComicService
    {
        public ComicService([NotNull] IGetXKCDComic getXKCDComic,
                            [NotNull] IGetGarfieldComics garfieldComics)
        {
            this.XKCDService = getXKCDComic;
            this.GarfieldComicsService = garfieldComics;
        }

        private IGetXKCDComic XKCDService { get; set; }

        private IGetGarfieldComics GarfieldComicsService { get; set; }

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
                    this.ComicImage = this.GetXkcdComics();
                    break;
            }

            return ComicImage;
        }

        private ComicEnum ChooseRandomComicSource()
        {
            var random = new Random();
            return (ComicEnum)random.Next(Enum.GetNames(typeof(ComicEnum)).Length);
        }

        private FileResult GetXkcdComics()
        {
            this.ComicImage = this.XKCDService.GetXkcdComic();
            return this.ComicImage;
        }

        private FileResult GetGarfieldComic()
        {
            this.ComicImage = this.GarfieldComicsService.GetGarfieldComic();
            return this.ComicImage;
        }        
    }
}
