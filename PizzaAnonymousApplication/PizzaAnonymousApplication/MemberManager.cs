using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

/// <summary>
/// MemberManager Class: 
/// The MemberManager class contains a list of all of the members in the system and has the methods to
/// perform operations on them.
///  
/// Author: Matt Lauter
/// Date Created: November 6, 2014
/// Last Modified By: Matt Lauter
/// Date Last Modified: November 16, 2014
/// </summary>
public class MemberManager
{
    // This is the list of members.
    private List<Member> memberList = new List<Member>();
    public List<Member> MemberList
    {
        get { return memberList; }
    }

    // This int is for generating new unique member IDs.
    int nextID = 100000000;

    /// <summary>
    /// This adds a new member to the memberList based on passed arguments. The member ID is generated from nextID.
    /// </summary>
    /// <param name="name">Member Name</param>
    /// <param name="streetAddress">Member Street Address</param>
    /// <param name="city">Member City</param>
    /// <param name="state">Member State</param>
    /// <param name="zipCode">Member Zip Code</param>
    public void addMember(String name, String streetAddress, String city, String state, int zipCode)
    {
        // Creates a new member to add to the system.  Note: nextID is incremented.
        Member member = new Member(name, nextID++, streetAddress, city, state, zipCode);
        memberList.Add(member);
    }

    /// <summary>
    /// This is one of two edit methods.  This allows the member's name to be modified.
    /// </summary>
    /// <param name="id">Member ID</param>
    /// <param name="name">New Member Name</param>
    public void editMemberName(int id, String name)
    {
        // Ensure member exists in the system.
        if (!validateMember(id))
            Console.Out.WriteLine("Member with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through members until a match is found, then replace the member's name.
            foreach (Member m in memberList)
            {
                if (m.Id == id)
                {
                    m.Name = name;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// This is the other edit method to change the member address.
    /// </summary>
    /// <param name="id">Member ID</param>
    /// <param name="streetAddress">Member Street Address</param>
    /// <param name="city">Member City</param>
    /// <param name="state">Member State</param>
    /// <param name="zipCode">Member Zip Code</param>
    public void editMemberAddress(int id, String streetAddress, String city, String state, int zipCode)
    {
        // Ensure the member exists in the system
        if (!validateMember(id))
            Console.Out.WriteLine("Member with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through members until a match is found, then replace address elements.
            foreach (Member m in memberList)
            {
                if (m.Id == id)
                {
                    m.StreetAddress = streetAddress;
                    m.City = city;
                    m.State = state;
                    m.ZipCode = zipCode;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// This method removes a member from the memberList
    /// </summary>
    /// <param name="id">Member ID</param>
    public void deleteMember(int id)
    {
        // Ensure member exists in the system.
        if (!validateMember(id))
            Console.Out.WriteLine("Member with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through members until a match is found and then remove the member from memberList.
            foreach (Member m in memberList)
            {
                if (m.Id == id)
                {
                    memberList.Remove(m);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// This returns an actual member object from the memberList.
    /// </summary>
    /// <param name="id">Member ID</param>
    /// <returns>The member with the passed ID or null if it doesn't exist</returns>
    public Member getMemberById(int id)
    {
        // Ensure the member exists in the system.
        if (!validateMember(id))
            Console.Out.WriteLine("Member with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through members until a match is found, then return that member.
            foreach (Member m in memberList)
            {
                if (m.Id == id)
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
        // Cycle through members.  If a match is found, return true.
        foreach (Member m in memberList)
        {
            if (m.Id == id)
                return true;
        }

        // Otherwise return false.
        return false;
    }

    /// <summary>
    /// Saves the contents of this class to an XML file so it can be recovered at a later time.
    /// </summary>
    public void save()
    {
        // Path to the file in which the information will be stored in XML
        String file = "MemberManager.xml";

        // Create an XmlWriterSettings object with the correct options. 
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.IndentChars = ("\t");
        settings.OmitXmlDeclaration = true;
        settings.NewLineChars = "\r\n";
        settings.NewLineHandling = NewLineHandling.Replace;

        // Create the XML document writer and perform the writing
        using (XmlWriter writer = XmlWriter.Create(file, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("MemberManager");
            writer.WriteElementString("NextID", nextID.ToString());
            writer.WriteStartElement("Members");

            // Write the contents of each member in memberList to the file
            foreach (Member member in memberList)
            {
                writer.WriteStartElement("Member");
                writer.WriteElementString("Name", member.Name);
                writer.WriteElementString("ID", member.Id.ToString());
                writer.WriteElementString("StreetAddress", member.StreetAddress);
                writer.WriteElementString("City", member.City);
                writer.WriteElementString("State", member.State);
                writer.WriteElementString("ZIPCode", member.ZipCode.ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }

    /// <summary>
    /// Loads the contents of a previously saved XML file into this instance of the class
    /// </summary>
    public void load()
    {
        // The path to the file to read from
        String file = "MemberManager.xml";

        // If there isn't a file to read from: exit, otherwise start reading.
        if (!File.Exists(file))
        {
            return;
        }
        else
        {
            Console.Out.WriteLine("Reading data in to MemberManager from file [" + file + "].");
        }

        // Create an XML reader for this file.
        using (XmlReader reader = XmlReader.Create(file))
        {
            // The variables should be overwritten when loading the information in.
            String name = "Undefined Name";
            int id = -1;
            String streetAddress = "Undefined Street Address";
            String city = "Undefined City";
            String state = "Undefined State";
            int zipCode = -1;

            // While there is information to read in the file
            while (reader.Read())
            {
                // If the read is a start tag
                if (reader.IsStartElement())
                {
                    // Get element name and switch on it.
                    switch (reader.Name)
                    {
                        case "NextID":
                            nextID = reader.ReadElementContentAsInt();
                            break;
                        case "Name":
                            name = reader.ReadElementContentAsString();
                            break;
                        case "ID":
                            id = reader.ReadElementContentAsInt();
                            break;
                        case "StreetAddress":
                            streetAddress = reader.ReadElementContentAsString();
                            break;
                        case "City":
                            city = reader.ReadElementContentAsString();
                            break;
                        case "State":
                            state = reader.ReadElementContentAsString();
                            break;
                        case "ZIPCode":
                            zipCode = reader.ReadElementContentAsInt();
                            break;
                    }
                }
                else
                {
                    // If the read is a closing member tag
                    if (reader.Name == "Member")
                    {
                        // Create the member and add it to the list of members
                        Member member = new Member(name, id, streetAddress, city, state, zipCode);
                        memberList.Add(member);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Creates a formatted string representation of this object.
    /// </summary>
    /// <returns>A string that represents the members in the member list</returns>
    public override string ToString()
    {
        string members = "";

        foreach (Member m in memberList)
        {
            members += m.ToString();
        }

        return members;
    }
}
