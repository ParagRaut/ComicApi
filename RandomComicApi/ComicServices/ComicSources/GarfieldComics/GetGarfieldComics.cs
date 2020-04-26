using System;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace RandomComicApi.ComicServices.ComicSources.GarfieldComics
{
    public class GetGarfieldComics : IGetGarfieldComics
    {
        public GetGarfieldComics()
        {
            this.BaseUri = new Uri("https://discordians-api.herokuapp.com/comic");
        }

        private Uri BaseUri { get; set; }

        private GarfieldComicModel VariousComicModel { get; set; }


        public FileResult GetGarfieldComic()
        {
            var comicUri = new Uri($"{this.BaseUri}/garfield");

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(comicUri).Result;
                VariousComicModel = JsonConvert.DeserializeObject<GarfieldComicModel>(response);
            }

            byte[] imageBytes;

            using (var response = new WebClient())
            {
                imageBytes = response.DownloadData(VariousComicModel.image);
            }

            MemoryStream memoryStream = new MemoryStream(imageBytes);

            return new FileStreamResult(memoryStream, "image/gif");
        }

    }
}
