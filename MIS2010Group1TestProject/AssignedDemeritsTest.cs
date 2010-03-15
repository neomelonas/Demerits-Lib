using MIS2010Group1ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace MIS2010Group1TestProject
{
    
    
    /// <summary>
    ///This is a test class for AssignedDemeritsTest and is intended
    ///to contain all AssignedDemeritsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AssignedDemeritsTest
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
        ///A test for StudentDemeritList
        ///</summary>
        [TestMethod()]
        public void GetStudentDemeritListTest()
        {
            Nullable<int> userID = new Nullable<int>();
            int expected = 6;
            DataSet actual;
            actual = AssignedDemerits.GetStudentDemeritList(userID);
            int actualRowCount = actual.Tables[0].Rows.Count;
            Assert.IsTrue(expected <= actualRowCount, "There are are fewer Demerits than expected.");

            userID = 7;
            expected = 2;
            actual = AssignedDemerits.GetStudentDemeritList(userID);
            actualRowCount = actual.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actualRowCount, "User #7 has more (or less) than 2 Demerits.");
        }

        /// <summary>
        ///A test for GetDemeritInformation
        ///</summary>
        [TestMethod()]
        public void GetDemeritInformationTest()
        {
            int demeritID = 104;
            int expected = 1;
            DataSet actual;
            actual = AssignedDemerits.GetDemeritInformation(demeritID);
            int actualRowCount = actual.Tables[0].Rows.Count;
            Assert.IsTrue(expected <= actualRowCount, "There is a disconnect between the result sets.");

            demeritID = 112;
            expected = 0;
            actual = AssignedDemerits.GetDemeritInformation(demeritID);
            actualRowCount = actual.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actualRowCount, "There is a disconnect between the result sets.");
        }

        /// <summary>
        ///A test for AddAssignedDemerit
        ///</summary>
        [TestMethod()]
        public void AddAssignedDemeritTest()
        {
            int teacherID = 10;
            int studentID = 1;
            double weight = 0.5;
            string error = null;
            string errorExpected = "None";
            bool expected = true;
            bool actual;
            actual = AssignedDemerits.AddAssignedDemerit(teacherID, studentID, weight, out error);
            Assert.AreEqual(errorExpected, error, "Insert failed.");
            Assert.AreEqual(expected, actual, "Insert failed.");

            teacherID = 1;
            studentID = 1;
            expected = false;
            errorExpected = "Foreign key violation: Teacher does not exist";
            actual = AssignedDemerits.AddAssignedDemerit(teacherID, studentID, weight, out error);
            Assert.AreEqual(errorExpected, error, "Error was something different! Or, it worked!");
            Assert.AreEqual(expected, actual, "Insert succeeded when Teacher was wrong.");

            teacherID = 10;
            studentID = 10;
            expected = false;
            errorExpected = "Foreign key violation: Student does not exist";
            actual = AssignedDemerits.AddAssignedDemerit(teacherID, studentID, weight, out error);
            Assert.AreEqual(errorExpected, error, "Error was something different! Or, it worked!");
            Assert.AreEqual(expected, actual, "Insert succeeded when student was wrong.");
        }
        [TestCleanup()]
        public void ADTestCleanup()
        {
            AssignedDemerits.ADTestCleanup();
        }
    }
}
