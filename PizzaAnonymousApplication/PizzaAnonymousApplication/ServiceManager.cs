using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

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
    private static ServiceManager serviceManager;

    private ServiceManager()
    {
    }

    public static ServiceManager instance()
    {
        if (serviceManager == null)
        {
            return serviceManager = new ServiceManager();
        }
        else
        {
            return serviceManager;
        }
    }

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
    public void addService(String name, double fee, String description)
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
    public void editServiceFee(int id, double fee)
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

    public void save()
    {
        // Create an XmlWriterSettings object with the correct options. 
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.IndentChars = ("\t");
        settings.OmitXmlDeclaration = true;
        settings.NewLineChars = "\r\n";
        settings.NewLineHandling = NewLineHandling.Replace;

        String file = "ServiceManager.xml";

        using (XmlWriter writer = XmlWriter.Create(file, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("ServiceManager");
            writer.WriteElementString("NextID", nextId.ToString());
            writer.WriteStartElement("Services");

            foreach (Service service in serviceList)
            {
                writer.WriteStartElement("Service");
                writer.WriteElementString("Name", service.Name);
                writer.WriteElementString("ID", service.Id.ToString());
                writer.WriteElementString("Fee", service.Fee.ToString());
                writer.WriteElementString("Description", service.Description);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }

    public void load()
    {
        String file = "ServiceManager.xml";

        if (!File.Exists(file))
        {
            return;
        }

        // Create an XML reader for this file.
        using (XmlReader reader = XmlReader.Create(file))
        {
            String name = "Undefined Name";
            int id = -1;
            double fee = -1.0;
            String description = "Undefined Description";


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
                        case "Fee":
                            fee = reader.ReadElementContentAsDouble();
                            break;
                        case "Description":
                            description = reader.ReadElementContentAsString();
                            break;
                    }
                }
                else
                {
                    if (reader.Name == "Service")
                    {
                        Service service = new Service(name, id, fee, description);
                        serviceList.Add(service);
                    }
                }
            }
        }
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
