using Contracts.BindingContract;
using Contracts.InteractorContract;
using Contracts.SearchContract;
using Contracts.StorageContract;
using Contracts.StorageContract.dbModels;
using Contracts.ViewContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactors
{
    public class OrderLogic : IOrdersLogic
    {
        private readonly IOrderStorage _storage;
        public OrderLogic(IOrderStorage storage)
        {
            _storage = storage;
        }

        public OrderBindignModel GetOrder(OrderSearch model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var get_model = _storage.GetOrder(model);
            if (get_model == null)
            {
                return null;
            }
            return getBinging(get_model);
        }

        public List<OrderBindignModel> GetOrderList(OrderSearch? model)
        {
            var models = model == null ? _storage.GetFullOrders() : _storage.GetFillteredList(model);
            if (models == null)
            {
                return new();
            }
            List<OrderBindignModel> bingingModels = new();
            foreach (var mod in models)
            {
                bingingModels.Add(getBinging(mod));
            }
            return bingingModels;
        }

        public OrderBindignModel getBinging(Order model)
        {
            return new()
            {
                Id = model.Id,
                TeacherID = model.TeacherID,
                TypeOrder = model.TypeOrders,
                DateOrder = model.DateOrders,
            };
        }

        public bool CreateOrder(OrderBindignModel model)
        {
            if (_storage.CreateOrder(model) == false)
            {
                return false;
            }
            return true;
        }

        public bool UpdateOrder(OrderBindignModel model)
        {
            if (_storage.UpdateOrder(model) == false)
            {
                return false;
            }
            return true;
        }
    }
}
