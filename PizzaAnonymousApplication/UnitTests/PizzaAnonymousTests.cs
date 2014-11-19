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

        [Test] // Positive test of delete member 
        public void deleteMemberValidID()
        {
            Member onlyMember = memberManager.MemberList.ElementAt(0);

            StringReader reader = new StringReader(onlyMember.Id + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.deleteMember();

            Assert.AreEqual(0, memberManager.MemberList.Count);
        }

        [Test] // Negative test of delete member that provides invalid member ID
        public void deleteMemberInvalidID()
        {
            Member onlyMember = memberManager.MemberList.ElementAt(0);

            StringReader reader = new StringReader("999999999" + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.deleteMember();

            Assert.AreNotEqual(0, memberManager.MemberList.Count);
        }

        [Test] // Positive test of validate member 
        public void validateMemberValidID()
        {
            Member onlyMember = memberManager.MemberList.ElementAt(0);

            StringReader reader = new StringReader(onlyMember.Id + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.validateMember();

            Assert.AreEqual(memberManager.MemberList.ElementAt(0), onlyMember);
        }

        [Test] // Negative test of validate member that provides an invalid ID
        public void validateMemberInvalidID()
        {
            Member onlyMember = memberManager.MemberList.ElementAt(0);

            StringReader reader = new StringReader(onlyMember.Id + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.validateMember();

            Assert.AreEqual(memberManager.MemberList.ElementAt(0), onlyMember);
        }

        // ***********************************************************

        [Test] // Positive test of editing a provider's name
        public void editProviderName()
        {
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);

            StringReader reader = new StringReader(onlyProvider.Id + Environment.NewLine +
                                                   "name"        + Environment.NewLine +
                                                   "New Name"    + Environment.NewLine);
            Console.SetIn(reader);
            pizzaAnonymous.editProvider();

            Assert.AreEqual("New Name", onlyProvider.Name);
        }

        [Test] // Positive test of editing a provider's address
        public void editProviderAddress()
        {
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);

            StringReader reader = new StringReader(onlyProvider.Id + Environment.NewLine +
                                                   "address"     + Environment.NewLine +
                                                   "New Street"  + Environment.NewLine +
                                                   "New City"    + Environment.NewLine +
                                                   "ZZ"          + Environment.NewLine +
                                                   "99999"       + Environment.NewLine);
            Console.SetIn(reader);
            pizzaAnonymous.editProvider();

            Assert.AreEqual("New Street", onlyProvider.StreetAddress);
            Assert.AreEqual("New City", onlyProvider.City);
            Assert.AreEqual("ZZ", onlyProvider.State);
            Assert.AreEqual("99999", onlyProvider.ZipCode.ToString());
        }

        [Test] // Negative test of edit provider that provides an invalid provider ID
        public void editProviderInvalidId()
        {
            StringReader reader = new StringReader("999999999"  + Environment.NewLine +
                                                   "address"    + Environment.NewLine +
                                                   "New Street" + Environment.NewLine +
                                                   "New City"   + Environment.NewLine +
                                                   "ZZ"         + Environment.NewLine +
                                                   "99999"      + Environment.NewLine);
            Console.SetIn(reader);
            pizzaAnonymous.editProvider();
        }

        [Test] // Negative test of edit provider that provides an invalid field to edit
        public void editProviderInvalidField()
        {
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);

            StringReader reader = new StringReader(onlyProvider.Id + Environment.NewLine +
                                                   "ID"          + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.editProvider();

            Assert.AreEqual("Provider One", onlyProvider.Name);
            Assert.AreEqual("100000000", onlyProvider.Id.ToString());
            Assert.AreEqual("P1 Street", onlyProvider.StreetAddress);
            Assert.AreEqual("P1 City", onlyProvider.City);
            Assert.AreEqual("P1", onlyProvider.State);
            Assert.AreEqual("11111", onlyProvider.ZipCode.ToString());
        }

        [Test] // Positive test of delete provider 
        public void deleteProviderValidID()
        {
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);

            StringReader reader = new StringReader(onlyProvider.Id + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.deleteProvider();

            Assert.AreEqual(0, providerManager.ProviderList.Count);
        }

        [Test] // Negative test of delete provider that provides invalid provider ID
        public void deleteProviderInvalidID()
        {
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);

            StringReader reader = new StringReader("999999999" + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.deleteProvider();

            Assert.AreNotEqual(0, providerManager.ProviderList.Count);
        }

        [Test] // Positive test of adding a service to a provider
        public void addServiceToProvider()
        {
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader = new StringReader(onlyProvider.Id + Environment.NewLine +
                                                   onlyService.Id + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.addServiceToProvider();

            Assert.True(onlyProvider.serviceExist(onlyService.Id));
        }

        [Test] // Negative test of adding a service to a provider with an invalid provider ID given
        public void addServiceToProviderInvalidProviderId()
        {
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader = new StringReader("999999999" + Environment.NewLine +
                                                   onlyService.Id + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.addServiceToProvider();

            Assert.False(onlyProvider.serviceExist(onlyService.Id));
        }

        [Test] // Negative test of adding a service to a provider with an invalid service ID given
        public void addServiceToProviderInvalidServiceId()
        {
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader = new StringReader(onlyProvider.Id + Environment.NewLine +
                                                   "999999"        + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.addServiceToProvider();

            Assert.False(onlyProvider.serviceExist(999999));
        }

        [Test] // Positive test of deleting a service from a provider
        public void deleteServiceFromProvider()
        {
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader1 = new StringReader(onlyProvider.Id + Environment.NewLine +
                                                   onlyService.Id + Environment.NewLine);

            Console.SetIn(reader1);
            pizzaAnonymous.addServiceToProvider();

            Assert.True(onlyProvider.serviceExist(onlyService.Id));

            StringReader reader2 = new StringReader(onlyProvider.Id + Environment.NewLine +
                                                    onlyService.Id + Environment.NewLine);

            Console.SetIn(reader2);
            pizzaAnonymous.deleteServiceFromProvider();

            Assert.False(onlyProvider.serviceExist(onlyService.Id));
        }

        [Test] // Negative test of deleting a service from a provider with invalid provider ID
        public void deleteServiceFromProviderInvalidProviderId()
        {
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader1 = new StringReader(onlyProvider.Id + Environment.NewLine +
                                                    onlyService.Id + Environment.NewLine);

            Console.SetIn(reader1);
            pizzaAnonymous.addServiceToProvider();

            Assert.True(onlyProvider.serviceExist(onlyService.Id));

            StringReader reader2 = new StringReader("999999999" + Environment.NewLine +
                                                    onlyService.Id + Environment.NewLine);

            Console.SetIn(reader2);
            pizzaAnonymous.deleteServiceFromProvider();

            Assert.True(onlyProvider.serviceExist(onlyService.Id));
        }

        [Test] // Negative test of deleting a service from a provider with invalid service ID
        public void deleteServiceFromProviderInvalidServiceId()
        {
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader1 = new StringReader(onlyProvider.Id + Environment.NewLine +
                                                    onlyService.Id + Environment.NewLine);

            Console.SetIn(reader1);
            pizzaAnonymous.addServiceToProvider();

            Assert.True(onlyProvider.serviceExist(onlyService.Id));

            StringReader reader2 = new StringReader(onlyProvider.Id + Environment.NewLine +
                                                    "999999" + Environment.NewLine);

            Console.SetIn(reader2);
            pizzaAnonymous.deleteServiceFromProvider();

            Assert.True(onlyProvider.serviceExist(onlyService.Id));
        }

        // **********************************************

        [Test] // Positive test of editing a service's name
        public void editServiceName()
        {
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader = new StringReader(onlyService.Id + Environment.NewLine +
                                                   "name"         + Environment.NewLine +
                                                   "New Name"     + Environment.NewLine);
            Console.SetIn(reader);
            pizzaAnonymous.editService();

            Assert.AreEqual("New Name", onlyService.Name);
        }

        [Test] // Positive test of editing a service's fee
        public void editServiceFee()
        {
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader = new StringReader(onlyService.Id + Environment.NewLine +
                                                   "fee"          + Environment.NewLine +
                                                   "18.18"        + Environment.NewLine);
            Console.SetIn(reader);
            pizzaAnonymous.editService();

            Assert.AreEqual("18.18", onlyService.Fee.ToString());
        }

        [Test] // Positive test of editing a service's description
        public void editServiceDescription()
        {
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader = new StringReader(onlyService.Id    + Environment.NewLine +
                                                   "description"     + Environment.NewLine +
                                                   "New Description" + Environment.NewLine);
            Console.SetIn(reader);
            pizzaAnonymous.editService();

            Assert.AreEqual("New Description", onlyService.Description);
        }

        [Test] // Negative test of edit service that provides an invalid service ID
        public void editServiceInvalidId()
        {
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader = new StringReader("999999" + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.editService();

            Assert.AreEqual("Service One", onlyService.Name);
            Assert.AreEqual("9.99", onlyService.Fee.ToString());
            Assert.AreEqual("Description Goes Here", onlyService.Description);
        }

        [Test] // Negative test of edit service that provides an invalid field to edit
        public void editServiceInvalidField()
        {
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader = new StringReader(onlyService.Id + Environment.NewLine +
                                                   "ID" + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.editService();

            Assert.AreEqual("Service One", onlyService.Name);
            Assert.AreEqual("100000", onlyService.Id.ToString());
            Assert.AreEqual("9.99", onlyService.Fee.ToString());
            Assert.AreEqual("Description Goes Here", onlyService.Description);
        }

        [Test] // Positive test of delete service 
        public void deleteServiceValidID()
        {
            Service onlyService = serviceManager.ServiceList.ElementAt(0);
            Provider onlyProvider = providerManager.ProviderList.ElementAt(0);

            StringReader reader1 = new StringReader(onlyProvider.Id + Environment.NewLine +
                                                    onlyService.Id + Environment.NewLine);

            Console.SetIn(reader1);
            pizzaAnonymous.addServiceToProvider();

            Assert.True(providerManager.validateService(onlyProvider.Id, onlyService.Id));

            StringReader reader2 = new StringReader(onlyService.Id + Environment.NewLine);

            Console.SetIn(reader2);
            pizzaAnonymous.deleteService();

            Assert.AreEqual(0, serviceManager.ServiceList.Count);
            Assert.False(providerManager.validateService(onlyProvider.Id, onlyService.Id));
        }

        [Test] // Negative test of delete service that provides invalid service ID
        public void deleteServiceInvalidID()
        {
            Service onlyService = serviceManager.ServiceList.ElementAt(0);

            StringReader reader = new StringReader("999999" + Environment.NewLine);

            Console.SetIn(reader);
            pizzaAnonymous.deleteService();

            Assert.AreNotEqual(0, serviceManager.ServiceList.Count);
        }
    }
}
