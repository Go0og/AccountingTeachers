using Contracts.BindingContract;
using Contracts.ViewContract;
using DataModel.enums;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using WebTeacherClient.Models;

namespace WebTeacherClient.Controllers
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


        public IActionResult Accounting()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllAccountingData()
        {
            var data = APIClient.GetRequest<List<DepartmentTeacherView>>($"api/main/get_depatrment_teacher");
            return Json(data);
        }

        [HttpGet]
        public IActionResult OrderTeacher()
        {
            return View(APIClient.GetRequest<List<OrderView>>($"api/main/get_orders_all"));
        }

        [HttpGet]
        public IActionResult OrderTeacherCreate(int? Note_id)
        {
            ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>($"api/main/get_full_teacher");
            ViewBag.departments = APIClient.GetRequest<List<DepartmentView>>($"api/main/get_full_department");
            if (Note_id != null) 
            {
                var Order = APIClient.GetRequest<OrderView>($"api/main/get_order?id={Note_id}");
                var Teacher = APIClient.GetRequest<TeacherView>($"api/main/get_teacher?id={Order.TeacherID}");
                ViewBag.teacher = Teacher;
                ViewBag.order = Order;
            }

            return View();
        }

        [HttpPost]
        public IActionResult OrderTeacherCreate(TypeOrders typeorders,int department, int teacher, string datestart, string dateend, int bet,string positionteacher, string action)
        {
            if (typeorders.ToString() == string.Empty)
            {
                ViewBag.ErrorMessage("not type orders");
                ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>($"api/main/get_full_teacher");
                ViewBag.departments = APIClient.GetRequest<List<DepartmentView>>($"api/main/get_full_department");
                return View();
            }
            if (department < 1)
            {
                ViewBag.ErrorMessage("not pic department");
                ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>($"api/main/get_full_teacher");
                ViewBag.departments = APIClient.GetRequest<List<DepartmentView>>($"api/main/get_full_department");
                return View();
            }
            if (teacher.ToString() == string.Empty)
            {
                ViewBag.ErrorMessage("not pic teacher");
                ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>($"api/main/get_full_teacher");
                ViewBag.departments = APIClient.GetRequest<List<DepartmentView>>($"api/main/get_full_department");
                return View();
            }
            if(positionteacher == string.Empty)
            {
                ViewBag.ErrorMessage("not pic position teacher");
                ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>($"api/main/get_full_teacher");
                ViewBag.departments = APIClient.GetRequest<List<DepartmentView>>($"api/main/get_full_department");
                return View();
            }
            if (Convert.ToDateTime(datestart) < Convert.ToDateTime("01.01.2000") || Convert.ToDateTime(dateend) < Convert.ToDateTime("01.01.2000")
                    || Convert.ToDateTime(datestart) > Convert.ToDateTime("01.01.3000") || Convert.ToDateTime(dateend) > Convert.ToDateTime("01.01.3000"))
            {
                ViewBag.ErrorMessage = "Bad datetime";
                ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>($"api/main/get_full_teacher");
                ViewBag.departments = APIClient.GetRequest<List<DepartmentView>>($"api/main/get_full_department");
                return View();
            }
            if (bet <= 0 || bet > 7000)
            {
                ViewBag.ErrorMessage = "Bad bet";
                ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>($"api/main/get_full_teacher");
                ViewBag.departments = APIClient.GetRequest<List<DepartmentView>>($"api/main/get_full_department");
                return View();
            }
            if (action == "Сохранить")
            {
                APIClient.PostRequest("api/main/save_order", new OrderBindignModel
                {
                    TeacherID = teacher,
                    DateOrder = DateTime.Now,
                    TypeOrder = typeorders,
                });
                if (typeorders == TypeOrders.Swap)
                {
                    APIClient.PostRequest("api/main/update_teacher", new TeacherBindingModel
                    {
                        Id = teacher,
                        DepartmentId = department,
                        PositionTeacher = Enum.Parse<PositionTeacher>(positionteacher),
                        DateSwap = Convert.ToDateTime(datestart),
                        DateEnd = Convert.ToDateTime(dateend),
                        bet = bet,
                    });
                }
                else if (typeorders == TypeOrders.Hiring)
                {
                    APIClient.PostRequest("api/main/update_teacher", new TeacherBindingModel
                    {
                        Id = teacher,
                        DepartmentId = department,
                        PositionTeacher = Enum.Parse<PositionTeacher>(positionteacher),
                        DateSwap = Convert.ToDateTime(datestart),
                        DateEnd = Convert.ToDateTime(dateend),
                        bet = bet,

                    });
                }
            }
            OrderTeacher();
            return View("OrderTeacher");
        }






    }
}
