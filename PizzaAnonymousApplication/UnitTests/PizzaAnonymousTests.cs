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
    class PizzaAnonymousTests
    {
        [Test]
        public void testAddMember()
        {
            PizzaAnonymous pizzaAnonymous = PizzaAnonymous.instance();
            MemberManager memberManager = new MemberManager();
            int beforeCount = memberManager.MemberList.Count;

            StringReader reader = new StringReader("Name"   + Environment.NewLine +
                                                   "Street" + Environment.NewLine +
                                                   "City"   + Environment.NewLine +
                                                   "AB"     + Environment.NewLine +
                                                   "12345"  + Environment.NewLine);
            Console.SetIn(reader);

            pizzaAnonymous.addMember();
            int afterCount = memberManager.MemberList.Count;
            Console.WriteLine(beforeCount + ", " + afterCount);
            Assert.AreEqual(afterCount, beforeCount + 1);

        }
    }
}
