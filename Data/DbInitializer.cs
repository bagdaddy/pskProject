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
            if (context.Users.Any() && context.Subjects.Any())
            {
                return; // DB has been seeded
            }

            var emp1 = new Employee
            {
                Id = Guid.Parse("b96f7695-c9d9-4b5c-849e-4219083d6220"),
                FirstName = "Mantas",
                LastName = "Bagdonas",
                Email = "bagzdiuglis_admin@gmail.com"
            };
            var emp2 = new Employee
            {
                Id = Guid.Parse("1c8c9ce0-2251-4ef3-ab0b-cfc7dd7a2949"),
                FirstName = "Evaldas",
                LastName = "Jonaitis",
                Email = "jonasEvladaitis@gmail.com"
            };
            var emp3 = new Employee
            {
                Id = Guid.Parse("5a677c6e-56e5-4cf1-9c64-157b483e8eff"),
                BossId = emp2.Id,
                FirstName = "Justinas",
                LastName = "Kondroska",
                Email = "jsutiniukas@gmail.com"
            };
            var emp4 = new Employee
            {
                Id = Guid.Parse("61f06ab5-1d19-40b3-90fa-bcd6ba20304b"),
                BossId = emp2.Id,
                FirstName = "Kostas",
                LastName = "Tostas",
                Email = "xd@gmail.com"
            };
            var emp5 = new Employee
            {
                Id = Guid.Parse("cd915686-9476-4dc9-af2f-1f4e8d6f744b"),
                BossId = emp4.Id,
                FirstName = "Arturas",
                LastName = "Honduras",
                Email = "nerasykce@gmail.com"
            };
            var employees = new Employee[]
            {
                emp1,
                emp2,
                emp3,
                emp4,
                emp5,
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
            context.Users.AddRange(employees);
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
                    EmployeeId = emp1.Id,
                    Employee = emp1,
                    SubjectId = parentSubject.Id,
                    Subject = parentSubject
                },
                new EmployeeSubject{
                    EmployeeId = emp1.Id,
                    Employee = emp1,
                    SubjectId = childOne.Id,
                    Subject = childOne
                },
                new EmployeeSubject{
                    EmployeeId = emp1.Id,
                    Employee = emp1,
                    SubjectId = childTwo.Id,
                    Subject = childTwo
                },
                new EmployeeSubject{
                    EmployeeId = emp2.Id,
                    Employee = emp2,
                    SubjectId = parentSubject.Id,
                    Subject = parentSubject
                },
                new EmployeeSubject{
                    EmployeeId = emp2.Id,
                    Employee = emp2,
                    SubjectId = childOne.Id,
                    Subject = childOne
                },
                new EmployeeSubject{
                    EmployeeId = emp3.Id,
                    Employee = emp3,
                    SubjectId = childTwo.Id,
                    Subject = childTwo
                },
                new EmployeeSubject{
                    EmployeeId = Guid.Parse("63f4e537-cb7b-4fe0-beca-cc080e42552d"),
                    SubjectId = childTwo.Id
                },
                new EmployeeSubject
                {
                    EmployeeId = emp4.Id,
                    Employee = emp4,
                    SubjectId = parentSubject.Id,
                    Subject = parentSubject
                },
                new EmployeeSubject
                {
                    EmployeeId = emp4.Id,
                    Employee = emp4,
                    SubjectId = childTwo.Id,
                    Subject = childTwo
                }
            };
            context.EmployeeSubjects.AddRange(employeeSubjects);
            context.SaveChanges();
        }

    }
}
