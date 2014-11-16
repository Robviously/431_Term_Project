﻿using System;
using System.Collections.Generic;

/// <summary>
/// Provider Class: 
/// The provider class stores all of the data for each provider that uses the Pizza Anonymous system.
///  
/// Author: Ryan Schwartz
/// Date Created: November 6, 2014
/// Last Modified By: Ryan Schwartz
/// Date Last Modified: November 16, 2014
/// </summary>
public class Provider
{
    // Provider's name with get/set properties.
    private String name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    // Provider's id with get property. This value is uniquely autogenerated by ProviderManager and cannot be changed.
    private int id;
    public int Id
    {
        get { return id; }
    }

    // Provider's address with get/set properties.
    private String streetAddress;
    public String StreetAddress
    {
        get { return streetAddress; }
        set { streetAddress = value; }
    }

    // Provider's city with get/set properties.
    private String city;
    public String City
    {
        get { return city; }
        set { city = value; }
    }

    // Provider's state with get/set properties.
    private String state;
    public String State
    {
        get { return state; }
        set { state = value; }
    }

    // Provider's zip code with get/set properties.
    private int zipCode;
    public int ZipCode
    {
        get { return zipCode; }
        set { zipCode = value; }
    }

    // Provider's list of services provided (by code number).
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
    /// Checks whether the service is provided by the Provider
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
    /// This is the only constructor for Provider and must be passed parameters (no default constructor)
    /// </summary>
    /// <param name="n">Provider name</param>
    /// <param name="i">Generated ID</param>
    /// <param name="sa">Provider address</param>
    /// <param name="c">Provider city</param>
    /// <param name="s">Provider state</param>
    /// <param name="zc">Provider zip code</param>
    public Provider(String n, int i, String sa, String c, String s, int zc)
    {
        name = n;
        id = i;
        streetAddress = sa;
        city = c;
        state = s;
        zipCode = zc;
    }

    public override string ToString()
    {
        return "  Name:    " + name + "\n" +
               "  ID:      " + id + "\n" +
               "  Address: " + streetAddress + "\n" +
               "           " + city + ", " + state + " " + zipCode + "\n\n";
    }
}
