using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApp.Data
{
    public interface IPersonRepository
    {
        Person GetPerson(int id);
        List<Person> GetAllPersons();
        void UpdatePerson(Person person);
        void AddPerson(Person person);
        void RemovePerson(int id);

        Task<bool> SaveChangesAsync();
    }
}
