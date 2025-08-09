using Contracts.InteractorContract;
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
        private readonly IteacherLogic _logic;

        public TeacherPresenter (IteacherLogic logic)
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
                Id = (int)models.Id,
                FIO = models.FIO,
                bet = (int)models.bet,
                PositionTeacher = models.PositionTeacher.ToString(),
                TitleTeacher = models.TitleTeacher.ToString(),
                DepartmentId = models.DepartmentId,
                DateStart = (DateTime)models.DateStart,
                DateSwap = (DateTime?)models.DateSwap,
                DateEnd = (DateTime)models.DateEnd,

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
                    PositionTeacher = item.PositionTeacher.ToString(),
                    TitleTeacher = item.TitleTeacher.ToString(),
                    DepartmentId = (int)item.DepartmentId,
                    DateStart = (DateTime)item.DateStart,
                    DateSwap = (DateTime?)item.DateSwap,
                    DateEnd = (DateTime)item.DateEnd,
                });
            }
            return teacherViews;
        }
    }
}
