using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ViewContract
{
    public class OrderView
    {
        public int Id { get; set; }
        public int TeacherID { get; set;}
        public string DateOrder { get; set; } = string.Empty;
        public string TypeOrders {  get; set; } = string.Empty;
    }
}
