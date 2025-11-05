using Contracts.InteractorContract;
using Contracts.PresenterContract;
using Contracts.SearchContract;
using Contracts.ViewContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenters
{
    public class DepartmentTeacherPresenter : IDepartmensTeachersPresenter
    {
        private readonly IteacherLogic _teacherLogic;
        private readonly IDepartmentLogic _departmentLogic;

        public DepartmentTeacherPresenter(IteacherLogic teacherLogic, IDepartmentLogic departmentLogic)
        {
            _teacherLogic = teacherLogic;
            _departmentLogic = departmentLogic;
        }

        public List<DepartmentTeacherView> MakeDepartmentList()
        {
            var DepartmentModel = _departmentLogic.GetDepartmentList(null);
            List<DepartmentTeacherView> ListView = new();
            foreach (var item in DepartmentModel) 
            {
                var TeacherModel = _teacherLogic.GetTeacherList(new TeacherSearch
                {
                    DeparmentId = item.Id,
                });
                foreach (var item2 in TeacherModel)
                {
                    ListView.Add(new DepartmentTeacherView
                    {
                        DepartmentName = item.Name,
                        FIO = item2.FIO,
                        Position = item2.PositionTeacher.ToString(),
                        Title = item2.TitleTeacher.ToString(),
                        Bet = item2.bet,
                    });
                }
            }

            return ListView;
        }
    }
}
