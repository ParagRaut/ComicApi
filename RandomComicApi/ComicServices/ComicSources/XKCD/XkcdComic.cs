﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomComicApi.ComicServices.ComicSources.XKCD.Models;

namespace RandomComicApi.ComicServices.ComicSources.XKCD
{
    public class XkcdComic : IXkcdComic
    {
        public XkcdComic(IXKCD xKcdComics)
        {
            this.XkcdService = xKcdComics;
        }

        private IXKCD XkcdService { get; }

        public FileResult GetXkcdComic()
        {
            int comicId = this.GetRandomComicNumber();

            using (Task<FileResult> comicImageFile = this.DownloadImageAndReturn(comicId))
            {
                if (comicImageFile.Status != TaskStatus.RanToCompletion && !comicImageFile.IsFaulted)
                    comicImageFile.Wait();
                if (comicImageFile.Status == TaskStatus.RanToCompletion) return comicImageFile.Result;
            }

            return null;
        }

        private int GetLatestComicId()
        {
            Comic response = this.XkcdService.GetLatestComic();
            Debug.Assert(response.Num != null, "response.Num != null");

            return (int)response.Num.Value;
        }

        private int GetRandomComicNumber()
        {
            int maxId = this.GetLatestComicId();
            var randomNumber = new Random();
            return randomNumber.Next(maxId);
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