using Contracts.ViewContract;
using DataModel.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.InteractorContract
{
    public interface IReportLogic
    {
        byte[]? SaveOrderToWordFile(OrderView order, TypeOrders order_type);

        byte[]? SaveAccountingToWordFile(List<DepartmentTeacherView> models);
    }
}
