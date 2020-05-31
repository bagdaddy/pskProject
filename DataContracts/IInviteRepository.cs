using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    public interface IInviteRepository
    {
        public void Add(Invite invite);
        Task SaveChanges();
        Task<Invite> GetById(Guid id);

        void Delete(Invite invite);
    }
}
