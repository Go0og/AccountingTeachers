using Contracts.BindingContract;
using Contracts.SearchContract;
using Contracts.StorageContract;
using Contracts.StorageContract.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseImplements.Implements
{
    public class UserStorage : IUserStorage
    {
        public bool Create(UserBindingModel user)
        {
            var NewUser = User.Create(user);
            if (NewUser == null)
            {
                return false;
            }
            using var context = new DataBaseImplement();
            context.Users.Add(NewUser);
            context.SaveChanges();
            return true;
        }

        public bool Update(UserBindingModel user)
        {
            using var context = new DataBaseImplement();
            var UpdateUser = context.Users.FirstOrDefault(x => x.Id == user.Id);
            if (UpdateUser == null)
            {
                return false;
            }
            UpdateUser.Update(user);
            context.SaveChanges();
            return true;
        }


        public bool Delete(UserBindingModel user)
        {
            using var context = new DataBaseImplement();
            var DeleteUser = context.Users.FirstOrDefault(x=>x.Id == user.Id);
            if (DeleteUser == null)
            {
                return false;
            }
            context.Users.Remove(DeleteUser);
            context.SaveChanges();

            return true;

        }

        public List<User> GetFillteredList(UserSearch SearchModel)
        {
            using var context = new DataBaseImplement();
            if (SearchModel.Id.HasValue)
            {
                return context.Users
                    .Where(x => x.Id == SearchModel.Id)
                    .ToList();
            }
            return new();
        }

        public List<User> GetFullList()
        {
            using var context = new DataBaseImplement();
            return context.Users.ToList();
        }

        public User? GetUser(UserSearch SearchModel)
        {
            using var context = new DataBaseImplement();
            if (SearchModel.Id.HasValue)
            {
                return context.Users
                    .FirstOrDefault(x => x.Id == SearchModel.Id);
            }
            if (!string.IsNullOrEmpty(SearchModel.Name))
            {
                return context.Users
                    .FirstOrDefault(x => x.Name == SearchModel.Name);
            }
            if (!string.IsNullOrEmpty(SearchModel.Email) && !string.IsNullOrEmpty(SearchModel.Password))
            {
                return context.Users
                    .FirstOrDefault(x => x.Email == SearchModel.Email && x.Password == SearchModel.Password);
            }
            return null;
        }

    }
}
