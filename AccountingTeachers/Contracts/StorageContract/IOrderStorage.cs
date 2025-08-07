using Contracts.SearchContract;
using Contracts.StorageContract.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StorageContract
{
    public interface IOrderStorage
    {
        public List<Order> GetFullOrders();
        public List<Order> GetFillteredList(OrderSearch SearchModel);
        public Order? GetOrder(OrderSearch SearchModel);
    }
}
