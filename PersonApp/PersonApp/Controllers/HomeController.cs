using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PersonApp.Data;
using PersonApp.Helpers;
using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonRepository personRepository;
        private readonly IMemoryCache memoryCache;

        public HomeController(ILogger<HomeController> logger,
                              IPersonRepository personRepository,
                              IMemoryCache memoryCache)
        {
            _logger = logger;
            this.personRepository = personRepository;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            var data = personRepository.GetAllPersons();

            var stringToByteHelper = new StringToByteHelper();

            var result = new List<PersonViewModel>();

            foreach (var row in data)
            {
                var member = new PersonViewModel() 
                {
                    person = new Person(),
                };

                member.person.Firstname = row.Firstname;
                member.surnameString = stringToByteHelper.GetString(row.Surname);
                member.person.PersonId = row.PersonId;
                member.person.PhoneNumber = row.PhoneNumber;
                member.person.Gender = row.Gender;
                member.person.DateOfBirth = row.DateOfBirth;
                member.person.EmailAddress = row.EmailAddress;

                result.Add(member);
            }

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
