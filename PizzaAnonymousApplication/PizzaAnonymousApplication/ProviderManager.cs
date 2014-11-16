using System;
using System.Collections.Generic;

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
    // This is the list of providers.
	private List<Provider> providerList = new List<Provider>();
    public List<Provider> ProviderList
    {
        get { return providerList; }
    }

    // This int is for generating new provider IDs.
    int nextID = 100000000;

    /// <summary>
    /// This adds a new provider to the providerList based on passed arguments.  The provider ID is generated from nextID.
    /// </summary>
    /// <param name="name">Provider Name</param>
    /// <param name="streetAddress">Provider Address</param>
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
            Console.Out.WriteLine("Provider with ID: " + id + " doesn't exist in system.");
        else
        {
            // Cycle through providers until a match is found, then replace the provider name.
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
    /// <param name="streetAddress">Provider address</param>
    /// <param name="city">Provider city</param>
    /// <param name="state">Provider state</param>
    /// <param name="zipCode">Provider zip code</param>
    public void editProviderAddress(int id, String streetAddress, String city, String state, int zipCode)
    {
        // Ensure the provider exists in the system
        if (!validateProvider(id))
            Console.Out.WriteLine("Provider with ID: " + id + " doesn't exist in system.");
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
            Console.Out.WriteLine("Provider with ID: " + id + " doesn't exist in system.");
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
    /// This adds a service to a provider.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="code">Service Code</param>
    public void addService(int id, int code)
    {
        // Ensure provider exists in the system.
        if (!validateProvider(id))
            Console.Out.WriteLine("Provider with ID: " + id + " doesn't exist in system.");
        else
        {
            if (validateService(id, code))
                Console.Out.WriteLine("Service with code " + code + " already exists in system.");
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
    /// This removes a service from a provider.
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <param name="code">Service Code</param>
    public void deleteService(int id, int code)
    {
        // Ensure provider exists in the system.
        if (!validateProvider(id))
            Console.Out.WriteLine("Provider with ID " + id + " doesn't exist in system.");
        else
        {
            if (!validateService(id, code))
                Console.Out.WriteLine("Service with code " + code + " is not currently provided.");
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
    /// <returns>The list of service codes</returns>
    public List<int> getAllServices(int id)
    {
        // Ensure provider exists in the system.
        if (!validateProvider(id))
            Console.Out.WriteLine("Provider with ID: " + id + " doesn't exist in system.");
        else
        {
            // Cycle through providers until a match is found and then return service list.
            foreach (Provider p in providerList)
            {
                if (p.Id == id)
                    return p.getServiceList();
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
            Console.Out.WriteLine("Provider with ID: " + id + " doesn't exist in system.");
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
            Console.Out.WriteLine("Provider with ID: " + id + " doesn't exist in system.");
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
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string providers = "";

        foreach (Provider m in providerList)
        {
            providers += m.ToString();
        }

        return providers;
    }
}
