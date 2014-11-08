using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberClasses
{
    static class memberManager
    {
        private static List<member> memberList = new List<member>();
        private static int nextMemberNumber = 0;


        public static void LoadNextMemberNumber(int number)
        {
            nextMemberNumber = number;
        }

        public static member getMember(int number)
        {
            foreach(member M in memberList)
            {
                if (M.getNumber() == number)
                    return M;
            }
            return new member();
        }

        public static string validateMember(int number)
        {
            member checkMember = getMember(number);

            if(checkMember.getMemberName() == null)//check this
            {
                return "Invalid number";
            }
            else
            {
                if (checkMember.getSuspended() == true)
                    return "Member suspended";
                else
                    return "Validated";
            }

        }

        public static void addMember(string name, string street, string city, string state, int zip)
        {
            member newMember = new member(nextMemberNumber, name, street, city, state, zip);
            memberList.Add(newMember);
            nextMemberNumber++;
        }
        public static void deleteMember(int number)
        {
            member memberToDelete = getMember(number);
            memberList.Remove(memberToDelete);
        }

        public static void editMember(int number, string newName)
        {
            member memberToEdit = getMember(number);
            if(validateMember(number) == "Validated")
            {
                memberToEdit.setMemberName(newName);
            }
        }

        public static void editMember(int number, string newStreet,string newCity, string newState, int newZip )
        {
            member memberToEdit = getMember(number);
            if (validateMember(number) == "Validated")
            {
                memberToEdit.setMemberStreetAddress(newStreet);
                memberToEdit.setMemberCity(newCity);
                memberToEdit.setMemberState(newStreet);
                memberToEdit.setZipCode(newZip);
            }
        }


        public static void suspendMember(int number)
        {
            member memberToSuspend = getMember(number);
            if (validateMember(number) == "Validated")
            {
                memberToSuspend.setSuspended(true);
            }
        }

        public static void unSuspendMember(int number)
        {
            member memberToUnsuspend = getMember(number);
            if (validateMember(number) == "Member suspended")
            {
                memberToUnsuspend.setSuspended(false);
            }
        }

    }
}
