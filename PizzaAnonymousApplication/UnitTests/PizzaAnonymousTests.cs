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
        MemberManager memberManager;
        ProviderManager providerManager;
        ServiceManager serviceManager;
        PizzaAnonymous pizzaAnonymous;

        [SetUp] // This runs before each test, creating an instance of each manager class and creating an entity in each
        public void setup()
        {
            memberManager = new MemberManager();    
            providerManager = new ProviderManager();    
            serviceManager = new ServiceManager();
            pizzaAnonymous = new PizzaAnonymous(memberManager, providerManager, serviceManager);

            String memberString = "Member One" + Environment.NewLine +
                                  "M1 Street"  + Environment.NewLine +
                                  "M1 City"    + Environment.NewLine +
                                  "M1"         + Environment.NewLine +
                                  "11111"      + Environment.NewLine;

            String providerString = "Provider One" + Environment.NewLine +
                                    "P1 Street"    + Environment.NewLine +
                                    "P1 City"      + Environment.NewLine +
                                    "P1"           + Environment.NewLine +
                                    "11111"        + Environment.NewLine;

            String serviceString = "Service One" + Environment.NewLine +
                                   "9.99"        + Environment.NewLine +
                                   "Description Goes Here" + Environment.NewLine;

            // Create a member in the system
            StringReader mReader = new StringReader(memberString);
            Console.SetIn(mReader);
            pizzaAnonymous.addMember();

            // Create a provider in the system
            StringReader pReader = new StringReader(providerString);
            Console.SetIn(pReader);
            pizzaAnonymous.addProvider();

            // Create a service in the system
            StringReader sReader = new StringReader(serviceString);
            Console.SetIn(sReader);
            pizzaAnonymous.addService();

            Console.SetIn(Console.In);
        }

        [Test] // Positive test of editing a member's name
        public void editMemberName()
        {
            Member onlyMember = memberManager.MemberList.ElementAt(0);

            StringReader reader = new StringReader(onlyMember.Id + Environment.NewLine +
                                                   "name"        + Environment.NewLine +
                                                   "New Name"    + Environment.NewLine);
            Console.SetIn(reader);
            pizzaAnonymous.editMember();

            Assert.AreEqual("New Name", onlyMember.Name);
        }

        [Test] // Positive test of editing a member's address
        public void editMemberAddress()
        {
            Member onlyMember = memberManager.MemberList.ElementAt(0);

            StringReader reader = new StringReader(onlyMember.Id + Environment.NewLine +
                                                   "address"     + Environment.NewLine +
                                                   "New Street"  + Environment.NewLine +
                                                   "New City"    + Environment.NewLine +
                                                   "ZZ"          + Environment.NewLine +
                                                   "99999"       + Environment.NewLine);
            Console.SetIn(reader);
            pizzaAnonymous.editMember();

            Assert.AreEqual("New Street", onlyMember.StreetAddress);
            Assert.AreEqual("New City", onlyMember.City);
            Assert.AreEqual("ZZ", onlyMember.State);
            Assert.AreEqual("99999", onlyMember.ZipCode.ToString());
        }

        [Test] // Negative test of edit member that provides an invalid member ID
        public void editMemberInvalidId()
        {
            StringReader reader = new StringReader("999999999"  + Environment.NewLine +
                                                   "address"    + Environment.NewLine +
                                                   "New Street" + Environment.NewLine +
                                                   "New City"   + Environment.NewLine +
                                                   "ZZ"         + Environment.NewLine +
                                                   "99999"      + Environment.NewLine);
            Console.SetIn(reader);
            pizzaAnonymous.editMember();
        }

        [Test] // Negative test of edit member that provides an invalid field to edit
        public void editMemberInvalidField()
        {
            Member onlyMember = memberManager.MemberList.ElementAt(0);

            StringReader reader = new StringReader(onlyMember.Id + Environment.NewLine +
                                                   "ID"          + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.editMember();

            Assert.AreEqual("Member One", onlyMember.Name);
            Assert.AreEqual("100000000", onlyMember.Id.ToString());
            Assert.AreEqual("M1 Street", onlyMember.StreetAddress);
            Assert.AreEqual("M1 City", onlyMember.City);
            Assert.AreEqual("M1", onlyMember.State);
            Assert.AreEqual("11111", onlyMember.ZipCode.ToString());
        }

        [Test]
        public void deleteMemberValidID()
        {
            Member onlyMember = memberManager.MemberList.ElementAt(0);

            StringReader reader = new StringReader(onlyMember.Id + Environment.NewLine +
                                                   "ID"          + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.editMember();

            Assert.AreEqual("Member One", onlyMember.Name);
            Assert.AreEqual("100000000", onlyMember.Id.ToString());
            Assert.AreEqual("M1 Street", onlyMember.StreetAddress);
            Assert.AreEqual("M1 City", onlyMember.City);
            Assert.AreEqual("M1", onlyMember.State);
            Assert.AreEqual("11111", onlyMember.ZipCode.ToString());
        }
    }
}
