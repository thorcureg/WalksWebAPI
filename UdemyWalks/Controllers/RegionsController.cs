using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using UdemyWalks.Models;
using UdemyWalks.Models.DTO;
using static System.Net.WebRequestMethods;

namespace UdemyWalks.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {
                //Get All Regions from web api
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7051/api/Regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception)
            {
                //Log the exception
                throw;
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7051/api/Regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var httpResponse = await response.Content.ReadFromJsonAsync<RegionDto>();

            if (httpResponse != null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7051/api/Regions/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto model)
        {
            var client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7051/api/Regions/{model.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var httpResponse = await response.Content.ReadFromJsonAsync<RegionDto>();

            if (httpResponse != null)
            {
                return RedirectToAction("Edit", "Regions");
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto dto)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var response = await client.DeleteAsync($"https://localhost:7051/api/Regions/{dto.Id}");

                response.EnsureSuccessStatusCode();
                
                return RedirectToAction("Index", "Regions");
            }
            catch (Exception)
            {

            }

            return View("Edit");
        }
    }
}
