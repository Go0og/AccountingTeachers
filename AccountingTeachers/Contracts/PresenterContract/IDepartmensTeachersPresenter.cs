using Contracts.SearchContract;
using Contracts.ViewContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PresenterContract
{
    public interface IDepartmensTeachersPresenter
    {
        public List<DepartmentTeacherView> MakeDepartmentList();
    }
}
