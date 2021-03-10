using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApp.Data
{
    public interface IGenderRepository
    {
        List<Gender> GetAllGenders();
        Gender GetGender(int id);


    }
}
