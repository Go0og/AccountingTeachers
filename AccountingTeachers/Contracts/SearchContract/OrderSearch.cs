using DataModel.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.SearchContract
{
    public class OrderSearch
    {
        public int? Id { get; set; }
        public TypeOrders? TypeOrders { get; set; }
        public int? TeacherID {  get; set; }
        public DateTime? DateOrders { get; set; }
    }
}
