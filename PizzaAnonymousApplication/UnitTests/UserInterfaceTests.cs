using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PizzaAnonymousApplication;
using System.IO;

namespace UnitTests
{
    [TestFixture]
    class UserInterfaceTests
    {

        [Test] // Positive test for get string
        public void getString()
        {
            String testString = "This is a test";

            StringReader reader = new StringReader(testString + Environment.NewLine);
            Console.SetIn(reader);

            Assert.AreEqual(testString, UserInterface.getString("Enter a String: "));
        }

        [Test] // Negative test for get string
        public void getStringOutOfRange()
        {
            String invalidString = "This is a test";
            String validString = "valid";

            StringReader reader = new StringReader(invalidString + Environment.NewLine +
                                                   validString + Environment.NewLine);
            Console.SetIn(reader);

            Assert.AreEqual(validString, UserInterface.getString("Enter a String: ", 1, 5));
        }

        [Test] // Positive test for get integer
        public void getInteger()
        {
            int testInteger = 10;

            StringReader reader = new StringReader(testInteger + Environment.NewLine);
            Console.SetIn(reader);

            Assert.AreEqual(testInteger, UserInterface.getInteger("Enter an Integer: "));
        }

        [Test] // Negative test for get integer
        public void getIntegerOutOfRange()
        {
            int invalidInteger = 10;
            int validInteger = 1000;

            StringReader reader = new StringReader(invalidInteger + Environment.NewLine +
                                                   validInteger + Environment.NewLine);
            Console.SetIn(reader);

            Assert.AreEqual(validInteger, UserInterface.getInteger("Enter an Integer: ", 4, 4));
        }

        [Test] // Positive test for get double
        public void getDouble()
        {
            double testDouble = 9.99;

            StringReader reader = new StringReader(testDouble + Environment.NewLine);
            Console.SetIn(reader);

            Assert.AreEqual(testDouble, UserInterface.getDouble("Enter a Double: "));
        }

        [Test] // Negative test for get double
        public void getDoubleOutOfRange()
        {
            double invalidDouble = 9.99;
            double validDouble = 12.02;

            StringReader reader = new StringReader(invalidDouble + Environment.NewLine +
                                                   validDouble + Environment.NewLine);
            Console.SetIn(reader);

            Assert.AreEqual(validDouble, UserInterface.getDouble("Enter a Double: ", 10.00, 13.15));
        }

        [Test] // Positive test for get date
        public void getDate()
        {
            String testDate = "11-12-2014";

            StringReader reader = new StringReader(testDate + Environment.NewLine);
            Console.SetIn(reader);

            Assert.AreEqual(testDate, UserInterface.getDate("Enter a Date: "));
        }

        [Test] // Negative test for get date
        public void getDateInvalidEntry()
        {
            String invalidDate = "111-12-2014";
            String validDate = "12-12-2014";

            StringReader reader = new StringReader(invalidDate + Environment.NewLine +
                                                   validDate + Environment.NewLine);
            Console.SetIn(reader);

            Assert.AreEqual(validDate, UserInterface.getDate("Enter a Date: "));
        }

        [Test] // Positive test for yes or no
        public void yesOrNoEnterYes()
        {
            String answer = "Yes";

            StringReader reader = new StringReader(answer + Environment.NewLine);
            Console.SetIn(reader);

            Assert.AreEqual(true, UserInterface.yesOrNo("Are you reading this? "));
        }

        [Test] // Positive test for yes or no
        public void yesOrNoEnterNo()
        {
            String answer = "No";

            StringReader reader = new StringReader(answer + Environment.NewLine);
            Console.SetIn(reader);

            Assert.AreEqual(false, UserInterface.yesOrNo("Are you reading this? "));
        }

        [Test] // Negative test for yes or no
        public void yesOrNoInvalidEntry()
        {
            String answer = "What?";

            StringReader reader = new StringReader(answer + Environment.NewLine);
            Console.SetIn(reader);

            // Make sure it prompts again
            Assert.AreEqual(false, UserInterface.yesOrNo("Are you reading this? "));
        }
    }
}
