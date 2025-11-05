using Contracts.BindingContract;
using Contracts.SearchContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.InteractorContract
{
    public interface IUserLogic
    {
        public List<UserBindingModel> GetList(UserSearch? searchModel);
        public UserBindingModel GetUser(UserSearch SearchModel);
        public bool CreateUser(UserBindingModel UserBindingModel);
        public bool UpdateUser(UserBindingModel UserBindingModel);
        public bool DeleteUser(UserBindingModel UserBindingModel);
        public void CheckModel(UserBindingModel bindingModel, bool obDel, bool onUp);
    }
}
