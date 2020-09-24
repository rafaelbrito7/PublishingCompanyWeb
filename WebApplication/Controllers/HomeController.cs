using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PublishingCompany.Domain;
using RestSharp;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        string _linkApi = "https://localhost:44320/api/";

        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrWhiteSpace(this.HttpContext.Session.GetString("Token")))
                return Redirect("Author/Index");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User user)
        {
            try
            {
                var client = new RestClient();
                var requestToken = new RestRequest(_linkApi + "Users/Token");

                requestToken.AddJsonBody(JsonConvert.SerializeObject(user));

                var result = client.Post<TokenResult>(requestToken).Data;


                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "Email or Password invalid!");
                    return View(user);
                }

                this.HttpContext.Session.SetString("Token", result.Token);

                return Redirect("Author/Index");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occured, please try again later!");
                return View(user);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class TokenResult
    {
        public string Token { get; set; }
    }

    public class loginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
