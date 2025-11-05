using Contracts.BindingContract;
using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StorageContract.dbModels
{
    public class User : IUser
    {

        public int Id { get; set; }
        public string Name {get;set;} = string.Empty;

        public string Email {get;set;} = string.Empty ;

        public string Password {get;set;} = string.Empty ;

        public static User? Create (UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new User()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password

            };
        }

        public void Update (UserBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            Name = model.Name;
            Email = model.Email;
            Password = model.Password;
        }
    }
}
