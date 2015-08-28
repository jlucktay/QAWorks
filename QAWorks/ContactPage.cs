using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace QAWorks
{
    [TestClass]
    public class ContactPage
    {
        private IWebDriver iwd;

        [TestInitialize]
        public void TestInit()
        {
            iwd = new FirefoxDriver();
            iwd.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            iwd.Manage().Window.Maximize();

            iwd.Navigate().GoToUrl("qaworks.com");
            Assert.AreEqual("Home Page - QAWorks", iwd.Title);

            iwd.FindElement(By.LinkText("Contact")).Click();
            Assert.AreEqual("Contact Us - QAWorks", iwd.Title);
        }

        [TestCleanup]
        public void TestClean()
        {
            iwd.Close();
        }

        /// <summary>
        /// Check that the directions on the Contact Us page contain accurate information.
        /// </summary>
        [TestMethod]
        public void CheckDirections()
        {
            string info1 = iwd.FindElement(By.Id("ContactInfoBlock1")).Text;
            Assert.IsTrue(info1.Contains("Office Location"));

            string info2 = iwd.FindElement(By.Id("ContactInfoBlock2")).Text;
            Assert.IsTrue(info2.Contains("Directions"));

            string info3 = iwd.FindElement(By.Id("ContactInfoBlock3")).Text;
            Assert.IsTrue(info3.Contains("M25"));
        }

        /// <summary>
        /// Submit a query using the form on the Contact Us page.
        /// </summary>
        [TestMethod]
        public void SubmitQuery()
        {
            IWebElement iweName = iwd.FindElement(By.Id("ctl00_MainContent_NameBox"));
            iweName.SendKeys("J. Bloggs");

            IWebElement iweEmail = iwd.FindElement(By.Id("ctl00_MainContent_EmailBox"));
            iweEmail.SendKeys("j.bloggs@qaworks.com");

            IWebElement iweMessage = iwd.FindElement(By.Id("ctl00_MainContent_MessageBox"));
            iweMessage.SendKeys("Please contact me, I want to find out more!");

            IWebElement iweSend = iwd.FindElement(By.Id("ctl00_MainContent_SendButton"));
            iweSend.Click();

            // After the form is submitted, the page title changes to just "QAWorks" (without "Contact Us".)
            Assert.AreEqual("QAWorks", iwd.Title);
        }
    }
}
