﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace RandomComicApi.ComicsService.ComicSources.CalvinAndHobbes
{
    public class Service
    {
        public static async Task<string> GetComicUrl()
        {
            var baseUrl = new Uri($" https://www.gocomics.com/random/calvinandhobbes");

            var httpClient = new HttpClient();

            string source = await httpClient.GetStringAsync(baseUrl);

            string imageLink = GetImageUri(source);

            return imageLink;
        }

        private static string GetImageUri(string source)
        {
            var document = new HtmlDocument();

            document.LoadHtml(source);

            const string imageClassNode = "//a[contains(@class, 'js-item-comic-link')]/picture/img";

            HtmlNode imageNode = document.DocumentNode.SelectSingleNode(imageClassNode);

            string imageLink = imageNode.GetAttributeValue("src", "");

            return imageLink;
        }
    }
}