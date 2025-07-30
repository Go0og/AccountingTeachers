using Contracts.SearchContract;
using Contracts.ViewContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PresenterContract
{
    public interface IUserPresenter
    {
        public List<UserView> MakeUserList(UserSearch? model);
        public UserView MakeUser (UserSearch model);
    }
}
