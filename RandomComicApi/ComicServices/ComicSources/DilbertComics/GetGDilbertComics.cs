using System;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace RandomComicApi.ComicServices.ComicSources.DilbertComics
{
    public class GetGDilbertComics : IGetGDilbertComics
    {
        public GetGDilbertComics()
        {
            this.BaseUri = new Uri("https://discordians-api.herokuapp.com/comic");
        }

        private Uri BaseUri { get; set; }

        private ComicModel ComicModel { get; set; }

        public FileResult GetDilbertComic()
        {
            var comicUri = new Uri($"{this.BaseUri}/dilbert");

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(comicUri).Result;
                this.ComicModel = JsonConvert.DeserializeObject<ComicModel>(response);
            }

            this.ComicModel.image = $"https:{this.ComicModel.image}.png";

            byte[] imageBytes;

            using (var response = new WebClient())
            {
                imageBytes = response.DownloadData(this.ComicModel.image);
            }

            MemoryStream memoryStream = new MemoryStream(imageBytes);

            return new FileStreamResult(memoryStream, "image/gif");
        }        

    }
}
