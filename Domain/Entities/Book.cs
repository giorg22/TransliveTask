using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int Rating { get; set; }
        public int PublishYear { get; set; }
        public bool IsTaken { get; set; }
        public IEnumerable<AuthorBooks> AuthorBooks { get; set; }
    }
}
