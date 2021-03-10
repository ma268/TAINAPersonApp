using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonApp.Controllers;
using PersonApp.Data;
using PersonApp.Models;

namespace PersonAppTests
{
    /// <summary>
    /// THIS TEST IS CURRENTLY NOT WORKING
    /// </summary>
    [TestClass]
    public class GenderTests
    {
        private AppDbContext context;

        [TestInitialize]
        public void Setup()
        {
            //DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            //would be better to use in memory db so that we are not relying on a live data access for tests.
            //builder.UseInMemoryDatabase();
            context = new AppDbContext(builder.Options);
        }

        [TestMethod]
        public void GetListOfGenders_OnlyContainsGenderTyoes_ReturnsTrue()
        {

            var genderRepo = new GenderRepository(context);
            var genders = genderRepo.GetAllGenders();

            foreach (var gender in genders)
            {
                Assert.IsTrue(gender is Gender);
            }
        }
    }
}
