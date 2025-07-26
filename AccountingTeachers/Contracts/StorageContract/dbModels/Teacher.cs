using Contracts.BindingContract;
using DataModel.enums;
using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StorageContract.dbModels
{
    public class Teacher : ITeacher
    {
        public int Id { get; set; }
        public string FIO { get; set; }= string.Empty;

        public int bet { get; set; }

        public TitleTeacher TitleTeacher { get; set; }

        public PositionTeacher PositionTeacher { get; set; }

        public int departmentId { get; set; }
        public Department? Department { get; set; }

        public static Teacher? Create (TeacherBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Teacher()
            {
                Id = model.Id,
                FIO = model.FIO,
                bet = model.bet,
                departmentId = model.departmentId,
                TitleTeacher = model.TitleTeacher,
                PositionTeacher = model.PositionTeacher
            };
        }
        public void Update(TeacherBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            FIO = model.FIO;
            bet = model.bet;
            departmentId = model.departmentId;
            TitleTeacher = model.TitleTeacher;
            PositionTeacher = model.PositionTeacher;
        }

    }
}
