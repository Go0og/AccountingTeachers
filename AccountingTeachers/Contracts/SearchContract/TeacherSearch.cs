using DataModel.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.SearchContract
{
    public class TeacherSearch
    {
        public int? Id { get; set; }
        public string? FIO { get; set; }
        public int? bet {  get; set; }
        public TitleTeacher? TitleTeacher { get; set; }
        public PositionTeacher? PositionTeacher { get; set; }
        public int? DeparmentId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateSwap { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}
