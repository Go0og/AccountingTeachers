using Contracts.StorageContract.dbModels;
using Contracts.ViewContract;
using DataModel.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactors.OfficePackage.Helpermodels
{
    public class WordOrder
    {
        public string Title { get; set; } = string.Empty;
        public Order? order { get; set; } = new();
        public Teacher? teacher { get; set; } = new();

        public TypeOrders order_type { get; set; }

    }
}
