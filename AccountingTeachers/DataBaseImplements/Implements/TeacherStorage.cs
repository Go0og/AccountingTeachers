using Contracts.SearchContract;
using Contracts.StorageContract;
using Contracts.StorageContract.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseImplements.Implements
{
    public class TeacherStorage : ITeacherStorage
    {
        public List<Teacher> GetFillteredList(TeacherSearch SearchModel)
        {
            using var context = new DataBaseImplement();
            if (SearchModel.Id.HasValue)
            {
                return context.Teachers
                    .Where(x => x.Id == SearchModel.Id)
                    .ToList();
            }
            if (SearchModel.DateStart.HasValue && SearchModel.DateEnd.HasValue)
            {
                return context.Teachers
                    .Where(x => x.DateStart >= SearchModel.DateStart  && x.DateEnd<= SearchModel.DateEnd)
                    .ToList();
            }
            if (SearchModel.DeparmentId.HasValue)
            {
                return context.Teachers
                    .Where(x => x.DepartmentId == SearchModel.DeparmentId)
                    .ToList();
            }

            return new();
        }

        public List<Teacher> GetFullList()
        {
            using var context = new DataBaseImplement();
            return context.Teachers.ToList();
        }

        public Teacher? GetTeacher(TeacherSearch SearchModel)
        {
            using var context = new DataBaseImplement();
            if (SearchModel.Id.HasValue)
            {
                return context.Teachers
                    .FirstOrDefault(x => x.Id == SearchModel.Id);
            }
            if (!string.IsNullOrEmpty(SearchModel.FIO))
            {
                return context.Teachers
                    .FirstOrDefault(x => x.FIO == SearchModel.FIO);
            }
            return null;
        }
    }
}
