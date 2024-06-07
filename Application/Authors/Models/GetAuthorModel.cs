using Application.Books.Model;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Models
{
    public class GetAuthorModel
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int BirthYear { get; set; }
        public IEnumerable<GetBookModel> Books { get; set; }
    }
}
