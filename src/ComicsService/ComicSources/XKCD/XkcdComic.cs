using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomComicApi.ComicsService.ComicSources.Xkcd.Models;

namespace RandomComicApi.ComicsService.ComicSources.Xkcd
{
    public class XkcdComic : IXkcdComic
    {
        public XkcdComic(IXKCD xKcdComics)
        {
            XkcdService = xKcdComics;
        }

        private IXKCD XkcdService { get; }

        public async Task<FileResult> GetXkcdComic()
        {
            int comicId = await GetRandomComicNumber();

            using (Task<FileResult> comicImageFile = DownloadImageAndReturn(comicId))
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

        private async Task<FileResult> DownloadImageAndReturn(int comicId)
        {
            Comic comicImage = await XkcdService.GetComicByIdAsync(comicId).ConfigureAwait(false);

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