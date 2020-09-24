using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublishingCompany.Domain;
using RestSharp;

namespace WebApplication.Controllers
{
    public class AuthorController : Controller
    {
        string _linkApi = "https://localhost:44320/api/";
        // GET: AuthorController
        public ActionResult Index()
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "Authors", DataFormat.Json);

            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

            var response = client.Get<List<Author>>(request);

            if (response.Data == null)
                response.Data = new List<Author>();

            return View(response.Data);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(Guid id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "Authors/" + id, DataFormat.Json);

            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

            var response = client.Get<Author>(request);

            return View(response.Data);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(author);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "Authors", DataFormat.Json);
                request.AddJsonBody(author);

                request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

                var response = client.Post<Author>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occured, please try again later!");
                return View(author);
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "Authors/" + id, DataFormat.Json);

            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

            var response = client.Get<Author>(request);

            return View(response.Data);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Author author)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(author);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "Authors/" + id, DataFormat.Json);
                request.AddJsonBody(author);

                request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

                var response = client.Put<Author>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "Authors/" + id, DataFormat.Json);

            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

            var response = client.Get<Author>(request);

            return View(response.Data);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Author author)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(_linkApi + "Authors/" + id, DataFormat.Json);
                request.AddJsonBody(author);

                request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

                var response = client.Delete<Author>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
