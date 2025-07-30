using Contracts.SearchContract;
using Contracts.ViewContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.PresenterContract
{
    public interface ITeacherPresenter
    {
        public List<TeacherView> MakeTeacherList(TeacherSearch? model);
        public TeacherView MakeTeacher(TeacherSearch model);
    }
}
