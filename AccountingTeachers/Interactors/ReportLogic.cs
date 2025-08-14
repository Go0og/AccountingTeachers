using Contracts.InteractorContract;
using Contracts.SearchContract;
using Contracts.StorageContract;
using Contracts.StorageContract.dbModels;
using Contracts.ViewContract;
using DataModel.enums;
using Interactors.OfficePackage;
using Interactors.OfficePackage.AbstractAccounting;
using Interactors.OfficePackage.AbstractOrder;
using Interactors.OfficePackage.Helpermodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactors
{
    public class ReportLogic : IReportLogic
    {
        private readonly IDepartmentStorage _departmentStorage;
        private readonly ITeacherStorage _teacherStorage;
        private readonly IOrderStorage _orderStorage;

        private readonly AbstractOrderHiringToWord _saveHiring;
        private readonly AbstractOrderSwapToWord _saveSwap;
        private readonly AbstractOrderFiringToWord _saveFiring;

        private readonly AbstractAccountingToWord _saveAccounting;


        public ReportLogic(IDepartmentStorage departmentStorage, ITeacherStorage teacherStorage, IOrderStorage orderStorage,
            AbstractOrderHiringToWord Save, AbstractOrderSwapToWord saveSwap, AbstractOrderFiringToWord saveFiring, 
            AbstractAccountingToWord abstractAccountingToWord)
        {
            _departmentStorage = departmentStorage;
            _teacherStorage = teacherStorage;
            _orderStorage = orderStorage;
            _saveHiring = Save;
            _saveSwap = saveSwap;
            _saveFiring = saveFiring;
            _saveAccounting = abstractAccountingToWord;
        }

        public byte[]? SaveAccountingToWordFile(List<DepartmentTeacherView> models)
        {
            var document = _saveAccounting.CreateDoc(new WordAccounting
            {
                Title = "Accounting",
                accounting = models
            });
            return document;
        }

        public byte[]? SaveOrderToWordFile(OrderView order, TypeOrders order_type)
        {
            Order? _order = _orderStorage.GetOrder(new OrderSearch
            {
                Id=order.Id,
            });
            Teacher? _teacher = _teacherStorage.GetTeacher(new TeacherSearch { Id = order.TeacherID });

            switch (order_type)
            {
                case TypeOrders.Hiring:
                    var document = _saveHiring.CreateDoc(new WordOrder
                    {
                        Title = "Hiring",
                        order = _order,
                        teacher = _teacher,
                        order_type = order_type
                    });
                    return document;
                case TypeOrders.Swap:
                    var swapdocument = _saveSwap.CreateDoc(new WordOrder
                    {
                        Title = "Hiring",
                        order = _order,
                        teacher = _teacher,
                        order_type = order_type
                    });
                    return swapdocument ;
                    
                case TypeOrders.Firing:
                    var firingdocument = _saveFiring.CreateDoc(new WordOrder
                    {
                        Title = "Hiring",
                        order = _order,
                        teacher = _teacher,
                        order_type = order_type
                    });
                    return firingdocument;
                default:
                    return null;
            }
            
        }
    }
}
