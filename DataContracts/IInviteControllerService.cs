using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Models.RequestModels;

namespace TP.DataContracts
{
    public interface IInviteControllerService
    {
        public Task<Guid> CreateInvite(Guid id, EmailRequestModel model);
    }
}
