using Contracts.PresenterContract;
using Contracts.SearchContract;
using Contracts.ViewContract;
using Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenters
{
    public class UserPresenter : IUserPresenter
    {
        private readonly UserLogic _logic;
        public UserPresenter (UserLogic logic)
        {
            _logic = logic;
        }

        public UserView? MakeUser(UserSearch model)
        {
            var models = _logic.GetUser(model);
            if (models == null)
            {
                return null;
            }
            var NewViewModel = new UserView
            {
                Id = (int)model.Id,
                Name = model.Name,
                Password = model.Password,
                Email = model.Email
            };
            return NewViewModel;
        }

        public List<UserView> MakeUserList(UserSearch? model)
        {
            var models = _logic.GetList(model);
            List<UserView> NewUserView = new();
            foreach (var modelItem in models) 
            {
                NewUserView.Add(new UserView
                {
                    Id = (int)modelItem.Id,
                    Name = modelItem.Name,
                    Password = modelItem.Password,
                    Email = modelItem.Email
                });
            }
            return NewUserView;
        }
    }
}
