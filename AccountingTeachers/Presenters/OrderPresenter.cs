using Contracts.InteractorContract;
using Contracts.PresenterContract;
using Contracts.SearchContract;
using Contracts.ViewContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenters
{
    public class OrderPresenter : IOrdersPresenter
    {
        private readonly IOrdersLogic _logic;
        public OrderPresenter(IOrdersLogic logic)
        {
            _logic = logic;
        }

        public OrderView? MakeOrder(OrderSearch model)
        {
            var models = _logic.GetOrder(model);
            if (models == null)
            {
                return null;
            }
            var NewView = new OrderView()
            {
                Id = models.Id,
                TeacherID = models.TeacherID,
                DateOrder = models.DateOrder.ToString(),
                TypeOrders = models.TypeOrder.ToString(),
            };
            return NewView;

        }

        public List<OrderView> MakeOrderList(OrderSearch? model)
        {
            var models = _logic.GetOrderList(model);
            List<OrderView> orderViews = new();

            foreach (var item in models)
            {
                orderViews.Add(new OrderView()
                {
                    Id = item.Id,
                    TeacherID = item.TeacherID,
                    DateOrder = item.DateOrder.ToString(),
                    TypeOrders = item.TypeOrder.ToString(),
                });
            }
            return orderViews;
        }
    }
}
