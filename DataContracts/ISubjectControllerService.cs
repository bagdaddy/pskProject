using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    interface ISubjectControllerService
    {
        List<Subject> getAll();
        Subject getById(Guid id);
        void delete(Guid id);
    }
}
