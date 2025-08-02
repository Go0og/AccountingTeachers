using Contracts.PresenterContract;
using Contracts.SearchContract;
using Contracts.ViewContract;
using Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenters
{
    public class TeacherPresenter : ITeacherPresenter
    {
        private readonly TeacherLogic _logic;

        public TeacherPresenter (TeacherLogic logic)
        {
            _logic = logic;
        }

        public TeacherView? MakeTeacher(TeacherSearch model)
        {
            var models = _logic.GetTeacher(model);
            if (model == null) 
            {
                return null;
            }
            var NewViewModel = new TeacherView
            {
                Id = (int)model.Id,
                FIO = model.FIO,
                bet = model.bet,
                PositionTeacher = model.PositionTeacher.ToString(),
                TitleTeacher = model.TitleTeacher.ToString(),
                departmentId = (int)model.DeparmentId,

            };
            return NewViewModel;
        }

        public List<TeacherView> MakeTeacherList(TeacherSearch? model)
        {
           var models = _logic.GetTeacherList(model);
            List<TeacherView> teacherViews = new();

            foreach (var item in models) 
            {
                teacherViews.Add(new TeacherView
                {
                    Id = (int)item.Id,
                    FIO = item.FIO,
                    bet = item.bet,
                    PositionTeacher = model.PositionTeacher.ToString(),
                    TitleTeacher = model.TitleTeacher.ToString(),
                    departmentId = (int)model.DeparmentId,
                });
            }
            return teacherViews;
        }
    }
}
