using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public enum ErrorCode
    {
        Success = 0,

        NotFound = 1,
        UserNotFound = 2,
        AuthorNotFound = 3,
        BookNotFound = 4,
        UserAlreadyExists = 5,
        ValidationFailed = 6,
        InvalidUsernameOrPassword = 7
    }
}
