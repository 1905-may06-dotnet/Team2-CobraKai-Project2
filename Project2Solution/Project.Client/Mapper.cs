using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Lib = Project.Domain;

namespace Project.Client
{
    public static class Mapper
    {
        public static Project.Client.Entities.Person Map(Lib.Person person) => new Project.Client.Entities.Person
        {
            Id = person.Id,
            Email = person.Email,
            Username = person.Username,
            Password = person.Password,
            Firstname = person.FirstName,
            Lastname = person.LastName
        };

        public static Lib.Person Map(Project.Client.Entities.Person person) => new Lib.Person
        {
            Id = person.Id,
            Email = person.Email,
            Username = person.Username,
            Password = person.Password,
            FirstName = person.Firstname,
            LastName = person.Lastname
        };

        public static IEnumerable<Lib.Person> Map(IEnumerable<Project.Client.Entities.Person> persons) => persons.Select(Map);
        public static IEnumerable<Project.Client.Entities.Person> Map(IEnumerable<Lib.Person> persons) => persons.Select(Map);
    }
}