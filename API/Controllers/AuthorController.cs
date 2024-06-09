using Application.Authors;
using Application.Authors.Requests;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(string id)
        {
            var result = await _authorService.GetAuthorById(id);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor(AddAuthorRequest request)
        {
            var result = await _authorService.CreateAuthor(request);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorRequest request)
        {
            var result = await _authorService.UpdateAuthor(request);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            var result = await _authorService.DeleteAuthor(id);
            return Ok(result);
        }
    }
}
