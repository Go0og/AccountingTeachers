using Contracts.SearchContract;
using Contracts.StorageContract;
using Contracts.StorageContract.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseImplements.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<Order> GetFillteredList(OrderSearch SearchModel)
        {
            using var context = new DataBaseImplement();
            if (SearchModel.Id.HasValue)
            {
                return context.Orders
                    .Where(x => x.Id == SearchModel.Id)
                    .ToList();
            }
            if (SearchModel.TeacherID.HasValue) 
            {
                return context.Orders
                    .Where(x => x.TeacherID == SearchModel.TeacherID)
                    .ToList();
            }
            if (SearchModel.DateOrders.HasValue)
            {
                return context.Orders
                    .Where(x => x.DateOrders <= SearchModel.DateOrders)
                    .ToList();
            }
            if (SearchModel.TypeOrders.HasValue) 
            {
                return context.Orders
                    .Where(x => x.TypeOrders == SearchModel.TypeOrders)
                    .ToList();
            }
            return new();
        }

        public List<Order> GetFullOrders()
        {
            using var context = new DataBaseImplement();
            return context.Orders.ToList();
        }

        public Order? GetOrder(OrderSearch SearchModel)
        {
            using var context = new DataBaseImplement();
            if (SearchModel.Id.HasValue)
            {
                return context.Orders
                    .FirstOrDefault(x => x.Id == SearchModel.Id);
            }
            return null;
        }
    }
}
