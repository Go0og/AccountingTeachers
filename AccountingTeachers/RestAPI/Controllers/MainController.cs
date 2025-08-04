using Contracts.BindingContract;
using Contracts.InteractorContract;
using Contracts.PresenterContract;
using Contracts.SearchContract;
using Contracts.StorageContract;
using Contracts.ViewContract;
using Interactors;
using Microsoft.AspNetCore.Mvc;

namespace RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IUserPresenter userPresenter;
        private readonly ITeacherPresenter teacherPresenter;
        private readonly IDepartmentPresenter departmentPresenter;

        private readonly IUserLogic userLogic;
        private readonly IteacherLogic teacherLogic;
        private readonly IDepartmentLogic departmentLogic;

        private readonly IUserStorage userStorage;
        private readonly ITeacherStorage teacherStorage;
        private readonly IDepartmentStorage departmentStorage;

        public MainController(IUserPresenter UserPresenter, ITeacherPresenter TeacherPresenter, IDepartmentPresenter DepartmentPresenter,
                                IUserLogic UserLogic, IteacherLogic TeacherLogic, IDepartmentLogic DepartmentLogic,
                                IUserStorage UserStorage, ITeacherStorage TeacherStorage, IDepartmentStorage DepartmentStorage)
        {
            userPresenter = UserPresenter;
            teacherPresenter = TeacherPresenter;
            departmentPresenter = DepartmentPresenter;

            userLogic = UserLogic;
            teacherLogic = TeacherLogic;
            departmentLogic = DepartmentLogic;

            userStorage = UserStorage;
            teacherStorage = TeacherStorage;
            departmentStorage = DepartmentStorage;
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


    }
}

