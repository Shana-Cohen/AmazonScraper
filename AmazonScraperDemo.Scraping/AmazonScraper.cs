using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Net.Http;

namespace AmazonScraperDemo.Scraping
{
    public static class AmazonScraper
    {
        public static List<AmazonItem> Scrape(string searchText)
        {
            var html = GetAmazonHtml(searchText);
            return ParseAmazonHtml(html);
        }

        private static List<AmazonItem> ParseAmazonHtml(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            var resultDivs = document.QuerySelectorAll(".s-result-item");
            var items = new List<AmazonItem>();
            foreach (var div in resultDivs)
            {
                var item = new AmazonItem();
                var titleSpan = div.QuerySelector("span.a-size-medium.a-color-base.a-text-normal");
                if(titleSpan == null)
                {
                    continue;
                }
                if (titleSpan != null)
                {
                    item.Title = titleSpan.TextContent;
                }

                var priceSpan = div.QuerySelector("span.a-offscreen");
                if (priceSpan != null)
                {
                    item.Price = priceSpan.TextContent;
                }

                var imageTag = div.QuerySelector(".s-image");
                if(imageTag != null)
                {
                    item.ImageUrl = imageTag.Attributes["src"].Value;
                }

                var linkTag = div.QuerySelector("a.a-link-normal.s-underline-text.s-underline-link-text.s-link-style.a-text-normal");
                if(linkTag != null)
                {
                    item.Link = $"https://amazon.com{linkTag.Attributes["href"].Value}";
                }

                items.Add(item);
            }

            return items;
        }

        private static string GetAmazonHtml(string searchText)
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };
            using var client = new HttpClient(handler);
            var url = $"https://www.amazon.com/s?k={searchText}";
            var html = client.GetStringAsync(url).Result;
            return html;
        }
    }
}
