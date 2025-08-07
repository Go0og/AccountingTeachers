using Contracts.BindingContract;
using Contracts.SearchContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.InteractorContract
{
    public interface IOrdersLogic
    {
        public List<OrderBindignModel> GetOrderList(OrderSearch? model);
        public OrderBindignModel GetOrder(OrderSearch model);
    }
}
