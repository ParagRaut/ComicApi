using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomComicApi.ComicsService.ComicSources.XKCD.Models;

namespace RandomComicApi.ComicsService.ComicSources.XKCD
{
    public class XkcdComic : IXkcdComic
    {
        public XkcdComic(IXKCD xKcdComics)
        {
            this.XkcdService = xKcdComics;
        }

        private IXKCD XkcdService { get; }

        public async Task<FileResult> GetXkcdComic()
        {
            int comicId = await this.GetRandomComicNumber();

            using (Task<FileResult> comicImageFile = this.DownloadImageAndReturn(comicId))
            {
                if (comicImageFile.Status != TaskStatus.RanToCompletion && !comicImageFile.IsFaulted)
                {
                    comicImageFile.Wait();
                }

                if (comicImageFile.Status == TaskStatus.RanToCompletion)
                {
                    return comicImageFile.Result;
                }
            }

            return null;
        }

        public async Task<string> GetXkcdComicUri()
        {
            int comicId = await this.GetRandomComicNumber();

            string comicImageUri = await this.GetImageUri(comicId);
            
            return comicImageUri;
        }

        private async Task<int> GetLatestComicId()
        {
            Comic response = await this.XkcdService.GetLatestComicAsync();
            Debug.Assert(response.Num != null, "response.Num != null");

            return (int)response.Num.Value;
        }

        private async Task<int> GetRandomComicNumber()
        {
            int maxId = await this.GetLatestComicId();
            var randomNumber = new Random();
            return randomNumber.Next(maxId);
        }

        private async Task<string> GetImageUri(int comicId)
        {
            Comic comicImage = await this.XkcdService.GetComicByIdAsync(comicId).ConfigureAwait(false);

            return comicImage.Img;
        }
        private async Task<FileResult> DownloadImageAndReturn(int comicId)
        {
            Comic comicImage = await this.XkcdService.GetComicByIdAsync(comicId).ConfigureAwait(false);

            var imgUrl = new Uri(comicImage.Img, UriKind.Absolute);

            byte[] imageBytes;

            using (var webClient = new WebClient())
            {
                imageBytes = webClient.DownloadData(imgUrl);
            }

            var memoryStream = new MemoryStream(imageBytes);

            return new FileStreamResult(memoryStream, "image/png");
        }
    }
}