using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApp.Models
{
    public class PersonViewModel
    {
        public Person person { get; set; }
        public string surnameString { get; set; }
        public List<Gender> availableGenders { get; set; }
    }
}
