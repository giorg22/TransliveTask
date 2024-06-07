using Application.Authors;
using Application.Authors.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1/[Controller]")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("GetAuthors")]
        public async Task<IActionResult> GetAuthors()
        {
            var result = await _authorService.GetAllAuthors();
            return Ok(result);
        }

        [HttpGet("Author/{id}")]
        public async Task<IActionResult> GetAuthorById(string id)
        {
            var result = await _authorService.GetAuthorById(id);
            return Ok(result);
        }

        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor(AddAuthorRequest request)
        {
            var result = await _authorService.CreateAuthor(request);
            return Ok(result);
        }

        [HttpDelete("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            var result = await _authorService.DeleteAuthor(id);
            return Ok(result);
        }
    }
}
