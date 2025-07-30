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
    public class TeacherLogic : IteacherLogic
    {
        private readonly ITeacherStorage _storage;
        public TeacherLogic (ITeacherStorage storage)
        {
            _storage = storage;
        }
        public TeacherBindingModel GetTeacher(TeacherSearch model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var get_model = _storage.GetTeacher(model);
            if (get_model == null)
            {
                return null;
            }
            return getBinding(get_model);
        }

        public List<TeacherBindingModel> GetTeacherList(TeacherSearch? model)
        {
            var models = _storage.GetFillteredList(model);
            if (models == null)
            {
                return new();
            }
            List<TeacherBindingModel> bindingModels = new();
            foreach (var mod in models)
            {
                bindingModels.Add(getBinding(mod));
            }
            return bindingModels;
        }

        public TeacherBindingModel getBinding(Teacher model)
        {
            return new()
            {
                Id = model.Id,
                FIO = model.FIO,
                bet = model.bet,
                TitleTeacher = model.TitleTeacher,
                PositionTeacher = model.PositionTeacher,
                departmentId = model.departmentId
            };
        }
    }
}
