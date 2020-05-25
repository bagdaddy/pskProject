using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController
    {
        private List<Goal> list;
        public GoalsController()
        {
            list = new List<Goal> {
                new Goal(new Guid("b96f7695-c9d9-4b5c-849e-4219083d6220"),new Guid("819df009-1d4d-4a57-ac73-55e79973992a")),
                new Goal(new Guid("b96f7695-c9d9-4b5c-849e-4219083d6220"),new Guid("ba099e58-9194-4841-b047-5d0c4541bba5")),
                new Goal(new Guid("1c8c9ce0-2251-4ef3-ab0b-cfc7dd7a2949"),new Guid("5c478ca2-b93f-4535-adc2-14676398d93e")),
            };
        }
    }
}
