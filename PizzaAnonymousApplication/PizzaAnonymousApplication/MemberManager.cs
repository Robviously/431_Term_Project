using System;
using System.Collections.Generic;

/// <summary>
/// MemberManager Class: 
/// The MemberManager class contains a list of all of the members in the system and has the methods to
/// perform operations on them.
///  
/// Author: Ryan Schwartz
/// Date Created: November 6, 2014
/// Last Modified By: Ryan Schwartz
/// Date Last Modified: November 6, 2014
/// </summary>
public class MemberManager
{

    private static MemberManager memberManager;

    private MemberManager()
    {
    }

    public static MemberManager instance()
    {
        if (memberManager == null)
        {
            return memberManager = new MemberManager();
        }
        else
        {
            return memberManager;
        }
    }

    // This is the list of members.
    private List<Member> memberList = new List<Member>();

    // This int is for generating new member IDs.
    int nextId = 100000000;

    /// <summary>
    /// This adds a new member to the memberList based on passed arguments.  The member ID is generated from nextId.
    /// </summary>
    /// <param name="name">Member Name</param>
    /// <param name="streetAddress">Member Address</param>
    /// <param name="city">Member City</param>
    /// <param name="state">Member State</param>
    /// <param name="zipCode">Member Zip Code</param>
    public void addMember(String name, String streetAddress, String city, String state, int zipCode)
    {
        // Creates a new member to add to the system.  Note: nextID is incremented.
        Member member = new Member(name, nextId++, streetAddress, city, state, zipCode);
        memberList.Add(member);
    }

    /// <summary>
    /// This allows the member's name to be modified.
    /// </summary>
    /// <param name="id">Member ID</param>
    /// <param name="name">New Member Name</param>
    public void editMemberName(int id, String name)
    {
        // Get the member object using its ID
        Member member = getMemberById(id);
        
        if (member != null)
        {
            member.Name = name;
        }
    }

    /// <summary>
    /// This allows the member's address to be modified.
    /// </summary>
    /// <param name="id">Member ID</param>
    /// <param name="streetAddress">Member address</param>
    /// <param name="city">Member city</param>
    /// <param name="state">Member state</param>
    /// <param name="zipCode">Member zip code</param>
    public void editMemberAddress(int id, String streetAddress, String city, String state, int zipCode)
    {
        // Get the member object using its ID
        Member member = getMemberById(id);

        if (member != null)
        {
            member.StreetAddress = streetAddress;
            member.City = city;
            member.State = state;
            member.ZipCode = zipCode;
        }
    }

    /// <summary>
    /// This method removes a member from the memberList
    /// </summary>
    /// <param name="id">Member ID</param>
    public void deleteMember(int id)
    {
        // Get the member object using its ID
        Member member = getMemberById(id);
        
        if (member != null)
        {
            memberList.Remove(member);
        }
    }

    /// <summary>
    /// This returns an actual member object from the memberList.
    /// </summary>
    /// <param name="id">Member ID</param>
    /// <returns>The member with the passed ID or null if it doesn't exist</returns>
    public Member getMemberById(int id)
    {
        // Cycle through members until a match is found, then return that member.
        foreach (Member m in memberList)
        {
            if (m.Id == id)
            {
                return m;
            }
        }

        // If member doesn't exist, return null.
        return null;
    }

    /// <summary>
    /// Determines whether a member with the passed ID exists in the system
    /// </summary>
    /// <param name="id">Member ID</param>
    /// <returns>True if the member exists in the system, otherwise false.</returns>
    public bool validateMember(int id)
    {
        if (getMemberById(id) != null)
        {
            return true;
        }

        // Otherwise return false.
        return false;
    }

    public String toString()
    {
        String members = "";

        foreach (Member m in memberList)
        {
            members = members + m.toString();
        }

        return members;
    }
}
