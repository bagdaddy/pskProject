using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Models.RequestModels;

namespace TP.DataContracts
{
    public interface IInviteControllerService
    {
        public Task<Guid> CreateInvite(Guid id, EmailRequestModel model);
        public Task DeleteInvite(Guid id);
        public Task<Invite> GetInvite(Guid id);

    }
}
