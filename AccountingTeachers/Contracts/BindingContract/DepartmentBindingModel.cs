using System;
using DataModel.enums;
using DataModel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BindingContract
{
    public class DepartmentBindingModel : Idepartment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}
