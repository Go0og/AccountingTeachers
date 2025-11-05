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

        public DateTime DateStart { get; set; }
        public DateTime? DateSwap { get; set; }
        public DateTime DateEnd { get; set; }
        public int DepartmentId { get; set; }
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
                DateStart = model.DateStart,
                DateSwap = model.DateSwap,
                DateEnd = model.DateEnd,
                DepartmentId = model.DepartmentId,
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
            if (model.FIO != "")
            {
                FIO = model.FIO;
            }
            if (model.bet > 100 && model.bet < 999999)
            {
                bet = model.bet;
            }
            if (model.DepartmentId > 0)
            {
                DepartmentId = model.DepartmentId;
            }
            if (model.DateStart > DateTime.Parse("01.01.1900")) 
            {
                DateStart = model.DateStart;
            }
            DateSwap = model.DateSwap;
            if (model.DateEnd > DateTime.Parse("01.01.1900"))
            {
                DateEnd = model.DateEnd;
            }
            if (model.TitleTeacher != TitleTeacher.Не_указано)
            {
                TitleTeacher = model.TitleTeacher;
            }
            if (model.PositionTeacher != PositionTeacher.Не_указано)
            {
                PositionTeacher = model.PositionTeacher;
            }
        }

    }
}
