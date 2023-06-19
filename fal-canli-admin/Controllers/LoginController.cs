using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using fal_canli_admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fal_canli_admin.Controllers
{
    public class LoginController : Controller
    {
        private string baseUrl;
        private readonly ILogger<LoginController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginController(ILogger<LoginController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            var settings = configuration.GetSection("Settings");
            this.baseUrl = settings["ApiUrl"];

            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection collection)
        {
            var email = collection["email"];
            var password = collection["password"];

            #region Login

            var url = "Login";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Post);

            var modelToJson = "{\"email\": \"" + email + "\", \"password\": \"" + password + "\"}";
            request.AddJsonBody(modelToJson);

            var responseAll = client.Post<ResultModel>(request);


            var token = responseAll.Result.ToString();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var userid = tokenS.Claims.First(claim => claim.Type == "userid").Value;

            #endregion


            #region userIsAdmin

            var url2 = "users/userIsAdmin/"+ userid;
            var client2 = new RestClient(baseUrl);
            var request2 = new RestRequest(url2, Method.Get);
            request2.AddHeader("Authorization", "bearer " + token);

            var responseAll2 = client2.Get<ResultModel>(request2);
            var userIsAdmin = Convert.ToBoolean(responseAll2.Result.ToString());

            #endregion


            if (userIsAdmin)
            {
                HttpContext.Session.SetString("Token", token.ToString());
                HttpContext.Session.SetString("UserId", userid);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("UserId");

            return RedirectToAction("Index", "Login");
        }
    }
}
