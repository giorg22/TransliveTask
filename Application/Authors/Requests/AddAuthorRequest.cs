using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Requests
{
    public class AddAuthorRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int BirthYear { get; set; }
    }
}
