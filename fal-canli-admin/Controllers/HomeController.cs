using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using fal_canli_admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using fal_canli_admin.Middlewares;
using Microsoft.IdentityModel.Tokens;
using System;

namespace fal_canli_admin.Controllers
{
    [AuthorizationMiddleware]
    public class HomeController : Controller
    {
        private string baseUrl;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
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


        #region UserList

        public IActionResult UserList()
        {
            var models = GetUsers();

            return View(models);
        }

        public IActionResult UserAddOrUpdate(string id)
        {
            ViewBag.ObjectId = id;

            UserModelForAddUpdate model = null;

            if (!id.IsNullOrEmpty())
                model = GetUser(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult UserAddOrUpdate(IFormCollection collection)
        {
            #region controls

            var objectId = collection["objectId"];

            var name = collection["name"];
            var lastname = collection["lastname"];
            var password = collection["password"];
            var passwordAgain = collection["passwordAgain"];
            var email = collection["email"];
            var birthday_day = collection["birthday_day"];
            var birthday_month = collection["birthday_month"];
            var birthday_year = collection["birthday_year"];
            var gender = collection["gender"];
            var isAdmin = collection["isAdmin"];
            var isDeported = collection["isDeported"];

            if (name.IsNullOrEmpty() ||
                lastname.IsNullOrEmpty() ||
                password.IsNullOrEmpty() ||
                passwordAgain.IsNullOrEmpty() ||
                email.IsNullOrEmpty() ||
                birthday_day.IsNullOrEmpty() ||
                birthday_month.IsNullOrEmpty() ||
                birthday_year.IsNullOrEmpty() ||
                gender.IsNullOrEmpty() ||
                isAdmin.IsNullOrEmpty() ||
                isDeported.IsNullOrEmpty())
            {
                return View();
            }

            if (password != passwordAgain)
            {
                return View();
            }

            #endregion

            #region prepare

            var birthday_day_number = Convert.ToInt32(birthday_day);
            var birthday_month_number = Convert.ToInt32(birthday_month);
            var birthday_year_number = Convert.ToInt32(birthday_year);
            var birthday = new DateTime(birthday_year_number, birthday_month_number, birthday_day_number);

            var isAdmin_bool = isAdmin == "1" ? true : false;
            var isDeported_bool = isDeported == "1" ? true : false;


            UserModelForAddUpdate userModelForAddUpdate = new UserModelForAddUpdate();
            userModelForAddUpdate.name = name;
            userModelForAddUpdate.lastname = lastname;
            userModelForAddUpdate.password = password;
            userModelForAddUpdate.email = email;
            userModelForAddUpdate.birthday = birthday.ToShortDateString();
            userModelForAddUpdate.gender = gender;
            userModelForAddUpdate.isAdmin = isAdmin_bool;
            userModelForAddUpdate.isDeported = isDeported_bool;

            #endregion

            if (objectId.ToString().IsNullOrEmpty())
            {
                AddUser(userModelForAddUpdate);
            }
            else
            {
                UpdateUser(objectId, userModelForAddUpdate);
            }

            return RedirectToAction("UserList", "Home");
        }

        public IActionResult UserDelete(string id)
        {
            DeleteUser(id);

            return RedirectToAction("UserList", "Home");
        }

        #endregion

        #region FortuneTeller

        public IActionResult FortuneTeller()
        {
            var models = GetFortuneTellers();
            var users = GetUsers();
            for (int i = 0; i < models.Count; i++)
            {
                models[i].userModel = users.Find(j => j._id == models[i].userid);
            }

            return View(models);
        }

        public IActionResult FortuneTellerAddOrUpdate(string id)
        {
            ViewBag.ObjectId = id;

            var users = GetUsers();
            ViewBag.Users = users;

            FortuneTellerModelForAddUpdate model = null;

            if (!id.IsNullOrEmpty())
                model = GetFortuneTeller(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult FortuneTellerAddOrUpdate(IFormCollection collection)
        {
            var users = GetUsers();
            ViewBag.Users = users;

            #region controls

            var objectId = collection["objectId"];


            var userid = collection["userid"];
            var nickname = collection["nickname"];
            var about = collection["about"];
            var fortuneTellerStatus = collection["fortuneTellerStatus"];
            var channelId = collection["channelId"];
            var hasTarot = collection["hasTarot"];
            var tarotServiceFee = collection["tarotServiceFee"];
            var hasCoffee = collection["hasCoffee"];
            var coffeeServiceFee = collection["coffeeServiceFee"];
            var hasBirthChart = collection["hasBirthChart"];
            var birthChartServiceFee = collection["birthChartServiceFee"];
            var hasAstrology = collection["hasAstrology"];
            var astrologyServiceFee = collection["astrologyServiceFee"];

            if (userid.IsNullOrEmpty() ||
                nickname.IsNullOrEmpty() ||
                about.IsNullOrEmpty() ||
                fortuneTellerStatus.IsNullOrEmpty() ||
                channelId.IsNullOrEmpty() ||
                hasTarot.IsNullOrEmpty() ||
                tarotServiceFee.IsNullOrEmpty() ||
                hasCoffee.IsNullOrEmpty() ||
                coffeeServiceFee.IsNullOrEmpty() ||
                hasBirthChart.IsNullOrEmpty() ||
                birthChartServiceFee.IsNullOrEmpty() ||
                hasAstrology.IsNullOrEmpty() ||
                astrologyServiceFee.IsNullOrEmpty())
            {
                return View();
            }

            #endregion

            #region prepare

            var hasTarot_bool = hasTarot == "1" ? true : false;
            var hasCoffee_bool = hasCoffee == "1" ? true : false;
            var hasBirthChart_bool = hasBirthChart == "1" ? true : false;
            var hasAstrology_bool = hasAstrology == "1" ? true : false;

            var tarotServiceFee_double = Convert.ToDouble(tarotServiceFee);
            var coffeeServiceFee_double = Convert.ToDouble(coffeeServiceFee);
            var birthChartServiceFee_double = Convert.ToDouble(birthChartServiceFee);
            var astrologyServiceFee_double = Convert.ToDouble(astrologyServiceFee);


            FortuneTellerModelForAddUpdate model = new FortuneTellerModelForAddUpdate();
            model.userid = userid;
            model.nickname = nickname;
            model.about = about;
            model.fortuneTellerStatus = fortuneTellerStatus;
            model.channelId = channelId;
            model.hasTarot = hasTarot_bool;
            model.tarotServiceFee = tarotServiceFee_double;
            model.hasCoffee = hasCoffee_bool;
            model.coffeeServiceFee = coffeeServiceFee_double;
            model.hasBirthChart = hasBirthChart_bool;
            model.birthChartServiceFee = birthChartServiceFee_double;
            model.hasAstrology = hasAstrology_bool;
            model.astrologyServiceFee = astrologyServiceFee_double;

            #endregion

            if (objectId.ToString().IsNullOrEmpty())
            {
                AddFortuneTeller(model);
            }
            else
            {
                UpdateFortuneTeller(objectId, model);
            }

            return RedirectToAction("FortuneTeller", "Home");
        }

        public IActionResult FortuneTellerDelete(string id)
        {
            DeleteFortuneTeller(id);

            return RedirectToAction("FortuneTeller", "Home");
        }

        #endregion

        #region DailyZodiacSign

        public IActionResult DailyZodiacSign()
        {
            var models = GetDailyZodiacSigns();

            return View(models);
        }

        public IActionResult DailyZodiacSignAddOrUpdate(string id)
        {
            ViewBag.ObjectId = id;

            DailyZodiacSignModelForAddUpdate model = null;

            if (!id.IsNullOrEmpty())
                model = GetDailyZodiacSign(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult DailyZodiacSignAddOrUpdate(IFormCollection collection)
        {
            #region controls

            var objectId = collection["objectId"];

            var type = collection["type"];
            var date_day = collection["date_day"];
            var date_month = collection["date_month"];
            var date_year = collection["date_year"];
            var title = collection["title"];
            var explanation = collection["explanation"];

            if (type.IsNullOrEmpty() ||
                date_day.IsNullOrEmpty() ||
                date_month.IsNullOrEmpty() ||
                date_year.IsNullOrEmpty() ||
                title.IsNullOrEmpty() ||
                explanation.IsNullOrEmpty())
            {
                return View();
            }

            #endregion

            #region prepare

            var date_day_number = Convert.ToInt32(date_day);
            var date_month_number = Convert.ToInt32(date_month);
            var date_year_number = Convert.ToInt32(date_year);
            var date = new DateTime(date_year_number, date_month_number, date_day_number);


            DailyZodiacSignModelForAddUpdate model = new DailyZodiacSignModelForAddUpdate();
            model.type = type;
            model.date = date.ToShortDateString();
            model.title = title;
            model.explanation = explanation;

            #endregion

            if (objectId.ToString().IsNullOrEmpty())
            {
                AddDailyZodiacSign(model);
            }
            else
            {
                UpdateDailyZodiacSign(objectId, model);
            }

            return RedirectToAction("DailyZodiacSign", "Home");
        }

        public IActionResult DailyZodiacSignDelete(string id)
        {
            DeleteDailyZodiacSign(id);

            return RedirectToAction("DailyZodiacSign", "Home");
        }

        #endregion

        #region UserCredit

        public IActionResult UserCredit()
        {
            var models = GetUserCredits();

            return View(models);
        }

        public IActionResult UserCreditAddOrUpdate(string id)
        {
            ViewBag.ObjectId = id;

            var fortuneTellers = GetFortuneTellers();
            ViewBag.FortuneTellers = fortuneTellers;
            var users = GetUsers();
            ViewBag.Users = users;

            UserCreditModelForAddUpdate model = null;

            if (!id.IsNullOrEmpty())
                model = GetUserCredit(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult UserCreditAddOrUpdate(IFormCollection collection)
        {
            var fortuneTellers = GetFortuneTellers();
            ViewBag.FortuneTellers = fortuneTellers;
            var users = GetUsers();
            ViewBag.Users = users;

            #region controls

            var objectId = collection["objectId"];

            var amount = collection["amount"];
            var userid = collection["userid"];
            var type = collection["type"];
            var fortuneTellerId = collection["fortuneTellerId"];

            if (amount.IsNullOrEmpty() ||
                userid.IsNullOrEmpty() ||
                type.IsNullOrEmpty() ||
                fortuneTellerId.IsNullOrEmpty())
            {
                return View();
            }

            #endregion

            #region prepare

            var amount_double = Convert.ToDouble(amount);


            UserCreditModelForAddUpdate model = new UserCreditModelForAddUpdate();
            model.amount = amount_double;
            model.userid = userid;
            model.type = type;
            model.fortuneTellerId = fortuneTellerId;

            #endregion

            if (objectId.ToString().IsNullOrEmpty())
            {
                AddUserCredit(model);
            }
            else
            {
                UpdateUserCredit(objectId, model);
            }

            return RedirectToAction("UserCredit", "Home");
        }

        public IActionResult UserCreditDelete(string id)
        {
            DeleteUserCredit(id);

            return RedirectToAction("UserCredit", "Home");
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region User

        public List<UserModel> GetUsers()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "Users";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Get<ResultModel>(request);

            var models = new List<UserModel>();
            if (responseAll.Result != null)
            {
                models = JsonConvert.DeserializeObject<List<UserModel>>(responseAll.Result.ToString());
            }

            return models;
        }

        public UserModelForAddUpdate GetUser(string id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "Users/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Get<ResultModel>(request);

            var models = new UserModelForAddUpdate();
            if (responseAll.Result != null)
            {
                models = JsonConvert.DeserializeObject<UserModelForAddUpdate>(responseAll.Result.ToString());
            }

            return models;
        }

        public void AddUser(UserModelForAddUpdate userModelForAddUpdate)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "Users";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Authorization", "bearer " + token);
            request.AddBody(JsonConvert.SerializeObject(userModelForAddUpdate));

            var responseAll = client.Post<ResultModel>(request);
        }

        public void UpdateUser(string id, UserModelForAddUpdate userModelForAddUpdate)
        {
            UserModelWithoutPasswordForAddUpdate userModelWithoutPasswordForAddUpdate = new UserModelWithoutPasswordForAddUpdate();

            if (userModelForAddUpdate.password.IsNullOrEmpty())
            {
                userModelWithoutPasswordForAddUpdate.name = userModelForAddUpdate.name;
                userModelWithoutPasswordForAddUpdate.lastname = userModelForAddUpdate.lastname;
                userModelWithoutPasswordForAddUpdate.email = userModelForAddUpdate.email;
                userModelWithoutPasswordForAddUpdate.birthday = userModelForAddUpdate.birthday;
                userModelWithoutPasswordForAddUpdate.gender = userModelForAddUpdate.gender;
                userModelWithoutPasswordForAddUpdate.isAdmin = userModelForAddUpdate.isAdmin;
                userModelWithoutPasswordForAddUpdate.isDeported = userModelForAddUpdate.isDeported;
            }

            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "Users/"+ id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Patch);
            request.AddHeader("Authorization", "bearer " + token);
            request.AddBody(JsonConvert.SerializeObject(userModelWithoutPasswordForAddUpdate));

            var responseAll = client.Patch<ResultModel>(request);
        }

        public bool DeleteUser(string id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "Users/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Delete<ResultModel>(request);

            if (responseAll.Result != null)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region FortuneTeller

        public List<FortuneTellerModel> GetFortuneTellers()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "fortune-teller";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Get<ResultModel>(request);

            var models = new List<FortuneTellerModel>();
            if (responseAll.Result != null)
            {
                models = JsonConvert.DeserializeObject<List<FortuneTellerModel>>(responseAll.Result.ToString());
            }

            #region userModel

            var userModels = GetUsers();

            foreach (var item in models)
            {
                item.userModel = userModels.Find(i => i._id == item.userid);
            }

            #endregion

            return models;
        }

        public FortuneTellerModelForAddUpdate GetFortuneTeller(string id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "fortune-teller/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Get<ResultModel>(request);

            var model = new FortuneTellerModelForAddUpdate();
            if (responseAll.Result != null)
            {
                model = JsonConvert.DeserializeObject<FortuneTellerModelForAddUpdate>(responseAll.Result.ToString());
            }

            #region userModel

            var userModels = GetUsers();
            model.userModel = userModels.Find(i => i._id == model.userid);

            #endregion

            return model;
        }

        public void AddFortuneTeller(FortuneTellerModelForAddUpdate model)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "fortune-teller";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Authorization", "bearer " + token);
            request.AddBody(JsonConvert.SerializeObject(model));

            var responseAll = client.Post<ResultModel>(request);
        }

        public void UpdateFortuneTeller(string id, FortuneTellerModelForAddUpdate model)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "fortune-teller/"+ id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Patch);
            request.AddHeader("Authorization", "bearer " + token);
            request.AddBody(JsonConvert.SerializeObject(model));

            var responseAll = client.Patch<ResultModel>(request);
        }

        public bool DeleteFortuneTeller(string id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "fortune-teller/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Delete<ResultModel>(request);

            if (responseAll.Result != null)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region DailyZodiacSign

        public List<DailyZodiacSignModel> GetDailyZodiacSigns()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "daily-zodiac-sign";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Get<ResultModel>(request);

            var models = new List<DailyZodiacSignModel>();
            if (responseAll.Result != null)
            {
                models = JsonConvert.DeserializeObject<List<DailyZodiacSignModel>>(responseAll.Result.ToString());
            }

            return models;
        }

        public DailyZodiacSignModelForAddUpdate GetDailyZodiacSign(string id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "daily-zodiac-sign/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Get<ResultModel>(request);

            var models = new DailyZodiacSignModelForAddUpdate();
            if (responseAll.Result != null)
            {
                models = JsonConvert.DeserializeObject<DailyZodiacSignModelForAddUpdate>(responseAll.Result.ToString());
            }

            return models;
        }

        public void AddDailyZodiacSign(DailyZodiacSignModelForAddUpdate model)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "daily-zodiac-sign";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Authorization", "bearer " + token);
            request.AddBody(JsonConvert.SerializeObject(model));

            var responseAll = client.Post<ResultModel>(request);
        }

        public void UpdateDailyZodiacSign(string id, DailyZodiacSignModelForAddUpdate model)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "daily-zodiac-sign/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Patch);
            request.AddHeader("Authorization", "bearer " + token);
            request.AddBody(JsonConvert.SerializeObject(model));

            var responseAll = client.Patch<ResultModel>(request);
        }

        public bool DeleteDailyZodiacSign(string id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "daily-zodiac-sign/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Delete<ResultModel>(request);

            if (responseAll.Result != null)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region UserCredit

        public List<UserCreditModel> GetUserCredits()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "user-credits";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Get<ResultModel>(request);

            var models = new List<UserCreditModel>();
            if (responseAll.Result != null)
            {
                models = JsonConvert.DeserializeObject<List<UserCreditModel>>(responseAll.Result.ToString());
            }

            #region userModel

            var userModels = GetUsers();

            foreach (var item in models)
            {
                item.userModel = userModels.Find(i => i._id == item.userid);
            }

            #endregion

            #region userModel

            var fortuneTellerModels = GetFortuneTellers();

            foreach (var item in models)
            {
                item.fortuneTellerModel = fortuneTellerModels.Find(i => i._id == item.fortuneTellerId);
            }

            #endregion

            return models;
        }

        public UserCreditModelForAddUpdate GetUserCredit(string id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "user-credits/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Get<ResultModel>(request);

            var model = new UserCreditModelForAddUpdate();
            if (responseAll.Result != null)
            {
                model = JsonConvert.DeserializeObject<UserCreditModelForAddUpdate>(responseAll.Result.ToString());
            }

            #region userModel

            var userModels = GetUsers();
            model.userModel = userModels.Find(i => i._id == model.userid);

            #endregion

            #region userModel

            var fortuneTellerModels = GetFortuneTellers();
            model.fortuneTellerModel = fortuneTellerModels.Find(i => i._id == model.fortuneTellerId);

            #endregion

            return model;
        }

        public void AddUserCredit(UserCreditModelForAddUpdate model)
        {
            UserCreditModelWithoutFortuneTellerForAddUpdate userCreditModelWithoutFortuneTellerForAddUpdate = new UserCreditModelWithoutFortuneTellerForAddUpdate()
            {
                amount = model.amount,
                userid = model.userid,
                type = model.type
            };

            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "user-credits";
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Authorization", "bearer " + token);
            request.AddBody(JsonConvert.SerializeObject(userCreditModelWithoutFortuneTellerForAddUpdate));

            var responseAll = client.Post<ResultModel>(request);
        }

        public void UpdateUserCredit(string id, UserCreditModelForAddUpdate model)
        {
            UserCreditModelWithoutFortuneTellerForAddUpdate userCreditModelWithoutFortuneTellerForAddUpdate = new UserCreditModelWithoutFortuneTellerForAddUpdate()
            {
                amount = model.amount,
                userid = model.userid,
                type = model.type
            };

            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "user-credits/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Patch);
            request.AddHeader("Authorization", "bearer " + token);
            request.AddBody(JsonConvert.SerializeObject(userCreditModelWithoutFortuneTellerForAddUpdate));

            var responseAll = client.Patch<ResultModel>(request);
        }

        public bool DeleteUserCredit(string id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var url = "user-credits/" + id;
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", "bearer " + token);

            var responseAll = client.Delete<ResultModel>(request);

            if (responseAll.Result != null)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
