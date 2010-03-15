using MIS2010Group1ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System;

namespace MIS2010Group1TestProject
{
    
    
    /// <summary>
    ///This is a test class for DemeritsTest and is intended
    ///to contain all DemeritsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DemeritsTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetAllDemerits
        ///</summary>
        [TestMethod()]
        public void GetAllDemeritsTest()
        {
            int expected = 5; // TODO: Initialize to an appropriate value
            DataSet actual = Demerits.GetAllDemerits();
            int actualRowCount = actual.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actualRowCount, "There are a new quantity of Demerits.  I would see this test changed!");
        }

        /// <summary>
        ///A test for ADtoDemeritList
        ///</summary>
        [TestMethod()]
        public void ADtoDemeritListTest()
        {
            int adID = 104;
            int demeritID = 3;
            string error = null;
            string errorExpected = "None";// "Primary key violation";
            bool expected = true;
            bool actual;
            actual = Demerits.ADtoDemeritList(adID, demeritID, out error);
            Assert.AreEqual(errorExpected, error, "Error happened, but was not supposed to...");
            Assert.AreEqual(expected, actual, "If you see this, there is a bigger error afoot.");
            

            adID = 129;
            demeritID = 3;
            expected = false;
            errorExpected = "Foreign key violation: Assigned Demerit does not exist";
            actual = Demerits.ADtoDemeritList(adID, demeritID, out error);
            Assert.AreEqual(errorExpected, error, "Assigned Demerit Error did not happen, but was supposed to...");
            Assert.AreEqual(expected, actual, "Something worked.");

            adID = 104;
            demeritID = 123;
            expected = false;
            errorExpected = "Foreign key violation: Demerit does not exist";
            actual = Demerits.ADtoDemeritList(adID, demeritID, out error);
            Assert.AreEqual(errorExpected, error, "Demerit Error did not happen, but was supposed to...");
            Assert.AreEqual(expected, actual, "Something worked.");
        }

        [TestCleanup()]
        public void DemeritsTestCleanup()
        {
            int demeritID = 3;
            int adID = 104;
            Demerits.DeleteFromDemeritList(demeritID, adID);
        }
    }
}
