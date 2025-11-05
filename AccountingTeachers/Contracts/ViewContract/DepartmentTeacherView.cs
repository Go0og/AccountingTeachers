using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ViewContract
{
    public class DepartmentTeacherView
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string FIO { get; set; } = string.Empty ;
        public string Position { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Bet { get; set; }
    }
}
