using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Models.RequestModels;

namespace TP.Controllers
{

    public class SubjectController: ControllerBase
    {
        private List<Subject> list;
        public SubjectController()
        {

            list = new List<Subject> {
                new Subject(new Guid("819df009-1d4d-4a57-ac73-55e79973992a"),"dotnet something", null, "this is a topic about something"),
                new Subject(new Guid("ba099e58-9194-4841-b047-5d0c4541bba5"),"java something", null, "this is a topic about something else"),
                new Subject(new Guid("5c478ca2-b93f-4535-adc2-14676398d93e"),"out of ideas", null, "this is a topic about something"),
                new Subject(new Guid("01bf2aae-3eac-4980-ae73-403c6bffaede"),"totally out of ideas", null, "this is a topic about something"),
                new Subject(new Guid("bc6cc9f3-a5c2-438e-9eff-593a46918b21"),"im done", null, "this is a topic about something")
            };
            list[0].ParentSubject = list[1];
        }
        [HttpGet("api/GetSubjects")]
        public async Task <IActionResult> GetSubjects()
        {
            return Ok(list);
        }

        [HttpGet("api/GetSubjects/{id}")]
        public async Task <IActionResult> GetSubjectsById(Guid id)
        {
            var subjectId = list.Select(x => x.Id).ToList();
            if (subjectId.Contains(id)) { return Ok(list.First(x => x.Id == id)); }
            return BadRequest("oopsie");
        }

        [HttpPost("api/CreateSubject")]
        public async Task<IActionResult> CreateSubject([FromBody]SubjectRequestModel subjectRequestModel)
        {
            return Ok();
        }
    }
}
