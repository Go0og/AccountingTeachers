using Contracts.BindingContract;
using Contracts.InteractorContract;
using Contracts.PresenterContract;
using Contracts.SearchContract;
using Contracts.StorageContract;
using Contracts.ViewContract;
using DataModel.enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTeacherClient;

namespace TeacherRestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : Controller
    {
        private readonly IUserPresenter userPresenter;
        private readonly ITeacherPresenter teacherPresenter;
        private readonly IDepartmentPresenter departmentPresenter;
        private readonly IDepartmensTeachersPresenter departmensTeachersPresenter;
        private readonly IOrdersPresenter ordersPresenter;

        private readonly IUserLogic userLogic;
        private readonly IteacherLogic teacherLogic;
        private readonly IDepartmentLogic departmentLogic;
        private readonly IOrdersLogic ordersLogic;

        private readonly IUserStorage userStorage;
        private readonly ITeacherStorage teacherStorage;
        private readonly IDepartmentStorage departmentStorage;
        private readonly IOrderStorage orderStorage;

        private readonly IReportLogic _reportLogic;


        public MainController(IUserPresenter UserPresenter, ITeacherPresenter TeacherPresenter, IDepartmentPresenter DepartmentPresenter,
                                IDepartmensTeachersPresenter DepartmentTeacherPresenter, IOrdersPresenter OrdersPresenter,
                                IUserLogic UserLogic, IteacherLogic TeacherLogic, IDepartmentLogic DepartmentLogic, IOrdersLogic OrdersLogic,
                                IUserStorage UserStorage, ITeacherStorage TeacherStorage, IDepartmentStorage DepartmentStorage, IOrderStorage OrderStorage,
                                IReportLogic reportLogic)
        {
            userPresenter = UserPresenter;
            teacherPresenter = TeacherPresenter;
            departmentPresenter = DepartmentPresenter;
            departmensTeachersPresenter = DepartmentTeacherPresenter;
            ordersPresenter = OrdersPresenter;

            userLogic = UserLogic;
            teacherLogic = TeacherLogic;
            departmentLogic = DepartmentLogic;
            ordersLogic = OrdersLogic;

            userStorage = UserStorage;
            teacherStorage = TeacherStorage;
            departmentStorage = DepartmentStorage;
            orderStorage = OrderStorage;

            _reportLogic = reportLogic;
        }

        [HttpPost]
        public void regiser_user(UserBindingModel model)
        {
            userLogic.CreateUser(model);
        }

        [HttpGet]
        public UserView? login_user(string login, string password)
        {
            return userPresenter.MakeUser(new UserSearch
            {
                Email = login,
                Password = password
            });
        }

        [HttpGet]
        public List<DepartmentTeacherView> get_depatrment_teacher()
        {
            return departmensTeachersPresenter.MakeDepartmentList();
        }


        [HttpGet]
        public List<OrderView> get_orders_all()
        {
            return ordersPresenter.MakeOrderList(null); 
        }

        [HttpGet]
        public List<TeacherView> get_full_teacher()
        {
            return teacherPresenter.MakeTeacherList(null);
        }

        [HttpGet]
        public List<DepartmentView> get_full_department()
        {
            return departmentPresenter.MakeDepartmentList(null);
        }

        [HttpPost]
        public void save_order(OrderBindignModel model)
        {
            if (model.Id >0)
            {
                ordersLogic.UpdateOrder(model);
            }
            else
            {
                ordersLogic.CreateOrder(model);
            }
        }
        [HttpPost]
        public void update_teacher(TeacherBindingModel model)
        {
            teacherLogic.UpdateTeacher(model);
        }
        [HttpGet]
        public OrderView? get_order(int id) { 
            return ordersPresenter.MakeOrder(new OrderSearch { Id = id });
        }
        [HttpGet]
        public TeacherView? get_teacher(int id)
        {
            return teacherPresenter.MakeTeacher(new TeacherSearch { Id = id });
        }

        [HttpGet]
        public byte[]? general_hiring(int Id, string order_type)
        {
            var d = ordersPresenter.MakeOrder(new OrderSearch { Id = Id });
            return _reportLogic.SaveOrderToWordFile(d, Enum.Parse<TypeOrders>(order_type));

        }
        [HttpGet]
        public byte[] general_accounting(string list)
        {
            return _reportLogic.SaveAccountingToWordFile(JsonConvert.DeserializeObject<List<DepartmentTeacherView>>(list));
        }
        
        [HttpGet]
        public bool get_order_hirring_by_teacher(int teacher_id)
        {
            bool result = false;
            var t = ordersPresenter.MakeOrder(new OrderSearch
            {
                TeacherID = teacher_id
            });
            if (t != null) { 
                result = true;
            }
            return result;
        }
    }
}
