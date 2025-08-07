using DataModel.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public interface IOrders : Iid
    {
        TypeOrders TypeOrder { get; }
        int TeacherID { get; }
        DateTime DateOrder { get; }
    }
}
