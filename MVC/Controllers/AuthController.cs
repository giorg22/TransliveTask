using Application.Auth.Requests;
using Microsoft.AspNetCore.Mvc;
using MVC.Client;

namespace BuyStuff.GE.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClientService _httpClient;

        public AuthController(HttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _httpClient.Login(request);
            if (!result.Success)
            {
                ViewBag.Errors = result.Errors;
                return View();
            }
            Response.Cookies.Append("jwt", result.Data);
            Response.Cookies.Append("username", request.UserName);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _httpClient.Register(request);
            if (!result.Success)
            {
                ViewBag.Errors = result.Errors;
                return View();
            }
            Response.Cookies.Append("jwt", result.Data);
            Response.Cookies.Append("username", request.UserName);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            Response.Cookies.Delete("username");
            return RedirectToAction("Index", "Home");
        }
    }
}
