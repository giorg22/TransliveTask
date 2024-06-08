using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Requests
{
    public class AddBookRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int PublishYear { get; set; }
        public bool IsTaken { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public IEnumerable<string> AuthorIds { get; set; }
    }
}
