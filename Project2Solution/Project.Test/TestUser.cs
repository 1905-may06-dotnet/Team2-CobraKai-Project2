using NUnit.Framework;

namespace Tests
{
    public class TestUser
    {
        [SetUp]
        public void Setup()
        {

        }

        //test log in is correct
        [Test]
        public void Create()
        {
            string username = "testuser";
            string password = "testpassword";
            string firstname = "First";
            string lastname = "Last";
            string email = "e@mail.com";
            bool testbool = false;

            /*
             * 
             * try {
             * Project.Data... user = new Project.Data...() {
             *     username = testuser
             *     password = testpassword
             *     firstname = First
             *     lastname = Last
             *     email = e@mail.com
             *  }
             *  testbool = true;
             * }
             * catch (Exception e) {
             * }
             * finally {
             *  Assert.IsTrue(testbool);
             * }
             * 
             */

        }

        [Test]
        public void Read()
        {
            string username = "testuser";
            string password = "testpassword";
            string firstname = "First";
            string lastname = "Last";
            string email = "e@mail.com";
            bool testbool = false;

            /*
             * try {
             * 
             * }
             * catch (Exception e) {
             * }
             * finally {
             * Assert.IsE
             * }
             */
        }

        [Test]
        public void Delete()
        {
            string username = "testuser";
            string password = "testpassword";
            string firstname = "First";
            string lastname = "Last";
            string email = "e@mail.com";
            bool testbool = false;
        }
    }
}