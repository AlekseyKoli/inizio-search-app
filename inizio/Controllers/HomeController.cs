using inizio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace inizio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        private async Task<List<GoogleSite>> SearchGoogle(string SearchText)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://google.serper.dev/search");

            var apiKey = _configuration["SerperApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException("Serper API key is missing.");
            }
            request.Headers.Add("X-API-KEY", apiKey);


            var content = new StringContent($"{{\"q\":\"{SearchText}\",\"gl\":\"cz\",\"hl\":\"cs\"}}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var searchResult = System.Text.Json.JsonSerializer.Deserialize<SearchResult>(jsonResponse);

            List<GoogleSite> sites = searchResult?.Sites ?? new List<GoogleSite>();

            return sites;
        }

        public async Task<IActionResult> Index(string SearchText)
        {
            List<GoogleSite> sites = new List<GoogleSite>();
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                sites = await SearchGoogle(SearchText);
            }

            return View(sites);
        }

        public async Task<IActionResult> DownloadJson(string SearchText)
        {
            List<GoogleSite> sites = new List<GoogleSite>();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                sites = await SearchGoogle(SearchText);
            }

            var jsonToDownload = JsonSerializer.Serialize(sites, new JsonSerializerOptions { WriteIndented = true });

            return File(
                System.Text.Encoding.UTF8.GetBytes(jsonToDownload),
                "application/json",
                "search_results.json"
                );
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        
    }
}
