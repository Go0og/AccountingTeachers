using Contracts.BindingContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StorageContract.dbModels
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public static Department Create(DepartmentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Department 
            {
                Id = model.Id, 
                Name = model.Name 
            };
        }

        public void Update(DepartmentBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            Name = model.Name;
        }
    }
}
