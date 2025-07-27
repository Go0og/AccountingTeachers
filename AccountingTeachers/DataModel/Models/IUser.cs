using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public interface IUser : Iid
    {
        string Name { get; }
        string Email { get; }
        string Password { get; }
    }
}
