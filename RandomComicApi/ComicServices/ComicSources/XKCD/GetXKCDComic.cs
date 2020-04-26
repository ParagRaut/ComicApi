using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RandomComicApi.ComicServices.ComicSources.XKCD
{
    public class GetXKCDComic : IGetXKCDComic
    {
        public GetXKCDComic(IXKCD xKCDComics)
        {
            this.XKCDService = xKCDComics;
        }

        private IXKCD XKCDService { get; set; }

        public FileResult GetXkcdComic()
        {
            int comicId = this.GetRandomComicNumber();

            using (var comicImageFile = this.DownloadImageAndReturn(comicId))
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

        private int GetRandomComicNumber()
        {
            var randomNumber = new Random();
            return (int)randomNumber.Next(2298);
        }

        private async Task<FileResult> DownloadImageAndReturn(int comicId)
        {
            var comicImage = await this.XKCDService.GetComicByIdAsync(comicId).ConfigureAwait(false);

            var imgUrl = new Uri(comicImage.Img, UriKind.Absolute);

            byte[] imageBytes;

            using (var webClient = new WebClient())
            {
                imageBytes = webClient.DownloadData(imgUrl);
            }

            MemoryStream memoryStream = new MemoryStream(imageBytes);

            return new FileStreamResult(memoryStream, "image/png");
        }
    }
}
