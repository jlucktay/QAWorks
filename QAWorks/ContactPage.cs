using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace QAWorks
{
    [TestClass]
    public class ContactPage
    {
        private IWebDriver _iwd;

        [TestInitialize]
        public void TestInit()
        {
            _iwd = new FirefoxDriver();
            _iwd.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            _iwd.Manage().Window.Maximize();

            _iwd.Navigate().GoToUrl("qaworks.com");
            Assert.AreEqual("Home Page - QAWorks", _iwd.Title);

            _iwd.FindElement(By.LinkText("Contact")).Click();
            Assert.AreEqual("Contact Us - QAWorks", _iwd.Title);
        }

        [TestCleanup]
        public void TestClean()
        {
            _iwd.Close();
        }

        /// <summary>
        /// Check that the directions on the Contact Us page contain accurate information.
        /// </summary>
        [TestMethod]
        public void CheckDirections()
        {
            IWebElement info1 = null, info2 = null, info3 = null;

            try
            {
                info1 = _iwd.FindElement(By.Id("ContactInfoBlock1"));
                info2 = _iwd.FindElement(By.Id("ContactInfoBlock2"));
                info3 = _iwd.FindElement(By.Id("ContactInfoBlock3"));
            }
            catch (NoSuchElementException nsee)
            {
                Assert.Fail("Element not found: " + nsee.Message);
            }

            Assert.IsTrue(info1.Text.Contains("Office Location"));
            Assert.IsTrue(info2.Text.Contains("Directions"));
            Assert.IsTrue(info3.Text.Contains("M25"));
        }

        /// <summary>
        /// Submit a query using the form on the Contact Us page.
        /// </summary>
        [TestMethod]
        public void SubmitQuery()
        {
            IWebElement iweName = null, iweEmail = null, iweMessage = null, iweSend = null;

            try
            {
                iweName = _iwd.FindElement(By.Id("ctl00_MainContent_NameBox"));
                iweEmail = _iwd.FindElement(By.Id("ctl00_MainContent_EmailBox"));
                iweMessage = _iwd.FindElement(By.Id("ctl00_MainContent_MessageBox"));
                iweSend = _iwd.FindElement(By.Id("ctl00_MainContent_SendButton"));
            }
            catch (NoSuchElementException nsee)
            {
                Assert.Fail("Element not found: " + nsee.Message);
            }

            iweName.SendKeys("J. Bloggs");
            iweEmail.SendKeys("j.bloggs@qaworks.com");
            iweMessage.SendKeys("Please contact me, I want to find out more!");
            iweSend.Click();

            // After the form is submitted, the page title changes to just "QAWorks" (without "Contact Us".)
            Assert.AreEqual("QAWorks", _iwd.Title);
        }
    }
}
