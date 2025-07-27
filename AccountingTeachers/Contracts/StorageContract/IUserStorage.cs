using Contracts.SearchContract;
using Contracts.StorageContract.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StorageContract
{
    public interface IUserStorage
    {
        public List<User> GetFullList();
        public List<User> GetFillteredList(UserSearch SearchModel);
        public User? GetUser(UserSearch SearchModel);
    }
}
