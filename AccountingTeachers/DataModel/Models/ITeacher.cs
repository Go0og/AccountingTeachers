using DataModel.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public interface ITeacher : Iid
    {
        string FIO { get; }
        
        int bet {  get; }

        TitleTeacher TitleTeacher { get; } 

        PositionTeacher PositionTeacher { get; }

        int DepartmentId { get; }

        DateTime DateStart { get; }

        DateTime? DateSwap { get; }
        DateTime DateEnd { get; }


    }
}
