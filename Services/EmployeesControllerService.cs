﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Services
{
    public class EmployeesControllerService : IEmployeesControllerService
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeesControllerService(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public void delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> getAll()
        {
            return _employeesRepository.getAll();
        }

        public Employee getById(Guid id)
        {
            return _employeesRepository.getById(id);
        }

        public Employee updateEmployee(Employee request, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
