using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberClasses
{
    static class memberManager
    {
        //attributes 
        private static List<member> memberList = new List<member>();
        private static int nextMemberNumber = 0;

        //function to load in next ID. only called on startup
        public static void LoadNextMemberNumber(int number)
        {
            nextMemberNumber = number;
        }
        //function returns next ID. called durring save
        public static int getNextMemberNumber()
        {
            return nextMemberNumber;
        }

        //returns member object if found
        public static member getMember(int number)
        {
            foreach(member M in memberList)
            {
                if (M.getNumber() == number)
                    return M;
            }
            return new member(); //if not found return empty object
        }

        
        public static string validateMember(int number)
        {
            member checkMember = getMember(number);

            if(checkMember.getMemberName() == null)//if checkMember isnt found values will be null.
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
            nextMemberNumber++;//update next member number
        }
        public static void deleteMember(int number)
        {
            member memberToDelete = getMember(number);
            memberList.Remove(memberToDelete);  //if member isnt found do nothing
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
                memberToEdit.setMemberState(newState);
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
