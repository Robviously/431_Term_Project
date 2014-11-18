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
    class MemberManagerTests
    {
        [Test]
        public void testAddMember()
        {
            MemberManager mm = new MemberManager();
            Console.Out.WriteLine("*** Create MemberManager, add 2 Members, and test MM.getMemberByID ***");
            mm.addMember("Jerry Test", "1234 test street", "Rosemount", "MN", 55068);
            mm.addMember("Franky tester", "1234 test ave", "St Cloud", "MN", 55068);
            Console.Out.WriteLine(mm);

            Assert.AreEqual(mm.MemberList.Count, 2);
        }

        [Test]
        public void testGetMemberById()
        {
            Console.Out.WriteLine("*** Attempting getMemberById with valid Member ID ***");
            MemberManager mm = new MemberManager();
            mm.addMember("Jerry Test", "1234 test street", "Rosemount", "MN", 55068);
            Console.Out.WriteLine(mm.getMemberById(100000000));

            Assert.IsInstanceOf<Member>(mm.getMemberById(100000000));
        }

        [Test]
        public void testInvalidGetMemberById()
        {
            Console.Out.WriteLine("*** Attempting getMemberById with valid Member ID ***");
            MemberManager mm = new MemberManager();
            mm.addMember("Jerry Test", "1234 test street", "Rosemount", "MN", 55068);

            Assert.IsNull(mm.getMemberById(100000005));
        }

        [Test]
        public void testValidateProvder()
        {
            Console.WriteLine("*** Test mm.validateMember for existing Provider ***");
            MemberManager mm = new MemberManager();
            mm.addMember("Jerry Test", "1234 test street", "Rosemount", "MN", 55068);

            Assert.IsTrue(mm.validateMember(100000000));
        }

        [Test]
        public void testInvalidValidateMember()
        {
            Console.Out.WriteLine("*** Test mm.validateMember for non-existing Member ***");
            MemberManager mm = new MemberManager();
            mm.addMember("Jerry Test", "1234 test street", "Rosemount", "MN", 55068);

            Assert.IsFalse(mm.validateMember(100000001));
        }

        [Test]
        public void testEditMemberName()
        {
            Console.Out.WriteLine("*** Test mm.editMemberName method ***");
            MemberManager mm = new MemberManager();
            mm.addMember("Jerry Test", "1234 test street", "Rosemount", "MN", 55068);

            string oldName = mm.getMemberById(100000000).Name;
            Console.WriteLine("Old Provider Info:\n" + mm.getMemberById(100000000));

            mm.editMemberName(100000000, "Frank Test");
            Console.WriteLine("New Provider Info:\n" + mm.getMemberById(100000000));

            Assert.IsFalse(oldName.Equals(mm.getMemberById(100000000).Name));
        }

        //    [Test]
        //    public void testEditProviderAddress()
        //    {
        //        Console.Out.WriteLine("*** Test PM.editProviderAddress method ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);

        //        string oldSA = pm.getProviderById(100000000).StreetAddress;
        //        string oldCity = pm.getProviderById(100000000).City;
        //        string oldState = pm.getProviderById(100000000).State;
        //        int oldZipCode = pm.getProviderById(100000000).ZipCode;
        //        Console.Out.WriteLine("Old Provider Info:\n" + pm.getProviderById(100000000));

        //        pm.editProviderAddress(100000000, "456 New Way", "Denver", "CO", 34567);
        //        Console.Out.WriteLine("New Provider Info:\n" + pm.getProviderById(100000000));

        //        Assert.IsFalse(oldSA.Equals(pm.getProviderById(100000000).StreetAddress));
        //        Assert.IsFalse(oldCity.Equals(pm.getProviderById(100000000).City));
        //        Assert.IsFalse(oldState.Equals(pm.getProviderById(100000000).State));
        //        Assert.IsFalse(oldZipCode == pm.getProviderById(100000000).ZipCode);
        //    }

        //    [Test]
        //    public void testDeleteProvider()
        //    {
        //        Console.Out.WriteLine("*** Test PM.deleteProvider, deleting ID 100000001 ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);
        //        pm.addProvider("Pizza = Terrible Clinic", "123 ABC St", "St. Cloud", "MN", 55555);

        //        Console.Out.WriteLine("Provider list before delete:" + pm);
        //        pm.deleteProvider(100000001);
        //        Console.Out.WriteLine("\nProvider list after delete:" + pm);

        //        Assert.AreEqual(pm.ProviderList.Count, 1);
        //    }

        //    [Test]
        //    public void testInvalidDeleteProvider()
        //    {
        //        Console.Out.WriteLine("*** Test PM.deleteProvider, deleting invalid provider ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);

        //        pm.deleteProvider(100000001);

        //        Assert.AreEqual(pm.ProviderList.Count, 1);
        //    }

        //    [Test]
        //    public void testAddService()
        //    {
        //        Console.Out.WriteLine("*** Add Services through PM.addService and test PM.getAllServices ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);

        //        pm.addService(100000000, 123456);
        //        pm.addService(100000000, 234567);
        //        pm.addService(100000000, 345678);
        //        pm.addService(100000000, 456789);
        //        pm.addService(100000000, 567890);

        //        Console.Out.WriteLine("Services for Provider 100000000:");
        //        List<int> services = pm.getAllServices(100000000);
        //        foreach (int i in services)
        //        {
        //            Console.Out.WriteLine(i);
        //        }

        //        Assert.AreEqual(pm.getAllServices(100000000).Count, 5);
        //    }

        //    [Test]
        //    public void testInvalidProviderGetAllServices()
        //    {
        //        Console.Out.WriteLine("*** Test PM.getAllServices with invalid provider ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);

        //        Assert.IsNull(pm.getAllServices(100000001));
        //    }

        //    [Test]
        //    public void testInvalidServiceAddService()
        //    {
        //        Console.Out.WriteLine("*** Add duplicate service 123456 to Provider 100000000 ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);
        //        pm.addService(100000000, 123456);
        //        pm.addService(100000000, 123456);

        //        Assert.AreEqual(pm.getAllServices(100000000).Count, 1);
        //    }

        //    [Test]
        //    public void testInvalidProviderAddService()
        //    {
        //        Console.Out.WriteLine("*** Add service 123456 to invalid provider ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);
        //        pm.addService(100000001, 123456);

        //        Assert.IsNull(pm.getAllServices(100000001));
        //    }

        //    [Test]
        //    public void testValidateService()
        //    {
        //        Console.Out.WriteLine("*** Test PM.validateService for existing Service in Provider 100000000 ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);
        //        pm.addService(100000000, 123456);

        //        Assert.IsTrue(pm.validateService(100000000, 123456));
        //    }

        //    [Test]
        //    public void testInvalidServiceValidateService()
        //    {
        //        Console.Out.WriteLine("*** Test PM.validateService for non-existing Service in Provider 100000000 ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);
        //        pm.addService(100000000, 123456);

        //        Assert.IsFalse(pm.validateService(100000000, 654321));
        //    }

        //    [Test]
        //    public void testInvalidProviderValidateService()
        //    {
        //        Console.Out.WriteLine("*** Test PM.validateService for non-existing Provider ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);
        //        pm.addService(100000000, 123456);

        //        Assert.IsFalse(pm.validateService(100000001, 123456));
        //    }

        //    [Test]
        //    public void testDeleteService()
        //    {
        //        Console.Out.WriteLine("*** Delete Service 123456 ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);
        //        pm.addService(100000000, 123456);
        //        pm.addService(100000000, 234567);

        //        Console.Out.WriteLine("Provider services before delete:\n");
        //        List<int> services = pm.getAllServices(100000000);
        //        foreach (int i in services)
        //        {
        //            Console.Out.WriteLine(i);
        //        }

        //        pm.deleteService(100000000, 123456);

        //        Console.Out.WriteLine("Provider services after delete:\n");
        //        services = pm.getAllServices(100000000);
        //        foreach (int i in services)
        //        {
        //            Console.Out.WriteLine(i);
        //        }

        //        Assert.AreEqual(pm.getAllServices(100000000).Count, 1);
        //    }

        //    [Test]
        //    public void testInvalidServiceDeleteService()
        //    {
        //        Console.Out.WriteLine("*** Delete invalid service ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);
        //        pm.addService(100000000, 123456);
        //        pm.deleteService(100000000, 654321);

        //        Assert.AreEqual(pm.getAllServices(100000000).Count, 1);
        //    }

        //    [Test]
        //    public void testInvalidProviderDeleteService()
        //    {
        //        Console.Out.WriteLine("*** Delete service 123456 from invalid provider ***");
        //        ProviderManager pm = new ProviderManager();
        //        pm.addProvider("This Is Why You're Fat Clinic", "15452 Zodiac St NE", "Forest Lake", "MN", 55025);
        //        pm.addService(100000000, 123456);

        //        pm.deleteService(100000001, 123456);

        //        Assert.IsNull(pm.getAllServices(100000001));
        //    }
    }
}

