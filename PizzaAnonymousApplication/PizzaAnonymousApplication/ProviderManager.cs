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
/// Date Last Modified: November 6, 2014
/// </summary>
public class ProviderManager
{
    private static ProviderManager providerManager;

    // This int is for generating new provider IDs.
    private static int nextId;

    private ProviderManager()
    {
        nextId = 100000000;
    }

    public static ProviderManager instance()
    {
        if (providerManager == null)
        {
            return providerManager = new ProviderManager();
        }
        else
        {
            return providerManager;
        }
    }

    // This is the list of providers.
    private List<Provider> providerList = new List<Provider>();

    public List<Provider> getProviderList()
    {
        return providerList;
    }

    /// <summary>
    /// This adds a new provider to the providerList based on passed arguments.  The provider ID is generated from nextId.
    /// </summary>
    /// <param name="name">Provider Name</param>
    /// <param name="streetAddress">Provider Address</param>
    /// <param name="city">Provider City</param>
    /// <param name="state">Provider State</param>
    /// <param name="zipCode">Provider Zip Code</param>
    public void addProvider(String name, String streetAddress, String city, String state, int zipCode)
    {
        // Creates a new provider to add to the system.  Note: nextID is incremented.
        Provider provider = new Provider(name, nextId++, streetAddress, city, state, zipCode);
        providerList.Add(provider);
    }

    /// <summary>
    /// This allows the provider's name to be modified.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="name">New Provider Name</param>
    public void editProviderName(int id, String name)
    {
        // Get the provider object using its ID
        Provider provider = getProviderById(id);

        if (provider != null)
        {
            provider.Name = name;
        }
    }

    /// <summary>
    /// This allows the provider's address to be modified.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="streetAddress">Provider address</param>
    /// <param name="city">Provider city</param>
    /// <param name="state">Provider state</param>
    /// <param name="zipCode">Provider zip code</param>
    public void editProviderAddress(int id, String streetAddress, String city, String state, int zipCode)
    {
        // Get the provider object using its ID
        Provider provider = getProviderById(id);

        if (provider != null)
        {
            provider.StreetAddress = streetAddress;
            provider.City = city;
            provider.State = state;
            provider.ZipCode = zipCode;
        }
    }

    /// <summary>
    /// This method removes a provider from the providerList
    /// </summary>
    /// <param name="id">Provider ID</param>
    public void deleteProvider(int id)
    {
        // Get the provider object using its ID
        Provider provider = getProviderById(id);

        if (provider != null)
        {
            providerList.Remove(provider);
        }
    }

    /// <summary>
    /// This returns an actual provider object from the providerList.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <returns>The provider with the passed ID or null if it doesn't exist</returns>
    public Provider getProviderById(int id)
    {
        // Cycle through providers until a match is found, then return that provider.
        foreach (Provider p in providerList)
        {
            if (p.Id == id)
            {
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
        if (getProviderById(id) != null)
        {
            return true;
        }

        // Otherwise return false.
        return false;
    }

    /// <summary>
    /// This adds a service to a provider.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="code">Service Code</param>
    public void addService(int id, int code)
    {
        // Get the provider object using its ID
        Provider provider = getProviderById(id);

        if (provider != null)
        {
            provider.addService(code);
        }
    }

    /// <summary>
    /// This removes a service from a provider.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="code">Service Code</param>
    public void deleteService(int id, int code)
    {
        // Get the provider object using its ID
        Provider provider = getProviderById(id);
         
        if (provider != null)
        {
            provider.removeService(code);
        }
    }

    /// <summary>
    /// This determines if a provider has a service.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="code">Service Code</param>
    public bool validateService(int id, int code)
    {
        // Get the provider object using its ID
        Provider provider = getProviderById(id);

        if (provider != null)
        {
            return provider.serviceExist(code);
        }
           
        // If provider doesn't exist, just return false.
        return false;
    }

    public void save()
    {
        // Create an XmlWriterSettings object with the correct options. 
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.IndentChars = ("\t");
        settings.OmitXmlDeclaration = true;
        settings.NewLineChars = "\r\n";
        settings.NewLineHandling = NewLineHandling.Replace;

        String file = "ProviderManager.xml";

        using (XmlWriter writer = XmlWriter.Create(file, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("ProviderManager");
            writer.WriteElementString("NextID", nextId.ToString());
            writer.WriteStartElement("Providers");

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

                foreach (int service in provider.getServiceList())
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

    public void load()
    {
        String file = "ProviderManager.xml";

        if (!File.Exists(file))
        {
            return;
        }

        // Create an XML reader for this file.
        using (XmlReader reader = XmlReader.Create(file))
        {
            String name = "Undefined Name";
            int id = -1;
            String streetAddress = "Undefined Street Address";
            String city = "Undefined City";
            String state = "Undefined State";
            int zipCode = -1;
            List<int> services = new List<int>();

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    // Get element name and switch on it.
                    switch (reader.Name)
                    {
                        case "NextID":
                            nextId = reader.ReadElementContentAsInt();
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
                    if (reader.Name == "Provider")
                    {
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
    /// 
    /// </summary>
    /// <returns></returns>
    public String toString()
    {
        String providers = "";

        foreach (Provider p in providerList)
        {
            providers = providers + p.toString();
        }

        return providers;
    }
}
