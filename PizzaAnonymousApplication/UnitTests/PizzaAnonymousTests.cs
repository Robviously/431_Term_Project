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

        [SetUp]
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

            StringReader mReader = new StringReader(memberString);
            Console.SetIn(mReader);
            pizzaAnonymous.addMember();

            StringReader pReader = new StringReader(providerString);
            Console.SetIn(pReader);
            pizzaAnonymous.addProvider();

            StringReader sReader = new StringReader(serviceString);
            Console.SetIn(sReader);
            pizzaAnonymous.addService();

            Console.SetIn(Console.In);
        }

        [Test]
        public void testEditMember()
        {
            pizzaAnonymous.printMembers();
            pizzaAnonymous.printProviders();
            pizzaAnonymous.printServices();
            /*
            StringReader reader2 = new StringReader(memberManager.MemberList.ElementAt(0).Id + Environment.NewLine +
                                                    "name" + Environment.NewLine +
                                                    "NewName" + Environment.NewLine);
            Console.SetIn(reader2);
            pizzaAnonymous.editMember();
            Assert.AreEqual(memberManager.MemberList.ElementAt(0).Name, "NewName"); */
        }
    }
}
