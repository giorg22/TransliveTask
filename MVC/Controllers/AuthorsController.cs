using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure;
using MVC.Client;
using Application.Shared;
using Application.Authors.Requests;
using System.Threading;
using Mapster;

namespace MVC.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly HttpClientService _httpClient;

        public AuthorsController(HttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Author
        public async Task<IActionResult> Index(string searchString)
        {
            var response = await _httpClient.GetAuthors();

            ViewData["authorsFilter"] = searchString;

            var authors = response.Data;

            authors = !string.IsNullOrEmpty(searchString)
                ? authors.Where(i => i.Firstname.ToLower().Contains(searchString) | i.Lastname.ToLower().Contains(searchString))
                : authors;

            return View(authors);
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAuthorById(id);
            if (!response.Success && response.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound();
            }

            return View(response.Data);
        }

        // GET: Author/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddAuthorRequest author)
        {
            if (ModelState.IsValid)
            {
                var result = await _httpClient.CreateAuthhor(author);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Author/Edit/5

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAuthorById(id);
            if (!response.Success && response.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound();
            }
            return View(response.Data.Adapt<UpdateAuthorRequest>());
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UpdateAuthorRequest author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _httpClient.UpdateAuthor(author);

                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Author/Delete/5

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _httpClient.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }

            await _httpClient.DeleteAuthor(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
