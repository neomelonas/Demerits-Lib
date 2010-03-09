using MIS2010Group1ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace MIS2010Group1TestProject
{
    
    
    /// <summary>
    ///This is a test class for DetentionTest and is intended
    ///to contain all DetentionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DetentionTest
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
        ///A test for GetStudentDetentions
        ///</summary>
        [TestMethod()]
        public void GetStudentDetentionsTest()
        {
            Nullable<int> studentID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            bool servedStatus = false; // TODO: Initialize to an appropriate value
            int expected = 4; // TODO: Initialize to an appropriate value
            int actualNumberOfRows;
            DataSet actual;
            actual = Detention.GetStudentDetentions(studentID, servedStatus);
            actualNumberOfRows = actual.Tables[0].Rows.Count;
            Assert.IsTrue(expected <= actualNumberOfRows, "There are more rows than expected.");

            studentID = 7;
            servedStatus = false;
            expected = 1;
            actual = Detention.GetStudentDetentions(studentID, servedStatus);
            actualNumberOfRows = actual.Tables[0].Rows.Count;
            Assert.IsTrue(expected <= actualNumberOfRows, "There are a different number of rows.");

            studentID = 7;
            servedStatus = true;
            expected = 1;
            actual = Detention.GetStudentDetentions(studentID, servedStatus);
            actualNumberOfRows = actual.Tables[0].Rows.Count;
            Assert.IsTrue(expected <= actualNumberOfRows, "There are a different number of rows.");
        }
    }
}
