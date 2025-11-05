using Contracts.SearchContract;
using Contracts.StorageContract;
using Contracts.StorageContract.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseImplements.Implements
{
    public class DeparmentStorage : IDepartmentStorage
    {
        public Department? GetDepartment(DepartmentSearch SearchModel)
        {
            using var context = new DataBaseImplement();
            if (SearchModel.Id.HasValue)
            {
                return context.Departments.FirstOrDefault(x => x.Id == SearchModel.Id);
            }
            else if (SearchModel.Name != string.Empty)
            {
                return context.Departments.FirstOrDefault(x=> x.Name == SearchModel.Name);
            }
            return null;
        }

        public List<Department> GetFillteredList(DepartmentSearch SearchModel)
        {
            using var context = new DataBaseImplement();
            if (SearchModel.Name != string.Empty) 
            {
                return context.Departments
                    .Where(x => x.Name == SearchModel.Name)
                    .ToList();
            }

            return new();
        }

        public List<Department> GetFullList()
        {
            using var context = new DataBaseImplement();
            return context.Departments.ToList();
        }
    }
}
