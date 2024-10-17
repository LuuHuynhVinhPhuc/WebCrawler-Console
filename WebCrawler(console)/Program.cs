using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Text;

namespace WebScrapper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Create an instance of HttpClient
            using (var client = new HttpClient())
            {
                try
                {
                    // Specify the URL you want to scrape
                    Console.Write("Import your link: ");
                    string url = Console.ReadLine(); // Replace with your target URL

                    // Send a GET request to the URL
                    string html = await client.GetStringAsync(url);

                    // Create an HtmlDocument instance and load the HTML content
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);

                    // Use XPath to select specific elements
                    var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//title");
                    if (titleNode != null)
                    {
                        Console.WriteLine($"Page Title: {titleNode.InnerText}");
                    }

                    // Example: Extract all paragraph texts
                    var paragraphs = htmlDocument.DocumentNode.SelectNodes("//p");
                    if (paragraphs != null)
                    {
                        Console.WriteLine("\nParagraphs:");
                        foreach (var paragraph in paragraphs)
                        {
                            Console.WriteLine(paragraph.InnerText.Trim());
                        }
                    }

                    // Example: Extract all links
                    var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
                    if (links != null)
                    {
                        Console.WriteLine("\nLinks:");
                        foreach (var link in links)
                        {
                            string href = link.GetAttributeValue("href", "");
                            Console.WriteLine($"- {link.InnerText}: {href}");
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error fetching the web page: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }
        }
    }
}