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
    public class WordAccounting
    {
        public string Title { get; set; } = string.Empty;
        public List<DepartmentTeacherView>? accounting { get; set; } = new();

    }
}
