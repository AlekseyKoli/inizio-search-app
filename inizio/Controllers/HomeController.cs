using System.Diagnostics;
using inizio.Models;
using Microsoft.AspNetCore.Mvc;

namespace inizio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(string SearchText)
        {
            List<GoogleSite> sites = new List<GoogleSite>();
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                return View(sites);
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://google.serper.dev/search");
            request.Headers.Add("X-API-KEY", "API_KEY_HERE");
            var content = new StringContent($"{{\"q\":\"{SearchText}\",\"gl\":\"cz\",\"hl\":\"cs\"}}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var searchResult = System.Text.Json.JsonSerializer.Deserialize<SearchResult>(jsonResponse);

            sites = searchResult?.Sites ?? new List<GoogleSite>();

            return View(sites);
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
