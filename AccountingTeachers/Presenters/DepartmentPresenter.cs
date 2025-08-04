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
    public class DepartmentPresenter : IDepartmentPresenter
    {

        private readonly IDepartmentLogic _logic;
        public DepartmentPresenter(IDepartmentLogic logic)
        {
            _logic = logic;
        }
        public DepartmentView MakeDepartment(DepartmentSearch model)
        {
            var models = _logic.GetDepartment(model);
            if (models == null)
            {
                return null;
            }
            return new DepartmentView
            {
                Id = models.Id,
                Name = models.Name,

            };
        }

        public List<DepartmentView> MakeDepartmentList(DepartmentSearch? model)
        {
            var models = _logic.GetDepartmentList(model);
            List<DepartmentView> departmentViews = new();

            foreach (var modelItem in models)
            {
                departmentViews.Add(new DepartmentView
                {
                    Id = modelItem.Id,
                    Name = modelItem.Name,

                });
            }
            return departmentViews;
        }
    }
}
