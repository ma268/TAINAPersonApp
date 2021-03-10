using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonApp.Helpers;

namespace PersonAppTests
{
    [TestClass]
    public class HelperTests
    {

        [TestMethod]
        public void GetBytes_PassedAString_ReturnsTrue()
        {
            var helper = new StringToByteHelper();

            var inputString = "teststring";

            var result = helper.GetBytes(inputString);

            Assert.IsTrue(result is byte[]);
        }

        [TestMethod]
        public void GetString_PassedBytes_ReturnsTrue()
        {
            var helper = new StringToByteHelper();

            var inputBytes = new byte[] { 0x20, 0x20, 0x20, 0x20};

            var result = helper.GetString(inputBytes);

            Assert.IsTrue(result is string);
        }
    }
}
