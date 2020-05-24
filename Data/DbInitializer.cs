using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Contexts;
using TP.Data.Entities;

namespace TP.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Employees.Any() && context.Subjects.Any())
            {
                return; // DB has been seeded
            }

            var employees = new Employee[]
            {
                new Employee
            {
                Id = Guid.Parse("b96f7695-c9d9-4b5c-849e-4219083d6220"),
                FirstName = "Mantas",
                LastName = "Bagdonas",
                Email = "bagzdiuglis_admin@gmail.com"
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
                FirstName = "Petras ntr",
                LastName = "komandos Gardziulas",
                Email = "bapetrasglis@gmail.com"
            },
            new Employee
            {
                Id = Guid.Parse("ca595500-ac9b-4d55-a887-7ec28bad3983"),
                FirstName = "Algis",
                LastName = "Greitullas",
                Email = "bapalgislis@gmail.com"
            },
            new Employee
            {
                Id = Guid.Parse("4cce91af-6898-4b09-9d8d-043bbdc4e654"),
                FirstName = "Nartumanas",
                LastName = "Karbovskis",
                Email = "bkarbouskis@gmail.com"
            },
            new Employee
            {
                Id = Guid.Parse("c85d09e8-8658-456c-8595-502076661f41"),
                FirstName = "Lobanas",
                LastName = "Jovalas",
                Email = "Jobanasjovalas@gmail.com"
            },
            new Employee
            {
                Id = Guid.Parse("e214ea22-5d39-4cea-bf71-3749806e7bdd"),
                FirstName = "Zemaiciu",
                LastName = "Blynai",
                Email = "mm.su.grietinyte@gmail.com"
            }
            };
            context.Employees.AddRange(employees);
            context.SaveChanges();

            var parentSubject = new Subject("Programming", null, "description description text");
            var childOne = new Subject("Object oriented programming", parentSubject.Id, "description description text");
            var childTwo = new Subject("Functional programming", parentSubject.Id, "description description text");
            var subjects = new Subject[]
            {
                parentSubject,
                childOne,
                childTwo,
                new Subject("Java language", childOne.Id, "description description text"),
                new Subject("C language", childTwo.Id, "description description text")
        };
            context.Subjects.AddRange(subjects);
            context.SaveChanges();

            var employeeSubjects = new EmployeeSubject[]
            {
                new EmployeeSubject{
                    EmployeeId = Guid.Parse("b96f7695-c9d9-4b5c-849e-4219083d6220"),
                    SubjectId = parentSubject.Id
                },
                new EmployeeSubject{
                    EmployeeId = Guid.Parse("b96f7695-c9d9-4b5c-849e-4219083d6220"),
                    SubjectId = childOne.Id
                },
                new EmployeeSubject{
                    EmployeeId = Guid.Parse("b96f7695-c9d9-4b5c-849e-4219083d6220"),
                    SubjectId = childTwo.Id
                },
                new EmployeeSubject{
                    EmployeeId = Guid.Parse("1c8c9ce0-2251-4ef3-ab0b-cfc7dd7a2949"),
                    SubjectId = parentSubject.Id
                },
                new EmployeeSubject{
                    EmployeeId = Guid.Parse("1c8c9ce0-2251-4ef3-ab0b-cfc7dd7a2949"),
                    SubjectId = childOne.Id
                },
                new EmployeeSubject{
                    EmployeeId = Guid.Parse("5a677c6e-56e5-4cf1-9c64-157b483e8eff"),
                    SubjectId = childTwo.Id
                },
                new EmployeeSubject{
                    EmployeeId = Guid.Parse("63f4e537-cb7b-4fe0-beca-cc080e42552d"),
                    SubjectId = childTwo.Id
                }
            };
            context.EmployeeSubjects.AddRange(employeeSubjects);
            context.SaveChanges();
        }

    }
}
