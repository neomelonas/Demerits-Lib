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
        ///A test for AddAssignedDemerit
        ///</summary>
        [TestMethod()]
        public void AddAssignedDemeritTest()
        {
            int teacherID = 10;
            int studentID = 1;
            string error = "None";
            string errorExpected = "None";
            bool expected = true;
            bool actual;
            actual = Demerits.AddAssignedDemerit(teacherID, studentID, out error);
            Assert.AreEqual(errorExpected, error, "Insert failed.");
            Assert.AreEqual(expected, actual, "Insert failed.");

            teacherID = 10;
            studentID = 1;
            expected = false;
            errorExpected = "Primary key violation";
            actual = Demerits.AddAssignedDemerit(teacherID, studentID, out error);
            Assert.AreEqual(errorExpected, error, "Insert failed.");
            Assert.AreEqual(expected, actual, "Insert succeeded with PK violation.");

            teacherID = 1;
            studentID = 1;
            errorExpected = "Foreign key violation: Teacher does not exist";
            actual = Demerits.AddAssignedDemerit(teacherID, studentID, out error);
            Assert.AreEqual(errorExpected, error, "Error was something different! Or, it worked!");
            Assert.AreEqual(expected, actual, "Insert succeeded when Teacher was wrong.");

            teacherID = 10;
            studentID = 10;
            errorExpected = "Foreign key violation: Student does not exist";
            actual = Demerits.AddAssignedDemerit(teacherID, studentID, out error);
            Assert.AreEqual(errorExpected, error, "Error was something different! Or, it worked!");
            Assert.AreEqual(expected, actual, "Insert succeeded when student was wrong.");
        }

        /// <summary>
        ///A test for ADtoDemeritList
        ///</summary>
        [TestMethod()]
        public void ADtoDemeritListTest()
        {
            int adID = 104;
            int demeritID = 3;
            string error = "None";
            string errorExpected = "None";
            bool expected = true;
            bool actual;
            actual = Demerits.ADtoDemeritList(adID, demeritID, out error);
            Assert.AreEqual(errorExpected, error, "Error happened, but was not supposed to...");
            Assert.AreEqual(expected, actual, "Something did not work.");

            adID = 129;
            demeritID = 3;
            expected = false;
            errorExpected = "Foreign key violation: Assigned Demerit does not exist.";
            actual = Demerits.ADtoDemeritList(adID, demeritID, out error);
            Assert.AreEqual(errorExpected, error, "Assigned Demerit Error did not happen, but was supposed to...");
            Assert.AreEqual(expected, actual, "Something worked.");

            adID = 104;
            demeritID = 123;
            expected = false;
            errorExpected = "Foreign key violation: Demerit does not exist.";
            actual = Demerits.ADtoDemeritList(adID, demeritID, out error);
            Assert.AreEqual(errorExpected, error, "Demerit Error did not happen, but was supposed to...");
            Assert.AreEqual(expected, actual, "Something worked.");
        }

        /// <summary>
        ///A test for GetDemeritInformation
        ///</summary>
        [TestMethod()]
        public void GetDemeritInformationTest()
        {
            int demeritID = 104;
            int expected = 3;
            DataSet actual;
            actual = Demerits.GetDemeritInformation(demeritID);
            int actualRowCount = actual.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actualRowCount, "There is a disconnect between the result sets.");

            demeritID = 112;
            expected = 0;
            actual = Demerits.GetDemeritInformation(demeritID);
            actualRowCount = actual.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actualRowCount, "There is a disconnect between the result sets.");
        }

        /// <summary>
        ///A test for StudentDemeritList
        ///</summary>
        [TestMethod()]
        public void GetStudentDemeritListTest()
        {
            Nullable<int> userID = new Nullable<int>();
            int expected = 10;
            DataSet actual;
            actual = Demerits.GetStudentDemeritList(userID);
            int actualRowCount = actual.Tables[0].Rows.Count;
            Assert.IsTrue(expected <= actualRowCount, "There are is a really odd disconnect here.");

            userID = 7;
            expected = 2;
            actual = Demerits.GetStudentDemeritList(userID);
            actualRowCount = actual.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actualRowCount, "User #7 has more (or less) than 2 Demerits.");
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            
        }
    }
}
