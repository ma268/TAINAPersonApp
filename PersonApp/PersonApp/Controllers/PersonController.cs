using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PersonApp.Data;
using PersonApp.Helpers;
using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository personRepository;
        private readonly IGenderRepository genderRepository;
        private readonly IMemoryCache memoryCache;

        public string Message { get; set; }

        public PersonController(ILogger<PersonController> logger,
                              IPersonRepository personRepository,
                              IGenderRepository genderRepository,
                              IMemoryCache memoryCache)
        {
            _logger = logger;
            this.personRepository = personRepository;
            this.genderRepository = genderRepository;
            this.memoryCache = memoryCache;
        }

        // GET: PersonController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var result = personRepository.GetPerson(id);

                Message = $"Person with id {id} selected on {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);

                var personDetails = new PersonViewModel()
                {
                    person = new Person(),
                };

                var stringToByteHelper = new StringToByteHelper();

                personDetails.person.Firstname = result.Firstname;
                personDetails.surnameString = stringToByteHelper.GetString(result.Surname);
                personDetails.person.PersonId = result.PersonId;
                personDetails.person.DateOfBirth = result.DateOfBirth;
                personDetails.person.EmailAddress = result.EmailAddress;
                personDetails.person.Gender = result.Gender;
                personDetails.person.PhoneNumber = result.PhoneNumber;

                return View(personDetails);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        // GET: PersonController/Create

        public ActionResult Create()
        {
            List<Gender> genderList = new List<Gender>();
            bool AlreadyCached = memoryCache.TryGetValue("CachedGenderList", out genderList);
            if (!AlreadyCached)
            {
                genderList = genderRepository.GetAllGenders();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(5));
                memoryCache.Set("CachedGenderList", genderList, cacheEntryOptions);
            }

            ViewBag.GenderList = genderList;

            return View(new PersonViewModel());
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PersonViewModel data)
        {

            var stringToByteHelper = new StringToByteHelper();

            var newPerson = new Person()
            {
                Firstname = data.person.Firstname,
                DateOfBirth = data.person.DateOfBirth,
                EmailAddress = data.person.EmailAddress,
                Gender = genderRepository.GetGender(data.person.Gender.Id),
                PhoneNumber = data.person.PhoneNumber,
                Surname = stringToByteHelper.GetBytes(data.surnameString)
            };

            try
            {
                personRepository.AddPerson(newPerson);
                await personRepository.SaveChangesAsync();
                Message = $"Person with id {newPerson.PersonId} added on {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return RedirectToAction(nameof(Index), controllerName: "Home");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {

            try
            {
                var result = personRepository.GetPerson(id);

                var personDetails = new PersonViewModel()
                {
                    person = new Person(),
                };

                var stringToByteHelper = new StringToByteHelper();

                personDetails.person.Firstname = result.Firstname;
                personDetails.surnameString = stringToByteHelper.GetString(result.Surname);
                personDetails.person.PersonId = result.PersonId;
                personDetails.person.DateOfBirth = result.DateOfBirth;
                personDetails.person.EmailAddress = result.EmailAddress;
                personDetails.person.Gender = result.Gender;
                personDetails.person.PhoneNumber = result.PhoneNumber;

                var genderList = genderRepository.GetAllGenders();

                ViewBag.GenderList = genderList;

                return View(personDetails);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PersonViewModel data)
        {

            var stringToByteHelper = new StringToByteHelper();

            var person = new Person()
            {
                Firstname = data.person.Firstname,
                Surname = stringToByteHelper.GetBytes(data.surnameString),
                EmailAddress = data.person.EmailAddress,
                DateOfBirth = data.person.DateOfBirth,
                Gender = genderRepository.GetGender(data.person.Gender.Id),
                PhoneNumber = data.person.PhoneNumber,
                PersonId = data.person.PersonId
            };

            try
            {
                personRepository.UpdatePerson(person);
                await personRepository.SaveChangesAsync();
                Message = $"Person with id {id} edited on {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return RedirectToAction(nameof(Details), new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
