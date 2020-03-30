using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using Virtual_Queue.Models;

namespace Virtual_Queue.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        [HttpPost]
        public async Task<ActionResult> ValidateAsync(SubscribeModel model)
        {
            // if (ModelState.IsValid)
            //{
            //    var values = new Dictionary<string, string>
            //                            {
            //                                { "storeid", model.StoreID },
            //                                { "secretcode", model.Secret }
            //                            };

            //    var client = new HttpClient();
            //    client.BaseAddress = new Uri("https://covid19hackathonbot.azurewebsites.net/");
            //    var content = new FormUrlEncodedContent(values);
            //    var response = await client.PostAsync("api/Queue", content);

            //    model.Response = await response.Content.ReadAsStringAsync();
            //}


            var client = new RestClient($"https://covid19hackathonbot.azurewebsites.net/api/Queue?storeid={model.StoreID}&secretcode={model.Secret}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Cookie", "ARRAffinity=805c2491375cb99688705ea9b53fd3699437dea86054c9e040cc24406d1ad374");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            model.Response = response.Content.ToString();

            return View("Index", model);
        }


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string id)
        {
            var SubscribeModel = new SubscribeModel();
            SubscribeModel.StoreID = id;
            return View(SubscribeModel);
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
