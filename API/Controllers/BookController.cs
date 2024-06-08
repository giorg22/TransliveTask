using Application.Authors.Requests;
using Application.Books;
using Application.Books.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[Controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _bookService.GetAllBooks();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(string id)
        {
            var result = await _bookService.GetBookById(id);
            return Ok(result);
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromForm] AddBookRequest request)
        {
            var result = await _bookService.CreateBook(request);
            return Ok(result);
        }

        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook([FromForm] UpdateBookRequest request)
        {
            var result = await _bookService.UpdateBook(request);
            return Ok(result);
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var result = await _bookService.DeleteBook(id);
            return Ok(result);
        }
    }
}
