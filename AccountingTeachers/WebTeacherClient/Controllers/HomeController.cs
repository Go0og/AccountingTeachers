using Contracts.BindingContract;
using Contracts.ViewContract;
using DataModel.enums;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;
using WebTeacherClient.Models;
using static System.Net.Mime.MediaTypeNames;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

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
        public IActionResult OrderTeacherCreateHiring(int? Note_id)
        {
            ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>($"api/main/get_full_teacher");
            var a = APIClient.GetRequest<List<DepartmentView>>($"api/main/get_full_department");
            ViewBag.departments = a;
            if (Note_id != null)
            {
                var Order = APIClient.GetRequest<OrderView>($"api/main/get_order?id={Note_id}");
                var Teacher = APIClient.GetRequest<TeacherView>($"api/main/get_teacher?id={Order.TeacherID}");
                ViewBag.teacher = Teacher;
                ViewBag.order = Order;
                var tmp = a.FirstOrDefault(x => x.Id == Teacher.DepartmentId);
                ViewBag.department_name = tmp.Name;
            }

            return View();
        }
        [HttpGet]
        public IActionResult OrderTeacherCreateSwap(int? Note_id)
        {
            ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>($"api/main/get_full_teacher");
            var a = APIClient.GetRequest<List<DepartmentView>>($"api/main/get_full_department");
            ViewBag.departments = a;
            if (Note_id != null)
            {
                var Order = APIClient.GetRequest<OrderView>($"api/main/get_order?id={Note_id}");
                var Teacher = APIClient.GetRequest<TeacherView>($"api/main/get_teacher?id={Order.TeacherID}");
                ViewBag.teacher = Teacher;
                ViewBag.order = Order;
                var tmp = a.FirstOrDefault(x => x.Id == Teacher.DepartmentId);
                ViewBag.department_name = tmp.Name;
            }

            return View();
        }
        [HttpGet]
        public IActionResult OrderTeacherCreateFirring(int? Note_id)
        {
            ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>($"api/main/get_full_teacher");
            var a = APIClient.GetRequest<List<DepartmentView>>($"api/main/get_full_department");
            ViewBag.departments = a;
            if (Note_id != null)
            {
                var Order = APIClient.GetRequest<OrderView>($"api/main/get_order?id={Note_id}");
                var Teacher = APIClient.GetRequest<TeacherView>($"api/main/get_teacher?id={Order.TeacherID}");
                ViewBag.teacher = Teacher;
                ViewBag.order = Order;
                var tmp = a.FirstOrDefault(x => x.Id == Teacher.DepartmentId);
                ViewBag.department_name = tmp.Name;
            }

            return View();
        }
        //              По хорошему переписать набор аргументов в заполненный словарь для адаптивности, но пока костыли держат потолок
        private bool CheckCreateTeacher(int? department, int? teacher, string? positionteacher, TypeOrders typeorders,int? newDepartment,
                                                    string? dateswap,string? datestart, string? dateend, int? bet, string? action)
        {
            if (typeorders == TypeOrders.No_type)
            {
                ViewBag.ErrorMessage = "Не выбран тип приказа";
            }

            if (teacher <= 0)
            {
                ViewBag.ErrorMessage = "Не выбран преподаватель";

            }
            if (bet.HasValue && (bet <= 0 || bet > 7000))
            {
                ViewBag.ErrorMessage = "Некорректная ставка оплаты для перевода";
                return false;
            }
            if (typeorders != TypeOrders.Firing && string.IsNullOrEmpty(positionteacher))
            {
                ViewBag.ErrorMessage = "Не указана позиция преподавателя";
                return false;
            }
            if (  typeorders == TypeOrders.Hiring  &&(!DateTime.TryParse(datestart, out var hireStartDate) || !DateTime.TryParse(dateend, out var hireEndDate) ||
                       hireStartDate < new DateTime(2000, 1, 1) || hireEndDate < new DateTime(2000, 1, 1) ||
                       hireStartDate > new DateTime(3000, 1, 1) || hireEndDate > new DateTime(3000, 1, 1)))
            {
                ViewBag.ErrorMessage = "Некорректные даты для найма";
                return false;
            }
            if (newDepartment.HasValue && newDepartment <= 0)
            {
                ViewBag.ErrorMessage = "Для перевода не выбрана новая кафедра";
                return false;
            }

            if (typeorders == TypeOrders.Swap && (!DateTime.TryParse(dateswap, out var swapDate) || !DateTime.TryParse(dateend, out var swapEndDate) ||
                        swapDate < new DateTime(2000, 1, 1) || swapEndDate < new DateTime(2000, 1, 1) ||
                        swapDate > new DateTime(3000, 1, 1) || swapEndDate > new DateTime(3000, 1, 1)))
            {
                ViewBag.ErrorMessage = "Некорректные даты для перевода";
                return false;
            }
            return true;
        }
        
        [HttpPost]
        public IActionResult OrderTeacherCreateHiring(int department, int teacher, string positionteacher, TypeOrders typeorders,
                                                        string datestart,string dateend,int bet,string action)
        {

            ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>("api/main/get_full_teacher");
            ViewBag.departments = APIClient.GetRequest<List<DepartmentView>>("api/main/get_full_department");
            if (!CheckCreateTeacher(department, teacher, positionteacher, TypeOrders.Hiring, null,null,datestart,dateend,bet,action))
            {
               
                return View();
            }

            APIClient.PostRequest("api/main/save_order", new OrderBindignModel
            {
                TeacherID = teacher,
                DateOrder = DateTime.Now,
                TypeOrder = TypeOrders.Hiring,
            });
            APIClient.PostRequest("api/main/update_teacher", new TeacherBindingModel
            {
                Id = teacher,
                DepartmentId = department,
                PositionTeacher = Enum.Parse<PositionTeacher>(positionteacher),
                DateStart = DateTime.Parse(datestart),
                DateEnd = DateTime.Parse(dateend),
                bet = bet
            });
            return View();
        }
        [HttpPost]
        public IActionResult OrderTeacherCreateSwap(int department,int NewDepartment, int teacher, string positionteacher, TypeOrders typeorders,
                                                       string dateswap, string dateend, int bet, string action)
        {

            ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>("api/main/get_full_teacher");
            ViewBag.departments = APIClient.GetRequest<List<DepartmentView>>("api/main/get_full_department");
            if (!CheckCreateTeacher(department, teacher, positionteacher, TypeOrders.Swap,NewDepartment, dateswap, null, dateend, bet, action))
            {
                return View();
            }

            APIClient.PostRequest("api/main/save_order", new OrderBindignModel
            {
                TeacherID = teacher,
                DateOrder = DateTime.Now,
                TypeOrder = TypeOrders.Swap,
            });
            APIClient.PostRequest("api/main/update_teacher", new TeacherBindingModel
            {
                Id = teacher,
                DepartmentId = department,
                PositionTeacher = Enum.Parse<PositionTeacher>(positionteacher),
                DateSwap = DateTime.Parse(dateswap),
                DateEnd = DateTime.Parse(dateend),
                bet = bet
            });
            return View();
        }
        [HttpPost]
        public IActionResult OrderTeacherCreateFirring(int department, int teacher, string positionteacher, TypeOrders typeorders,
                                                       string datestart, string dateend, int bet, string action)
        {

            ViewBag.teachers = APIClient.GetRequest<List<TeacherView>>("api/main/get_full_teacher");
            ViewBag.departments = APIClient.GetRequest<List<DepartmentView>>("api/main/get_full_department");
            if (!CheckCreateTeacher(department, teacher, null, TypeOrders.Firing, null, null, null, dateend, null, action))
            {
                return View();
            }

            APIClient.PostRequest("api/main/save_order", new OrderBindignModel
            {
                TeacherID = teacher,
                DateOrder = DateTime.Now,
                TypeOrder = TypeOrders.Firing,
            });
            APIClient.PostRequest("api/main/update_teacher", new TeacherBindingModel
            {
                Id = teacher,
            });
            return View();
        }



        [HttpGet]
        public IActionResult generateWordReport(int Id, TypeOrders order)
        {
            var fileMemStream = APIClient.GetRequest<byte[]>($"api/main/general_hiring?Id={Id}&order_type={order.ToString()}");
            return File(fileMemStream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Report.docx");
        }
        
        [HttpGet]
        public IActionResult generateWordReportAccounting(string data)
        {
            // Декодируем данные из base64
            var decodedData = Encoding.UTF8.GetString(Convert.FromBase64String(data));

            // Десериализуем в список объектов
            List<DepartmentTeacherView> teachers = JsonConvert.DeserializeObject<List<DepartmentTeacherView>>(decodedData);
            var fileMemStream = APIClient.GetRequest<byte[]>($"api/main/general_accounting?list={teachers}"); 
            return File(fileMemStream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Report.docx");
        }
        /*
        [HttpGet]
        public IActionResult generateWordReportAccounting(string data)
        {
            try
            {
                // Декодируем данные из base64
                var decodedData = Encoding.UTF8.GetString(Convert.FromBase64String(data));

                // Десериализуем в список объектов
                var teachers = JsonConvert.DeserializeObject<List<DepartmentTeacherView>>(decodedData);

                // Проверяем данные
                if (teachers == null || !teachers.Any())
                    return BadRequest("Нет данных для отчета");

                // Правильный вызов API с сериализованными данными
                var serializedTeachers = Uri.EscapeDataString(JsonConvert.SerializeObject(teachers));
                var fileBytes = APIClient.GetRequest<byte[]>($"api/main/general_accounting?teachers={serializedTeachers}");

                return File(fileBytes,
                           "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                           "Учетность_преподавателей.docx");
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка при генерации отчета: {ex}");
                return StatusCode(500, $"Ошибка генерации: {ex.Message}");
            }
        
        }
        */
    }
}
