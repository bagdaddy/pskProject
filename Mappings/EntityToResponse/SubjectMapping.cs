using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Models.ResponseModels;

namespace TP.Mappings.EntityToResponse
{
    public static class SubjectMapping
    {
        public static List<BareSubjectResponseModel> MapToResponse(this List<Subject> subjects)
        {
            var responseModel = subjects.Select(x => new BareSubjectResponseModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return responseModel;
        }
    }
}
