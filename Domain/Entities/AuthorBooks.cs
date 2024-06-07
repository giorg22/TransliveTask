using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuthorBooks : BaseEntity
    {
        public string AuthorId { get; set; }
        public Author Author { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
    }
}
