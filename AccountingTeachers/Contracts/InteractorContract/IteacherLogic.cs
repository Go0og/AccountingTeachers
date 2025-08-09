using Contracts.BindingContract;
using Contracts.SearchContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.InteractorContract
{
    public interface IteacherLogic
    {
        public List<TeacherBindingModel> GetTeacherList(TeacherSearch? model);
        public TeacherBindingModel GetTeacher(TeacherSearch model);
        public bool UpdateTeacher(TeacherBindingModel model); 
    }
}
