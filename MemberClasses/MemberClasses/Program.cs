using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            //basic test program
            //test Load of ID
            memberManager.LoadNextMemberNumber(100000000);
            //test adding members
            memberManager.addMember("person1", "test", "test2", "MN", 55068);
            memberManager.addMember("person2", "test", "test2", "MN", 55068);
            memberManager.addMember("person3", "test", "test2", "MN", 55068);
            memberManager.addMember("person4", "test", "test2", "MN", 55068);
            memberManager.addMember("person5", "test", "test2", "MN", 55068);
            memberManager.addMember("person6", "test", "test2", "MN", 55068);
            memberManager.addMember("person7", "test", "test2", "MN", 55068);
            memberManager.addMember("person8", "test", "test2", "MN", 55068);

            //test validate
            Console.WriteLine(memberManager.validateMember(100000001));
            //test delete
            memberManager.deleteMember(100000001);
            Console.WriteLine(memberManager.validateMember(100000001));
            Console.WriteLine(memberManager.validateMember(100000000));
            //test suspend
            memberManager.suspendMember(100000000);
            Console.WriteLine(memberManager.validateMember(100000000));
            //test unSuspend
            memberManager.unSuspendMember(100000000);
            Console.WriteLine(memberManager.validateMember(100000000));

            //test name Edit
            Console.WriteLine(memberManager.getMember(100000004).getMemberName());
            memberManager.editMember(100000004, "Edit1Test");
            Console.WriteLine(memberManager.getMember(100000004).getMemberName());

            //test address Edit
            Console.WriteLine(memberManager.getMember(100000004).getMemberStreetAddress());
            Console.WriteLine(memberManager.getMember(100000004).getMemberCity());
            Console.WriteLine(memberManager.getMember(100000004).getMemberState());
            Console.WriteLine(memberManager.getMember(100000004).getZipCode());
            memberManager.editMember(100000004, "street", "city","state",9999);
            Console.WriteLine(memberManager.getMember(100000004).getMemberStreetAddress());
            Console.WriteLine(memberManager.getMember(100000004).getMemberCity());
            Console.WriteLine(memberManager.getMember(100000004).getMemberState());
            Console.WriteLine(memberManager.getMember(100000004).getZipCode());


        }
    }
}
