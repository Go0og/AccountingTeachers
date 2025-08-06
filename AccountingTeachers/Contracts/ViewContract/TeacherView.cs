using DataModel.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ViewContract
{
    public class TeacherView
    {
        public int Id { get; set; }
        public string FIO { get; set; } = string.Empty;
        public int bet {  get; set; }
        public string TitleTeacher { get; set; } = string.Empty;
        public string PositionTeacher { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateSwap { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
