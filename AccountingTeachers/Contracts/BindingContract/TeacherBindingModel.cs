using DataModel.enums;
using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BindingContract
{
    public class TeacherBindingModel : ITeacher
    {
        public int Id { get; set; }

        public string FIO { get; set; } = string.Empty;

        public int bet { get; set; }

        public TitleTeacher TitleTeacher { get; set; } = TitleTeacher.Не_указано;

        public PositionTeacher PositionTeacher { get; set; } = PositionTeacher.Не_указано;

        public int departmentId { get; set; }

        
    }
}
