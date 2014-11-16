﻿using System;
using System.Collections.Generic;
using Reports;

/// <summary>
/// Member Class: 
/// The member class stores all of the data for each member that uses the Pizza Anonymous system.
///  
/// Author: Ryan Schwartz
/// Date Created: November 6, 2014
/// Last Modified By: Ryan Schwartz
/// Date Last Modified: November 6, 2014
/// </summary>
public class Provider
{
    // Member's name with get/set properties.
    private String name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    // Member's id with get property. This value is uniquely autogenerated by MemberManager and cannot be changed.
    private int id;
    public int Id
    {
        get { return id; }
    }

    // Member's address with get/set properties.
    private String streetAddress;
    public String StreetAddress
    {
        get { return streetAddress; }
        set { streetAddress = value; }
    }

    // Member's city with get/set properties.
    private String city;
    public String City
    {
        get { return city; }
        set { city = value; }
    }

    // Member's state with get/set properties.
    private String state;
    public String State
    {
        get { return state; }
        set { state = value; }
    }

    // Member's zip code with get/set properties.
    private int zipCode;
    public int ZipCode
    {
        get { return zipCode; }
        set { zipCode = value; }
    }

    // Member's list of services provided (by code number).
    private List<int> serviceList = new List<int>();

    /// <summary>
    /// This simply returns the entire service list.
    /// </summary>
    /// <returns>Full list of service codes</returns>
    public List<int> getServiceList()
    {
        return serviceList;
    }

    /// <summary>
    /// Checks whether the service is provided by the Member
    /// </summary>
    /// <param name="code">Service code</param>
    /// <returns>Whether or not code exists in serviceList.</returns>
    public bool serviceExist(int code)
    {
        return serviceList.Contains(code) ? true : false;
    }

    /// <summary>
    /// Adds a service to the serviceList (if not already provided)
    /// </summary>
    /// <param name="code">Service Code</param>
    public void addService(int code)
    {
        if (!serviceList.Contains(code))
            serviceList.Add(code);
    }

    /// <summary>
    /// Removes a service from the serviceList (if exists)
    /// </summary>
    /// <param name="code">Service code</param>
    public void removeService(int code)
    {
        if (serviceList.Contains(code))
            serviceList.Remove(code);
    }

    /// <summary>
    /// This is the only constructor for Member and must be passed parameters (no default constructor)
    /// </summary>
    /// <param name="n">Member name</param>
    /// <param name="i">Generated ID</param>
    /// <param name="sa">Member address</param>
    /// <param name="c">Member city</param>
    /// <param name="s">Member state</param>
    /// <param name="zc">Member zip code</param>
    public Provider(String n, int i, String sa, String c, String s, int zc)
    {
        name = n;
        id = i;
        streetAddress = sa;
        city = c;
        state = s;
        zipCode = zc;
    }

    public String toString()
    {
        return "  Name:    " + name + "\n" +
               "  ID:      " + id + "\n" +
               "  Address: " + streetAddress + "\n" +
               "           " + city + ", " + state + " " + zipCode + "\n\n";
    }
}
