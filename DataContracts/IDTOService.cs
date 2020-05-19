﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Services.DTOServices.Models;

namespace TP.DataContracts
{
    interface IDTOService
    {
        public List<EmployeeDTO> EmployeesToDTO(List<Employee> employeeList);

        public EmployeeDTO EmployeeToDTO(Employee employee);
    }
}
