using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ulysses.App.Tests.Base
{
    public class BaseTest
    {
        protected ApplicationUnderTest TestedApp;

        [TestInitialize]
        public void TestSetUp()
        {
            TestedApp = ApplicationUnderTest.Launch(@"C:\Users\mordred\Documents\GitHub\Ulysses\Ulysses.App.Tests\bin\Debug\Ulysses.App.exe");
        }

        [TestCleanup]
        public void TestTearDown()
        {
            //if (TestedApp.Exists)
            //{
            //    //TestedApp.Close();
            //}
        }
    }
}