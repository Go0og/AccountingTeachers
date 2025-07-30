using Contracts.BindingContract;
using Contracts.SearchContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.InteractorContract
{
    public interface IDepartmentLogic
    {
        public List<DepartmentBindingModel> GetDepartmentList(DepartmentSearch? model);
        public DepartmentBindingModel GetDepartment(DepartmentSearch model);
    }
}
