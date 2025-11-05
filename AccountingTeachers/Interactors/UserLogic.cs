using Contracts.BindingContract;
using Contracts.InteractorContract;
using Contracts.SearchContract;
using Contracts.StorageContract;
using Contracts.StorageContract.dbModels;

namespace Interactors
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserStorage _storage;
        public UserLogic(IUserStorage storage)
        {
            _storage = storage;
        }
        public void CheckModel(UserBindingModel bindingModel, bool obDel = false, bool onUp = false)
        {
            if (string.IsNullOrEmpty(bindingModel.Id.ToString()))
            {
                throw new ArgumentNullException("Id is missing", nameof(bindingModel.Id));
            }
            if (string.IsNullOrEmpty(bindingModel.Name))
            {
                throw new ArgumentNullException("Name is missing", nameof(bindingModel.Name));
            }
            if (string.IsNullOrEmpty(bindingModel.Email))
            {
                throw new ArgumentNullException("Email is missing", nameof(bindingModel.Email));
            }
            if (string.IsNullOrEmpty(bindingModel.Password))
            {
                throw new ArgumentNullException("Login is missing", nameof(bindingModel.Password));
            }
            if (obDel)
            {
                return;
            }
        }

        public bool CreateUser(UserBindingModel UserBindingModel)
        {
            CheckModel(UserBindingModel);
            if (_storage.Create(UserBindingModel) == false)
            {
                throw new Exception("insert operation failed");
            }
            return true;
        }
        public bool UpdateUser(UserBindingModel UserBindingModel)
        {
            CheckModel(UserBindingModel);
            if (_storage.Update(UserBindingModel) == false)
            {
                throw new Exception("Update  operation failed");
            }
            return true;
        }

        public bool DeleteUser(UserBindingModel UserBindingModel)
        {
            CheckModel(UserBindingModel, true);
            if (_storage.Delete(UserBindingModel) == false)
            {
                throw new Exception("Delete operation failed");
            }
            return true;
        }

        public List<UserBindingModel> GetList(UserSearch? searchModel)
        {
            var models = searchModel == null ? _storage.GetFullList() : _storage.GetFillteredList(searchModel);
            if (models.Count == 0) 
            {
                return new();
            }
            List<UserBindingModel> bindingModels = new();
            foreach (var model in models)
            {
                bindingModels.Add(getBindingModel(model));
            }
            return bindingModels;
        }

        public UserBindingModel GetUser(UserSearch SearchModel)
        {
            if (SearchModel == null) throw new ArgumentNullException(nameof(SearchModel));

            var model = _storage.GetUser(SearchModel);
            if (model == null)
            {
                return null;
            }
            return getBindingModel(model);
        }
        public UserBindingModel getBindingModel(User model)
        {
            return new()
            {
                Id = model.Id,
                Name = model.Name,
                Password = model.Password,
                Email = model.Email
            };
        }

    }
}
