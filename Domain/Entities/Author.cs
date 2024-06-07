using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int BirthYear { get; set; }
        public ICollection<AuthorBooks> AuthorBooks { get; set; }
    }
}
