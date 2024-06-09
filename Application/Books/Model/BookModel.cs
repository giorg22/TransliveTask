using Application.Authors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Model
{
    public class BookModel 
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public double Rating { get; set; }
        public int PublishYear { get; set; }
        public bool IsTaken { get; set; }
        public IEnumerable<AuthorModel> Authors { get; set; }
    }
}
