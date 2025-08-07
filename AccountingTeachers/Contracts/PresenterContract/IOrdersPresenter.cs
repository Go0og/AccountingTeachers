using Contracts.SearchContract;
using Contracts.ViewContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PresenterContract
{
    public interface IOrdersPresenter
    {
        public List<OrderView> MakeOrderList(OrderSearch? model);
        public OrderView? MakeOrder(OrderSearch model);
    }
}
