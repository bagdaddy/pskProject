using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.Data
{
    public static class FakeEmployees
    {
        public static List<Employee> getAll()
        {
            return employees;
        }

        public static Employee getById(Guid id)
        {
            return employees.FirstOrDefault(employee => employee.Id == id);
        }

        private static List<Employee> employees = new List<Employee>() {
        new Employee
            {
                Id = Guid.Parse("b96f7695-c9d9-4b5c-849e-4219083d6220"),
                FirstName = "Mantas",
                LastName = "Bagdonas",
                Email = "bagzdiuglis@gmail.com"
            },
        new Employee
            {
                Id = Guid.Parse("1c8c9ce0-2251-4ef3-ab0b-cfc7dd7a2949"),
                FirstName = "Evaldas",
                LastName = "Jonaitis",
                Email = "jonasEvladaitis@gmail.com"
            },
        new Employee
            {
                Id = Guid.Parse("5a677c6e-56e5-4cf1-9c64-157b483e8eff"),
                FirstName = "Justinas",
                LastName = "Kondroska",
                Email = "jsutiniukas@gmail.com"
            },
        new Employee
            {
                Id = Guid.Parse("63f4e537-cb7b-4fe0-beca-cc080e42552d"),
                FirstName = "Spotify",
                LastName = "Premium",
                Email = "hellos@gmail.com"
            },
            new Employee
            {
                Id = Guid.Parse("69ca4eef-df2a-4d04-87eb-c0e2f31c0708"),
                FirstName = "Petras",
                LastName = "Gardziulas",
                Email = "bapetrasglis@gmail.com"
            }
        };
    }
}
