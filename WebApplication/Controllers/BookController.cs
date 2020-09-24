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
    public class BookController : Controller
    {
        string _linkApi = "https://localhost:44320/api/";
        // GET: BookController
        public ActionResult Index()
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "Books", DataFormat.Json);

            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

            var response = client.Get<List<Book>>(request);

            if (response.Data == null)
                response.Data = new List<Book>();

            return View(response.Data);
        }

        // GET: BookController/Details/5
        public ActionResult Details(Guid id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "Books/" + id, DataFormat.Json);

            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

            var response = client.Get<Book>(request);

            return View(response.Data);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(book);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "Books", DataFormat.Json);
                request.AddJsonBody(book);

                request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

                var response = client.Post<Book>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occured, please try again later!");
                return View(book);
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "Books/" + id, DataFormat.Json);

            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

            var response = client.Get<Book>(request);

            return View(response.Data);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Book book)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(book);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "Books/" + id, DataFormat.Json);
                request.AddJsonBody(book);

                request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

                var response = client.Put<Book>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "Books/" + id, DataFormat.Json);

            request.AddHeader("Authorization", $"Bearer {this.HttpContext.Session.GetString("Token")}");

            var response = client.Get<Book>(request);

            return View(response.Data);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Book book)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(_linkApi + "Books/" + id, DataFormat.Json);
                request.AddJsonBody(book);

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
