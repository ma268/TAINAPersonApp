using Microsoft.EntityFrameworkCore;
using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApp.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext context;

        public PersonRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void AddPerson(Person person)
        {
            try
            {
                using (context)
                {
                    context.Person.Add(person);
                }
            }
            catch(AccessViolationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Person> GetAllPersons()
        {
            return context.Person.Include(p => p.Gender).ToList();
        }

        public Person GetPerson(int id)
        {
            try
            {
                using (context)
                {
                    return context.Person.Include(p => p.Gender).FirstOrDefault(p => p.PersonId == id);
                }
            }
            catch (NullReferenceException ex)
            {
                throw;
            }
            catch (DuplicateWaitObjectException ex)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void RemovePerson(int id)
        {
            try
            {
                using (context)
                {
                    context.Person.Remove(GetPerson(id));
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdatePerson(Person person)
        {
            try
            {
                using (context)
                {
                    context.Person.Update(person);
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await context.SaveChangesAsync() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
