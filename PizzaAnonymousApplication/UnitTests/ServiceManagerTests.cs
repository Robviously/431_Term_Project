using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PizzaAnonymousApplication;

namespace UnitTests
{
    [TestFixture]
    class ServiceManagerTests
    {
        [Test]
        public void testAddService()
        {
            ServiceManager sm = new ServiceManager();
            Console.WriteLine("*** Create ServiceManager, add 3 Services, and test SM.getServiceByID ***");
            sm.addService("Someone's Physio Lab", 100.50, "Best Physio lab in town");
            sm.addService("Someone's Bio Lab", 99.50, "Best Bio lab in town");
            sm.addService("Someone's Chemo Lab", 110.50, "Best Chemo lab in town");
            Console.WriteLine("  Displaying Services  ");
            Console.WriteLine(sm);

            Assert.AreEqual(sm.ServiceList.Count, 3);
            Console.WriteLine("Expected 3 : Found [" + sm.ServiceList.Count + "]");
        }

        [Test]
        public void testGetServiceById()
        {
            Console.WriteLine("*** Attempting getServiceById with valid Service ID number ***");
            ServiceManager sm = new ServiceManager();
            sm.addService("Someone's Physio Lab", 100.50, "Best physio lab in town");
            sm.addService("Someone's Bio Lab", 99.50, "Best Bio lab in town");
            sm.addService("Someone's Chemo Lab", 110.50, "Best Chemo lab in town");
            Console.WriteLine("  Displaying Services  ");
            Console.WriteLine(sm);

            Console.WriteLine("Getting Service info associated with ID# 100000");
            Console.WriteLine(sm.getServiceById(100000));
            Console.WriteLine("Getting Service info associated with ID# 100002");
            Console.WriteLine(sm.getServiceById(100002));

            Assert.IsInstanceOf<Service>(sm.getServiceById(100000));
            Assert.IsInstanceOf<Service>(sm.getServiceById(100001));
            Assert.IsInstanceOf<Service>(sm.getServiceById(100002));
        }

        [Test]
        public void testInvalidGetServiceById()
        {
            Console.WriteLine("*** Attempting getServiceById with invalid Service ID number ***");
            ServiceManager sm = new ServiceManager();
            sm.addService("Someone's Physio Lab", 100.50, "Best physio lab in town");
            sm.addService("Someone's Bio Lab", 150, "Best physio lab in town");
            Console.WriteLine("  Displaying Services  ");
            Console.WriteLine(sm);

            Console.WriteLine("Getting Service info associated with ID# 100006");
            Assert.IsNull(sm.getServiceById(100006));
            Console.WriteLine("Getting Service info associated with ID# 1000003");
            Assert.IsNull(sm.getServiceById(1000003));
        }

        [Test]
        public void testValidateService()
        {
            Console.WriteLine("*** Test sm.validateService for existing Service ***");
            ServiceManager sm = new ServiceManager();
            sm.addService("Someone's Physio Lab", 100.50, "Best physio lab in town");
            sm.addService("Someone's Bio Lab", 99.50, "Best Bio lab in town");
            sm.addService("Someone's Chemo Lab", 110.50, "Best Chemo lab in town");
            Console.WriteLine("  Displaying Services  ");
            Console.WriteLine(sm);

            Assert.IsTrue(sm.validateService(100000));
            Console.WriteLine("Service ID 100000 is a valid ID");
            Assert.IsTrue(sm.validateService(100002));
            Console.WriteLine("Service ID 100002 is a valid ID");
        }

        [Test]
        public void testInvalidValidateService()
        {
            Console.WriteLine("*** Test sm.validateService for non-existing Service ***");
            ServiceManager sm = new ServiceManager();
            sm.addService("Someone's Physio Lab", 100.50, "Best physio lab in town");
            sm.addService("Someone's Bio Lab", 99.50, "Best Bio lab in town");
            sm.addService("Someone's Chemo Lab", 110.50, "Best Chemo lab in town");
            Console.WriteLine("  Displaying Services  ");
            Console.WriteLine(sm);

            Assert.IsFalse(sm.validateService(100005));
            Console.WriteLine("Service ID 100005 is not a valid ID");
            Assert.IsFalse(sm.validateService(233245555));
            Console.WriteLine("Service ID 233245555 is not a valid ID");
            Assert.IsFalse(sm.validateService(00348080));
            Console.WriteLine("Service ID 00348080 is not a valid ID");
        }

        [Test]
        public void testEditServiceName()
        {
            Console.WriteLine("*** Test sm.editServiceName method ***");
            ServiceManager sm = new ServiceManager();
            sm.addService("Someone's Physio Lab", 100.50, "Best physio lab in town");
            sm.addService("Someone's Bio Lab", 99.50, "Best Bio lab in town");
            sm.addService("Someone's Chemo Lab", 110.50, "Best Chemo lab in town");
            Console.WriteLine("  Displaying Services  ");
            Console.WriteLine(sm);

            string oldName1 = sm.getServiceById(100000).Name;
            string oldName2 = sm.getServiceById(100001).Name;

            Console.WriteLine("Updating Service Name associated with ID# 100000");
            sm.editServiceName(100000, "Nobody's Physio Lab");
            Console.WriteLine("New Service Info:\n" + sm.getServiceById(100000));

            Console.WriteLine("Updating Service Name associated with ID# 100001");
            sm.editServiceName(100001, "Nobody's Bio Lab");
            Console.WriteLine("New Service Info:\n" + sm.getServiceById(100001));

            Assert.IsFalse(oldName1.Equals(sm.getServiceById(100000).Name));
            Assert.IsFalse(oldName2.Equals(sm.getServiceById(100001).Name));

        }

        [Test]
        public void testEditServiceFee()
        {
            Console.WriteLine("*** Test sm.editServiceFee method ***");
            ServiceManager sm = new ServiceManager();
            sm.addService("Someone's Physio Lab", 100.50, "Best physio lab in town");
            sm.addService("Someone's Bio Lab", 99.50, "Best Bio lab in town");
            sm.addService("Someone's Chemo Lab", 110.50, "Best Chemo lab in town");
            Console.WriteLine("  Displaying Services  ");
            Console.WriteLine(sm);

            double oldFee1 = sm.getServiceById(100001).Fee;
            double oldFee2 = sm.getServiceById(100002).Fee;

            Console.WriteLine("Updating Service Fee associated with ID# 100001");
            sm.editServiceFee(100001, 200.25);
            Console.WriteLine("New Service Info:\n" + sm.getServiceById(100001));

            Console.WriteLine("Updating Service Fee associated with ID# 100002");
            sm.editServiceFee(100002, 999.99);
            Console.WriteLine("New Service Info:\n" + sm.getServiceById(100002));

            Assert.IsFalse(oldFee1.Equals(sm.getServiceById(100001).Fee));
            Assert.IsFalse(oldFee2.Equals(sm.getServiceById(100002).Fee));
        }

        [Test]
        public void testEditServiceDescription()
        {
            Console.WriteLine("*** Test sm.editServiceDescription method ***");
            ServiceManager sm = new ServiceManager();
            sm.addService("Someone's Physio Lab", 100.50, "Best physio lab in town");
            sm.addService("Someone's Bio Lab", 99.50, "Best Bio lab in town");
            sm.addService("Someone's Chemo Lab", 110.50, "Best Chemo lab in town");
            Console.WriteLine("  Displaying Services  ");
            Console.WriteLine(sm);

            string oldDesc1 = sm.getServiceById(100000).Description;
            string oldDesc2 = sm.getServiceById(100002).Description;

            Console.WriteLine("Updating Service Description associated with ID# 100000");
            sm.editServiceDescription(100000, "This is now worst Physio Lab in town");
            Console.WriteLine("New Service Info:\n" + sm.getServiceById(100000));

            Console.WriteLine("Updating Service Description associated with ID# 100002");
            sm.editServiceDescription(100002, "This is now worst Bio Lab in town");
            Console.WriteLine("New Service Info:\n" + sm.getServiceById(100001));

            Assert.IsFalse(oldDesc1.Equals(sm.getServiceById(100000).Description));
            Assert.IsFalse(oldDesc2.Equals(sm.getServiceById(100002).Description));

        }

        [Test]
        public void testDeleteService()
        {
            Console.WriteLine("*** Test sm.deleteService method, deleting existent Service ***");
            ServiceManager sm = new ServiceManager();
            sm.addService("Someone's Physio Lab", 100.50, "Best physio lab in town");
            sm.addService("Someone's Bio Lab", 99.50, "Best Bio lab in town");
            sm.addService("Someone's Chemo Lab", 110.50, "Best Chemo lab in town");

            Console.WriteLine("Service list before delete:\n" + sm);
            Console.WriteLine("Deleting service with ID# 100001");
            sm.deleteService(100001);
            Console.WriteLine("\nService list after delete:\n" + sm);

            Assert.AreEqual(sm.ServiceList.Count, 2);

            Console.WriteLine("Service list before delete:\n" + sm);
            Console.WriteLine("Deleting service with ID# 100000");
            sm.deleteService(100000);
            Console.WriteLine("\nService list after delete:\n" + sm);

            Assert.AreEqual(sm.ServiceList.Count, 1);
        }

        [Test]
        public void testInvalidDeleteService()
        {
            Console.WriteLine("*** Test sm.deleteService, deleting non-existent Service ***");
            ServiceManager sm = new ServiceManager();
            sm.addService("Someone's Physio Lab", 100.50, "Best physio lab in town");
            sm.addService("Someone's Bio Lab", 99.50, "Best Bio lab in town");
            sm.addService("Someone's Chemo Lab", 110.50, "Best Chemo lab in town");

            Console.WriteLine("Service list before delete:\n" + sm);
            Console.WriteLine("Deleting service with ID# 100000009");
            sm.deleteService(100000009);
            Console.WriteLine("\nService list after delete:\n" + sm);

            Assert.AreEqual(sm.ServiceList.Count, 3);

            Console.WriteLine("Service list before delete:\n" + sm);
            Console.WriteLine("Deleting service with ID# 293898");
            sm.deleteService(293898);
            Console.WriteLine("\nService list after delete:\n" + sm);

            Assert.AreEqual(sm.ServiceList.Count, 3);
        }
    }
}
