using MIS2010Group1ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace MIS2010Group1TestProject
{
    
    
    /// <summary>
    ///This is a test class for CommentsTest and is intended
    ///to contain all CommentsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CommentsTest
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
        ///A test for GetComments
        ///</summary>
        [TestMethod()]
        public void GetCommentsTest()
        {
            Nullable<int> adID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> userID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            int expected = 6; // TODO: Initialize to an appropriate value
            DataSet actual;
            actual = Comments.GetComments(adID, userID);
            int actualRowCount = actual.Tables[0].Rows.Count; //Previously the error was reading as if this was a dataset
            Assert.AreEqual(expected, actualRowCount, "Error! ERROR! OH GOD WHY???");

            expected = 1;
            adID = 107;
            actual = Comments.GetComments(adID, userID);
            actualRowCount = actual.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actualRowCount, "Error with AssignedDemeritID specified.");

            expected = 1;
            userID = 1;
            actual = Comments.GetComments(adID, userID);
            actualRowCount = actual.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actualRowCount, "Error with userID specified.");


            adID = 104;
            userID = 1;
            actual = Comments.GetComments(adID, userID);
            actualRowCount = actual.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actualRowCount, "Error with both AssignedDemeritID and userID specified.");
        }

        /// <summary>
        ///A test for procAddNewComment
        ///</summary>
        [TestMethod()]
        public void AddNewCommentTest()
        {
            string description = "This is a TEST comment.";
            int adID = 104;
            Nullable<int> commentLink = new Nullable<int>(); // TODO: Initialize to an appropriate value
            int userID = 1;
            string error = string.Empty; // TODO: Initialize to an appropriate value
            string errorExpected = "None";
            bool expected = true;
            bool actual;
            actual = Comments.AddNewComment(description, adID, commentLink, userID, out error);
            Assert.AreEqual(errorExpected, error, "THERE WAS AN ERROR with Regular Comments");
            Assert.AreEqual(expected, actual, "Regular Comment test unsuccessful");

            commentLink = 1; // TODO: Initialize to an appropriate value
            actual = Comments.AddNewComment(description, adID, commentLink, userID, out error);
            Assert.AreEqual(errorExpected, error, "THERE WAS AN ERROR with Linked Comments");
            Assert.AreEqual(expected, actual, "Linked Comment test unsuccessful");
        }
    }
}
