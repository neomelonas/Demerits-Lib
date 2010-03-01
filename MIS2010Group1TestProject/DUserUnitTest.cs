using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Data;
using MIS2010Group1ClassLibrary;

namespace MIS2010Group1TestProject
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class DUserUnitTest
    {
        public DUserUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void UserListTestMethod()
        {
            string expectedFirstResult = "3";
            string role = "Teacher";
            DataSet dataset = DUser.GetUserList(role);
            string firstResult = dataset.Tables[0].Rows[0]["userID"].ToString();
            Assert.AreEqual(expectedFirstResult, firstResult, "Expected '3' for Teacher, got something else.");

            expectedFirstResult = "1";
            role = "Student";
            dataset = DUser.GetUserList(role);
            firstResult = dataset.Tables[0].Rows[0]["userID"].ToString();
            Assert.AreEqual(expectedFirstResult, firstResult, "Expected '1' for Student, got something else.");

            expectedFirstResult = "5";
            role = "Parent";
            dataset = DUser.GetUserList(role);
            firstResult = dataset.Tables[0].Rows[0]["userID"].ToString();
            Assert.AreEqual(expectedFirstResult, firstResult, "Expected '5' for Parent, got something else.");
        }
    }
}
