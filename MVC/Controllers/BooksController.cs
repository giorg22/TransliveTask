using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure;
using Application.Authors.Requests;
using Application.Shared;
using System.Net.Http;
using MVC.Client;
using Application.Books.Requests;
using System.Runtime.CompilerServices;
using Mapster;

namespace MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClientService _httpClient;

        public BooksController(HttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Books
        // GET: Author

        public async Task<IActionResult> Index(string SearchString)
        {
            var response = await _httpClient.GetBooks();

            ViewData["booksFilter"] = SearchString;

            var books = response.Data;

            books = !string.IsNullOrEmpty(SearchString)
                ? books.Where(i => i.Title.ToLower().Contains(SearchString))
                : books;

            return View(books);
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetBookById(id);
            if (!response.Success && response.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound();
            }

            return View(response.Data);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var response = await _httpClient.GetAuthors();
            ViewBag.Authors = response.Data;
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddBookRequest book)
        {
            if (ModelState.IsValid)
            {
                var result = await _httpClient.CreateBook(book);
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

            var result = await _httpClient.GetAuthors();
            ViewBag.Authors = result.Data;

            var response = await _httpClient.GetBookById(id);
            if (!response.Success && response.ErrorCode == ErrorCode.NotFound)
            {
                return NotFound();
            }
            return View(response.Data.Adapt<UpdateBookRequest>());
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UpdateBookRequest book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            var result = await _httpClient.GetAuthors();
            ViewBag.Authors = result.Data;

            if (ModelState.IsValid)
            {
                await _httpClient.UpdateBook(book);

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Author/Delete/5

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _httpClient.GetBookById(id);
            if (book.Data == null)
            {
                return NotFound();
            }

            await _httpClient.DeleteBook(id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _httpClient.ChangeBookStatus(id);

            return RedirectToAction(nameof(Details), new { id });
        }

    }
}
