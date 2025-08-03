using Contracts.BindingContract;
using Contracts.ViewContract;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using WebClietn.Models;

namespace WebClietn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enter(string Login, string password)
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Заполните все поля (логин и пароль)";
                return View();
            }
            APIClient._user = APIClient.GetRequest<UserView>($"api/main/login_user?login={Login}&password={password}");
            if (APIClient._user == null)
            {
                ViewBag.ErrorMessage = "Неверный логин или пароль";
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string login, string password, string fio)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fio))
            {
                ViewBag.ErrorMessage = "Заполните все поля (логин, пароль или фио)";
                return View();
            }

            APIClient.PostRequest("api/main/regiser_user", new UserBindingModel
            {
                Email = login,
                Name = fio,
                Password = password,
            });

            return RedirectToAction("Enter");
        }























    }
}
