using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberClasses
{
    class member
    {
        //attributes 
        private int number;
        private string MemberName;
        private string MemberStreetAddress;
        private string MemberCity;
        private string MemberState;
        private int zipCode;
        private bool suspended;

        //constructors
        public member() { }
        public member(int num, string name, string street, string city, string state, int zip)
        {
            number = num;
            MemberName = name;
            MemberStreetAddress = street;
            MemberCity = city;
            MemberState = state;
            zipCode = zip;
            suspended = false;
        }

        //gets
        public int getNumber() { return number; }
        public string getMemberName() { return MemberName; }
        public string getMemberStreetAddress() { return MemberStreetAddress; }
        public string getMemberCity() { return MemberCity; }
        public string getMemberState() { return MemberState; }
        public int getZipCode() { return zipCode; }
        public bool getSuspended() { return suspended; }

        //sets
        public void setMemberName(string name) { MemberName = name; }
        public void setMemberStreetAddress(string street) { MemberStreetAddress = street; }
        public void setMemberCity(string city) { MemberCity = city; }
        public void setMemberState(string state) { MemberState = state; }
        public void setZipCode(int zip) { zipCode = zip; }
        public void setSuspended(bool value) { suspended = value; }




    }
}
