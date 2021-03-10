using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApp.Data
{
    public class GenderRepository : IGenderRepository
    {
        private readonly AppDbContext context;

        public GenderRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<Gender> GetAllGenders()
        {
            try
            {
                return context.Gender.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Gender GetGender(int id)
        {
            try
            {
                return context.Gender.FirstOrDefault(g => g.Id == id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
