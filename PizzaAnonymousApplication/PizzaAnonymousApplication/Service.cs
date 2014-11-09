﻿using System;
using System.Collections.Generic;

/// <summary>
/// Service Class: 
/// The service class stores all of the data for each service that uses the Pizza Anonymous system.
///  
/// Author: Ryan Schwartz
/// Date Created: November 6, 2014
/// Last Modified By: Ryan Schwartz
/// Date Last Modified: November 6, 2014
/// </summary>
public class Service
{
    // Service's name with get/set properties.
    private String name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    // Service's id with get property. This value is uniquely autogenerated by ServiceManager and cannot be changed.
    private int id;
    public int Id
    {
        get { return id; }
    }

    // Service's fee with get/set properties.
    private float fee;
    public float Fee
    {
        get { return fee; }
        set { fee = value; }
    }

    // Service's description with get/set properties.
    private String description;
    public String Description
    {
        get { return description; }
        set { description = value; }
    }

    /// <summary>
    /// This is the only constructor for Service and must be passed parameters (no default constructor)
    /// </summary>
    /// <param name="n">Service name</param>
    /// <param name="i">Generated ID</param>
    /// <param name="sa">Service address</param>
    /// <param name="c">Service city</param>
    /// <param name="s">Service state</param>
    /// <param name="zc">Service zip code</param>
    public Service(String n, int i, float f, String d)
    {
        name = n;
        id = i;
        fee = f;
        description = d;
    }

    public String toString()
    {
        return "\n" +
               "  Name:       " + name + "\n" +
               "  ID:         " + id + "\n" +
               "  Fee:        " + fee + "\n" +
               "  Descripton: " + description + "\n";
    }
}
