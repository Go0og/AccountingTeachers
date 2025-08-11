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
            Dictionary<int, string> NameTeacher = new();
            var teachers = APIClient.GetRequest<List<TeacherView>>($"api/main/get_full_teacher");
            foreach(var teacher in teachers)
            {
                NameTeacher.Add(teacher.Id, teacher.FIO);
            }
            ViewBag.Name = NameTeacher;
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
        public IActionResult OrderTeacherCreate(
       TypeOrders typeorders,
       int department,
       int newDepartment,
       int teacher,
       string datestart,
       string dateend,
       string dateswap,
       int bet,
       string positionteacher,
       string action)
        {
            // Загружаем данные для ViewBag (на случай возврата с ошибкой)
            ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>("api/main/get_full_teacher");
            ViewBag.departments = APIClient.GetRequest<List<DepartmentView>>("api/main/get_full_department");

            // Валидация типа приказа
            if (typeorders == TypeOrders.No_type)
            {
                ViewBag.ErrorMessage = "Не выбран тип приказа";
                return View();
            }

            // Валидация преподавателя
            if (teacher <= 0)
            {
                ViewBag.ErrorMessage = "Не выбран преподаватель";
                return View();
            }

            // Валидация в зависимости от типа приказа
            switch (typeorders)
            {
                case TypeOrders.Hiring:
                    if (department <= 0)
                    {
                        ViewBag.ErrorMessage = "Для найма не выбрана кафедра";
                        return View();
                    }
                    if (string.IsNullOrEmpty(positionteacher))
                    {
                        ViewBag.ErrorMessage = "Для найма не указана позиция преподавателя";
                        return View();
                    }
                    if (!DateTime.TryParse(datestart, out var hireStartDate) || !DateTime.TryParse(dateend, out var hireEndDate) ||
                        hireStartDate < new DateTime(2000, 1, 1) || hireEndDate < new DateTime(2000, 1, 1) ||
                        hireStartDate > new DateTime(3000, 1, 1) || hireEndDate > new DateTime(3000, 1, 1))
                    {
                        ViewBag.ErrorMessage = "Некорректные даты для найма";
                        return View();
                    }
                    if (bet <= 0 || bet > 7000)
                    {
                        ViewBag.ErrorMessage = "Некорректная ставка оплаты для найма";
                        return View();
                    }
                    break;

                case TypeOrders.Swap:
                    if (newDepartment <= 0)
                    {
                        ViewBag.ErrorMessage = "Для перевода не выбрана новая кафедра";
                        return View();
                    }
                    if (string.IsNullOrEmpty(positionteacher))
                    {
                        ViewBag.ErrorMessage = "Для перевода не указана позиция преподавателя";
                        return View();
                    }
                    if (!DateTime.TryParse(dateswap, out var swapDate) || !DateTime.TryParse(dateend, out var swapEndDate) ||
                        swapDate < new DateTime(2000, 1, 1) || swapEndDate < new DateTime(2000, 1, 1) ||
                        swapDate > new DateTime(3000, 1, 1) || swapEndDate > new DateTime(3000, 1, 1))
                    {
                        ViewBag.ErrorMessage = "Некорректные даты для перевода";
                        return View();
                    }
                    if (bet <= 0 || bet > 7000)
                    {
                        ViewBag.ErrorMessage = "Некорректная ставка оплаты для перевода";
                        return View();
                    }
                    break;

                case TypeOrders.Firing:
                    // Для увольнения требуется только преподаватель
                    break;
            }

            // Если нажата кнопка "Сохранить"
            if (action == "Сохранить")
            {
                try
                {
                    // Создаем приказ
                    APIClient.PostRequest("api/main/save_order", new OrderBindignModel
                    {
                        TeacherID = teacher,
                        DateOrder = DateTime.Now,
                        TypeOrder = typeorders,
                    });

                    // Обновляем данные преподавателя в зависимости от типа приказа
                    switch (typeorders)
                    {
                        case TypeOrders.Hiring:
                            APIClient.PostRequest("api/main/update_teacher", new TeacherBindingModel
                            {
                                Id = teacher,
                                DepartmentId = department,
                                PositionTeacher = Enum.Parse<PositionTeacher>(positionteacher),
                                DateStart = DateTime.Parse(datestart),
                                DateEnd = DateTime.Parse(dateend),
                                bet = bet
                            });
                            break;

                        case TypeOrders.Swap:
                            APIClient.PostRequest("api/main/update_teacher", new TeacherBindingModel
                            {
                                Id = teacher,
                                DepartmentId = newDepartment,
                                PositionTeacher = Enum.Parse<PositionTeacher>(positionteacher),
                                DateSwap = DateTime.Parse(dateswap),
                                DateEnd = DateTime.Parse(dateend),
                                bet = bet
                            });
                            break;

                        case TypeOrders.Firing:
                            APIClient.PostRequest("api/main/update_teacher", new TeacherBindingModel
                            {
                                Id = teacher,
                                DateEnd = DateTime.Now
                            });
                            break;
                    }

                    // Перенаправляем на страницу списка приказов
                    return RedirectToAction("OrderTeacher");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Ошибка при сохранении: {ex.Message}";
                    return View();
                }
            }

            // Если действие не распознано, возвращаем на форму
            return View();
        }



    }
}
