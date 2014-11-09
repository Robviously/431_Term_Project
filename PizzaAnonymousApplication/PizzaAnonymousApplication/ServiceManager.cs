using System;
using System.Collections.Generic;

/// <summary>
/// ServiceManager Class: 
/// The ServiceManager class contains a list of all of the services in the system and has the methods to
/// perform operations on them.
///  
/// Author: Ryan Schwartz
/// Date Created: November 6, 2014
/// Last Modified By: Ryan Schwartz
/// Date Last Modified: November 6, 2014
/// </summary>
public class ServiceManager
{
    // This is the list of services.
    private List<Service> serviceList = new List<Service>();

    // This int is for generating new service IDs.
    int nextId = 100000000;

    /// <summary>
    /// This adds a new service to the serviceList based on passed arguments.  The service ID is generated from nextId.
    /// </summary>
    /// <param name="name">Service Name</param>
    /// <param name="fee">Service Fee</param>
    /// <param name="description">Service Description</param>
    public void addService(String name, float fee, String description)
    {
        // Creates a new service to add to the system.  Note: nextID is incremented.
        Service service = new Service(name, nextId++, fee, description);
        serviceList.Add(service);
    }

    /// <summary>
    /// This allows the service's name to be modified.
    /// </summary>
    /// <param name="id">Service ID</param>
    /// <param name="name">New Service Name</param>
    public void editServiceName(int id, String name)
    {
        // Get the service object using its ID
        Service service = getServiceById(id);

        if (service != null)
        {
            service.Name = name;
        }
    }

    /// <summary>
    /// This allows the service's fee to be modified.
    /// </summary>
    /// <param name="id">Service ID</param>
    /// <param name="fee">Service fee</param>
    public void editServiceFee(int id, float fee)
    {
        // Get the service object using its ID
        Service service = getServiceById(id);

        if (service != null)
        {
            service.Fee = fee;
        }
    }

    /// <summary>
    /// This allows the service's description to be modified.
    /// </summary>
    /// <param name="id">Service ID</param>
    /// <param name="description">Service description</param>
    public void editServiceDescription(int id, String description)
    {
        // Get the service object using its ID
        Service service = getServiceById(id);

        if (service != null)
        {
            service.Description = description;
        }
    }

    /// <summary>
    /// This method removes a service from the serviceList
    /// </summary>
    /// <param name="id">Service ID</param>
    public void deleteService(int id)
    {
        // Get the service object using its ID
        Service service = getServiceById(id);

        if (service != null)
        {
            serviceList.Remove(service);
        }
    }

    /// <summary>
    /// This returns an actual service object from the serviceList.
    /// </summary>
    /// <param name="id">Service ID</param>
    /// <returns>The service with the passed ID or null if it doesn't exist</returns>
    public Service getServiceById(int id)
    {
        // Cycle through services until a match is found, then return that service.
        foreach (Service s in serviceList)
        {
            if (s.Id == id)
            {
                return s;
            }
        }

        // If service doesn't exist, return null.
        return null;
    }

    /// <summary>
    /// Determines whether a service with the passed ID exists in the system
    /// </summary>
    /// <param name="id">Service ID</param>
    /// <returns>True if the service exists in the system, otherwise false.</returns>
    public bool validateService(int id)
    {
        if (getServiceById(id) != null)
        {
            return true;
        }

        // Otherwise return false.
        return false;
    }

    public String toString()
    {
        String services = "";

        foreach (Service s in serviceList)
        {
            services = services + s.toString();
        }

        return services;
    }
}
