using Contracts.SearchContract;
using Contracts.StorageContract.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StorageContract
{
    public interface IDepartmentStorage
    {
        public List<Department> GetFullList();
        public List<Department> GetFillteredList(DepartmentSearch SearchModel);
        public Department? GetDepartment(DepartmentSearch SearchModel);
    }
}
