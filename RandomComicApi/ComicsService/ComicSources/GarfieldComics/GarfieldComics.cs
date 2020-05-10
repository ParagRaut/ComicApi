using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RandomComicApi.ComicsService.ComicSources.GarfieldComics
{
    public class GarfieldComics : IGarfieldComics
    {
        public GarfieldComics()
        {
            this.BaseUri = new Uri("https://discordians-api.herokuapp.com/comic");
        }

        private Uri BaseUri { get; }

        private ComicModel ComicModel { get; set; }

        public async Task<FileResult> GetGarfieldComic()
        {
            var comicUri = new Uri($"{this.BaseUri}/garfield");

            var httpClient = new HttpClient();

            string response = await httpClient.GetStringAsync(comicUri);
            this.ComicModel = JsonConvert.DeserializeObject<ComicModel>(response);
            
            byte[] imageBytes;

            var responseData = new WebClient();
            imageBytes = responseData.DownloadData(this.ComicModel.image);
            
            var memoryStream = new MemoryStream(imageBytes);

            return new FileStreamResult(memoryStream, "image/gif");
        }

        public async Task<string> GetGarfieldComicUri()
        {
            var comicUri = new Uri($"{this.BaseUri}/garfield");

            var httpClient = new HttpClient();

            string response = await httpClient.GetStringAsync(comicUri);
            this.ComicModel = JsonConvert.DeserializeObject<ComicModel>(response);

            return this.ComicModel.image;
        }
    }
}