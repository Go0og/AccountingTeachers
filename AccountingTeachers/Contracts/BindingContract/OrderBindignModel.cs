using DataModel.enums;
using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BindingContract
{
    public class OrderBindignModel : IOrders
    {
        public int Id { get; set; }
        public TypeOrders TypeOrder {  get; set; } = TypeOrders.No_type;

        public int TeacherID {get; set; }

        public DateTime DateOrder {  get; set; }

       
    }
}
