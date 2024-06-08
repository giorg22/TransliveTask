using Application.Auth.Requests;
using Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth
{
    public interface IAuthService
    {
        Task<Response<string>> Login(LoginRequest request);
        Task<Response<string>> Register(RegisterRequest request);
    }
}
