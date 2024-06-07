using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class Response<T>
    {
        public bool Success { get; set; } = true;
        public T? Data { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
