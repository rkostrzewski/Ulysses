using System.Configuration;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ulysses.UI.Tests.Base
{
    public class BaseTest
    {
        private const string ApplicationPathSettingsKey = "ApplicationPath";
        private static readonly string ApplicationPath = ConfigurationManager.AppSettings[ApplicationPathSettingsKey];
        protected ApplicationUnderTest TestedApp;

        [TestInitialize]
        public void TestSetUp()
        {
            TestedApp = ApplicationUnderTest.Launch(ApplicationPath);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            if (TestedApp.Exists)
            {
                //TestedApp.Close();
            }
        }
    }
}