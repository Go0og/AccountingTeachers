using Contracts.BindingContract;
using DataModel.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.StorageContract.dbModels
{
    public class Order
    {
        public int Id { get; set; }
        public int TeacherID { get; set; }
        public Teacher? Teacher { get; set; }
        public TypeOrders TypeOrders { get; set; }
        public DateTime DateOrders { get; set; }

        public static Order? Create (OrderBindignModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Order
            {
                Id = model.Id,
                TeacherID = model.TeacherID,
                DateOrders = model.DateOrder,
                TypeOrders = model.TypeOrder,
            };
        }

        public void Update(OrderBindignModel model)
        {
            if (model == null)
            {
                return;
            }
            Id= model.Id;
            TeacherID= model.TeacherID;
            DateOrders = model.DateOrder;
            TypeOrders = model.TypeOrder;
        }
    }
}
