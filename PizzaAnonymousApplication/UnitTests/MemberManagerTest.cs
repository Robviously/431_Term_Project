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

        [Test]
        public void testEditMemberAddress()
        {
            string oldAddress, newAddress;
            Console.Out.WriteLine("*** Test mm.editMemberAddress method ***");
            MemberManager mm = new MemberManager();
            mm.addMember("Jerry Test", "1234 test street", "Rosemount", "MN", 55068);

            string oldSA = mm.getMemberById(100000000).StreetAddress;
            string oldCity = mm.getMemberById(100000000).City;
            string oldState = mm.getMemberById(100000000).State;
            int oldZipCode = mm.getMemberById(100000000).ZipCode;
            oldAddress = oldSA + " " + oldCity + " " + oldState + " " + oldZipCode;
            Console.Out.WriteLine("Old Provider Info:\n" + mm.getMemberById(100000000));

            mm.editMemberAddress(100000000, "222 different street", "St Cloud", "MN", 56301);
            Console.Out.WriteLine("New Provider Info:\n" + mm.getMemberById(100000000));
            newAddress = mm.getMemberById(100000000).StreetAddress + " " + mm.getMemberById(100000000).City +
                         " " + mm.getMemberById(100000000).State + " " + mm.getMemberById(100000000).ZipCode;

            Assert.IsFalse(oldAddress.Equals(newAddress));   
        }

        [Test]
        public void testDeleteMember()
        {
            int memberCount;
            Console.Out.WriteLine("*** Test mm.deleteMember, deleting ID 100000001 ***");
            MemberManager mm = new MemberManager();
            mm.addMember("Jerry Test", "1234 test street", "Rosemount", "MN", 55068);
            mm.addMember("Franky tester", "1234 test ave", "St Cloud", "MN", 55068);

            memberCount = mm.MemberList.Count;

            Console.WriteLine("Provider list before delete:" + mm);
            mm.deleteMember(100000001);
            Console.Out.WriteLine("\nProvider list after delete:" + mm);

            Assert.AreEqual(mm.MemberList.Count, memberCount-1);
        }

        [Test]
        public void testInvalidDeleteMember()
        {
            Console.WriteLine("*** Test mm.deleteMember, deleting invalid Member ***");
            MemberManager mm = new MemberManager();
            mm.addMember("Jerry Test", "1234 test street", "Rosemount", "MN", 55068);


            mm.deleteMember(100000001);

            Assert.AreEqual(mm.MemberList.Count, 1);
        } 
    }
}


