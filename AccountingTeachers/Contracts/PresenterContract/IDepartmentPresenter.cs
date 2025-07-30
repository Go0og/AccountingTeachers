using Contracts.SearchContract;
using Contracts.ViewContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PresenterContract
{
    public interface IDepartmentPresenter
    {
        public List<DepartmentView> MakeDepartmentList(DepartmentSearch? model);
        public DepartmentView MakeDepartment(DepartmentSearch model);
    }
}
