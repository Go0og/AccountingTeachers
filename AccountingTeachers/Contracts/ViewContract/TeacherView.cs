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
        public TitleTeacher TitleTeacher { get; set; }
        public PositionTeacher PositionTeacher { get; set; }
        public int departmentId { get; set; }
    }
}
