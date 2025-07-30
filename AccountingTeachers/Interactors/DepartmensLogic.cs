using Contracts.BindingContract;
using Contracts.InteractorContract;
using Contracts.SearchContract;
using Contracts.StorageContract;
using Contracts.StorageContract.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactors
{
    public class DepartmensLogic : IDepartmentLogic
    {
        private readonly IDepartmentStorage _storage;
        public DepartmensLogic(IDepartmentStorage storage)
        {
            _storage = storage;
        }
        public DepartmentBindingModel GetDepartment(DepartmentSearch model)
        {
            if (model == null) 
            {
                throw new ArgumentNullException(nameof(model));
            }
            var mod = _storage.GetDepartment(model);
            if (mod == null)
            {
                return null;
            }
            return GetBindingModel(mod);
        }

        public List<DepartmentBindingModel> GetDepartmentList(DepartmentSearch? model)
        {
            var models = _storage.GetFillteredList(model);
            if (models == null)
            {
                return new();
            }
            List<DepartmentBindingModel> bindingModel = new();
            foreach (var mod in models)
            {
                bindingModel.Add(GetBindingModel(mod));
            }
            return bindingModel;
        }
        public DepartmentBindingModel GetBindingModel(Department model)
        {
            return new()
            {
                Id = model.Id,
                Name = model.Name,

            };
        }
    }
}
