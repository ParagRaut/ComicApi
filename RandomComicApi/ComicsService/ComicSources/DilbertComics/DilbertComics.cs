using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RandomComicApi.ComicsService.ComicSources.DilbertComics
{
    public class DilbertComics : IDilbertComics
    {
        public DilbertComics()
        {
            this.BaseUri = new Uri("https://discordians-api.herokuapp.com/comic");
        }

        private Uri BaseUri { get; }

        private ComicModel ComicModel { get; set; }

        public async Task<FileResult> GetDilbertComic()
        {
            var comicUri = new Uri($"{this.BaseUri}/dilbert");

            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(comicUri);
            this.ComicModel = JsonConvert.DeserializeObject<ComicModel>(response);
            
            this.ComicModel.image = $"https:{this.ComicModel.image}.png";

            byte[] imageBytes;

            var responseData = new WebClient();
            imageBytes = responseData.DownloadData(this.ComicModel.image);

            var memoryStream = new MemoryStream(imageBytes);

            return new FileStreamResult(memoryStream, "image/gif");
        }

        public async Task<string> GetDilbertComicUri()
        {
            var comicUri = new Uri($"{this.BaseUri}/dilbert");


            var httpClient = new HttpClient();

            string response = await httpClient.GetStringAsync(comicUri);
            this.ComicModel = JsonConvert.DeserializeObject<ComicModel>(response);


            this.ComicModel.image = $"https:{this.ComicModel.image}.png";

            return this.ComicModel.image;
        }
    }
}