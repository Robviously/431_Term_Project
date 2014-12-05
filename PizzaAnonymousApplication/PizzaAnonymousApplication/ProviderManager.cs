using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

/// <summary>
/// ProviderManager Class: 
/// The ProviderManager class contains a list of all of the providers in the system and has the methods to
/// perform operations on them.
///  
/// Author: Ryan Schwartz
/// Date Created: November 6, 2014
/// Last Modified By: Ryan Schwartz
/// Date Last Modified: November 16, 2014
/// </summary>
public class ProviderManager
{
    // This is the list of providers.
	private List<Provider> providerList = new List<Provider>();
    public List<Provider> ProviderList
    {
        get { return providerList; }
    }

    // This int is for generating new unique provider IDs.
    int nextID = 100000000;

    /// <summary>
    /// This adds a new provider to the providerList based on passed arguments. The provider ID is generated from nextID.
    /// </summary>
    /// <param name="name">Provider Name</param>
    /// <param name="streetAddress">Provider Street Address</param>
    /// <param name="city">Provider City</param>
    /// <param name="state">Provider State</param>
    /// <param name="zipCode">Provider Zip Code</param>
    public void addProvider(String name, String streetAddress, String city, String state, int zipCode)
    {
        // Creates a new provider to add to the system.  Note: nextID is incremented.
        Provider provider = new Provider(name, nextID++, streetAddress, city, state, zipCode);
        providerList.Add(provider);
    }
    
    /// <summary>
    /// This is one of two edit methods.  This allows the provider's name to be modified.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="name">New Provider Name</param>
    public void editProviderName(int id, String name)
    {
        // Ensure provider exists in the system.
        if (!validateProvider(id))
            Console.WriteLine("Provider with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through providers until a match is found, then replace the provider's name.
            foreach (Provider p in providerList)
            {
                if (p.Id == id)
                {
                    p.Name = name;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// This is the other edit method to change the provider address.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="streetAddress">Provider Street Address</param>
    /// <param name="city">Provider City</param>
    /// <param name="state">Provider State</param>
    /// <param name="zipCode">Provider Zip Code</param>
    public void editProviderAddress(int id, String streetAddress, String city, String state, int zipCode)
    {
        // Ensure the provider exists in the system
        if (!validateProvider(id))
            Console.WriteLine("Provider with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through providers until a match is found, then replace address elements.
            foreach (Provider p in providerList)
            {
                if (p.Id == id)
                {
                    p.StreetAddress = streetAddress;
                    p.City = city;
                    p.State = state;
                    p.ZipCode = zipCode;
                    return;
                }
            }
        }
    }
    
    /// <summary>
    /// This method removes a provider from the providerList
    /// </summary>
    /// <param name="id">Provider ID</param>
    public void deleteProvider(int id)
    {
        // Ensure provider exists in the system.
        if (!validateProvider(id))
            Console.WriteLine("Provider with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through providers until a match is found and then remove the provider from providerList.
            foreach (Provider p in providerList)
            {
                if (p.Id == id)
                {
                    providerList.Remove(p);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// This adds a service to a provider's list of services.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="code">Service Code</param>
    public void addService(int id, int code)
    {
        // Ensure provider exists in the system.
        if (!validateProvider(id))
            Console.WriteLine("Provider with ID [" + id + "] doesn't exist in system.");
        else
        {
            if (validateService(id, code))
                Console.WriteLine("Service with code [" + code + "] already exists in system.");
            else
            {
                // Cycle through providers until a match is found and then add the service to that provider.
                foreach (Provider p in providerList)
                {
                    if (p.Id == id)
                    {
                        p.addService(code);
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// This removes a service from a provider's list of services.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="code">Service Code</param>
    public void deleteService(int id, int code)
    {
        // Ensure provider exists in the system.
        if (!validateProvider(id))
            Console.WriteLine("Provider with ID [" + id + "] doesn't exist in system.");
        else
        {
            if (!validateService(id, code))
                Console.WriteLine("Service with code [" + code + "] is not currently provided.");
            else
            {
                // Cycle through providers until a match is found and then remove the service from the provider.
                foreach (Provider p in providerList)
                {
                    if (p.Id == id)
                    {
                        p.removeService(code);
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// This returns a provider's list of services
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <returns>The list of service codes as a list of integers</returns>
    public List<int> getAllServices(int id)
    {
        // Ensure provider exists in the system.
        if (!validateProvider(id))
            Console.WriteLine("Provider with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through providers until a match is found and then return service list.
            foreach (Provider p in providerList)
            {
                if (p.Id == id)
                    return p.getServiceList;
            }
        }

        return null;
    }

    /// <summary>
    /// This determines if a provider has a service.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="code">Service Code</param>
    public bool validateService(int id, int code)
    {
        // Ensure provider exists in the system.
        if (!validateProvider(id))
            Console.WriteLine("Provider with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through providers until a match is found and then check for service.
            foreach (Provider p in providerList)
            {
                if (p.Id == id)
                    return p.serviceExist(code);
            }
        }

        // If provider doesn't exist, just return false.
        return false;
    }

    /// <summary>
    /// This returns an actual provider object from the providerList.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <returns>The provider with the passed ID or null if it doesn't exist</returns>
    public Provider getProviderById(int id)
    {
        // Ensure the provider exists in the system.
        if (!validateProvider(id))
            Console.WriteLine("Provider with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through providers until a match is found, then return that provider.
            foreach (Provider p in providerList)
            {
                if (p.Id == id)
                    return p;
            }
        }

        // If provider doesn't exist, return null.
        return null;
    }

    /// <summary>
    /// Determines whether a provider with the passed ID exists in the system
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <returns>True if the provider exists in the system, otherwise false.</returns>
    public bool validateProvider(int id)
    {
        // Cycle through providers.  If a match is found, return true.
        foreach (Provider p in providerList)
        {
            if (p.Id == id)
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
        String file = "ProviderManager.xml";

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
            writer.WriteStartElement("ProviderManager");
            writer.WriteElementString("NextID", nextID.ToString());
            writer.WriteStartElement("Providers");

            // Write the contents of each provider in providerList to the file
            foreach (Provider provider in providerList)
            {
                writer.WriteStartElement("Provider");
                writer.WriteElementString("Name", provider.Name);
                writer.WriteElementString("ID", provider.Id.ToString());
                writer.WriteElementString("StreetAddress", provider.StreetAddress);
                writer.WriteElementString("City", provider.City);
                writer.WriteElementString("State", provider.State);
                writer.WriteElementString("ZIPCode", provider.ZipCode.ToString());

                writer.WriteStartElement("ServicesProvided");

                foreach (int service in provider.getServiceList)
                {
                    writer.WriteElementString("ServiceID", service.ToString());
                }

                writer.WriteEndElement();
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
        String file = "ProviderManager.xml";

        // If there isn't a file to read from: exit, otherwise start reading.
        if (!File.Exists(file))
        {
            return;
        }
        else
        {
            Console.WriteLine("Reading data in to ProviderManager from file [" + file + "].");
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
            List<int> services = new List<int>();

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
                        case "ServiceID":
                            services.Add(reader.ReadElementContentAsInt());
                            break;
                    }
                }
                else
                {
                    // If the read is a closing provider tag
                    if (reader.Name == "Provider")
                    {
                        // Create the provider and add it to the list of providers
                        Provider provider = new Provider(name, id, streetAddress, city, state, zipCode);
                        providerList.Add(provider);

                        foreach (int service in services)
                        {
                            addService(id, service);
                        }

                        services.Clear();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Creates a formatted string representation of this object.
    /// </summary>
    /// <returns>A string that represents the providers in the provider list</returns>
    public override string ToString()
    {
        string providers = "";

        foreach (Provider p in providerList)
        {
            providers += p.ToString();
        }

        return providers;
    }
}
