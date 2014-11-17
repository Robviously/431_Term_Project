using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

/// <summary>
/// ServiceManager Class: 
/// The ServiceManager class contains a list of all of the services in the system and has the methods to
/// perform operations on them.
///  
/// Author: Julius Karmacharya
/// Date Created: November 6, 2014
/// Last Modified By: Julius Karmacharya
/// Date Last Modified: November 16, 2014
/// </summary>
public class ServiceManager
{
    // This is the list of services.
    private List<Service> serviceList = new List<Service>();
    public List<Service> ServiceList
    {
        get { return serviceList; }
    }

    // This int is for generating new unique service IDs.
    int nextID = 100000;

    /// <summary>
    /// This adds a new service to the serviceList based on passed arguments. The service ID is generated from nextID.
    /// </summary>
    /// <param name="name">Service Name</param>
    /// <param name="fee">Service Fee</param>
    /// <param name="description">Service Description</param>
    public void addService(String name, double fee, String description)
    {
        // Creates a new service to add to the system.  Note: nextID is incremented.
        Service service = new Service(name, nextID++, fee, description);
        serviceList.Add(service);
    }

    /// <summary>
    /// This is one of three edit methods.  This allows the service's name to be modified.
    /// </summary>
    /// <param name="id">Service ID</param>
    /// <param name="name">New Service Name</param>
    public void editServiceName(int id, String name)
    {
        // Ensure service exists in the system.
        if (!validateService(id))
            Console.Out.WriteLine("Service with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through services until a match is found, then replace the service's name.
            foreach (Service s in serviceList)
            {
                if (s.Id == id)
                {
                    s.Name = name;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// This is one of three edit methods.  This allows the service's fee to be modified.
    /// </summary>
    /// <param name="id">Service ID</param>
    /// <param name="fee">Service Fee</param>
    public void editServiceFee(int id, double fee)
    {
        // Ensure the service exists in the system
        if (!validateService(id))
            Console.Out.WriteLine("Service with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through services until a match is found, then replace the service's fee.
            foreach (Service s in serviceList)
            {
                if (s.Id == id)
                {
                    s.Fee = fee;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// This is one of three edit methods.  This allows the service's description to be modified.
    /// </summary>
    /// <param name="id">Service ID</param>
    /// <param name="description">Service Description</param>
    public void editServiceDescription(int id, String description)
    {
        // Ensure the service exists in the system
        if (!validateService(id))
            Console.Out.WriteLine("Service with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through services until a match is found, then replace the service's description.
            foreach (Service s in serviceList)
            {
                if (s.Id == id)
                {
                    s.Description = description;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// This method removes a service from the serviceList
    /// </summary>
    /// <param name="id">Service ID</param>
    public void deleteService(int id)
    {
        // Ensure service exists in the system.
        if (!validateService(id))
            Console.Out.WriteLine("Service with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through services until a match is found and then remove the service from serviceList.
            foreach (Service s in serviceList)
            {
                if (s.Id == id)
                {
                    serviceList.Remove(s);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// This returns an actual service object from the serviceList.
    /// </summary>
    /// <param name="id">Service ID</param>
    /// <returns>The service with the passed ID or null if it doesn't exist</returns>
    public Service getServiceById(int id)
    {
        // Ensure the service exists in the system.
        if (!validateService(id))
            Console.Out.WriteLine("Service with ID [" + id + "] doesn't exist in system.");
        else
        {
            // Cycle through services until a match is found, then return that service.
            foreach (Service s in serviceList)
            {
                if (s.Id == id)
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
        // Cycle through services.  If a match is found, return true.
        foreach (Service s in serviceList)
        {
            if (s.Id == id)
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
        String file = "ServiceManager.xml";

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
            writer.WriteStartElement("ServiceManager");
            writer.WriteElementString("NextID", nextID.ToString());
            writer.WriteStartElement("Services");

            // Write the contents of each service in serviceList to the file
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

    /// <summary>
    /// Loads the contents of a previously saved XML file into this instance of the class
    /// </summary>
    public void load()
    {
        // The path to the file to read from
        String file = "ServiceManager.xml";

        // If there isn't a file to read from: exit, otherwise start reading.
        if (!File.Exists(file))
        {
            return;
        }
        else
        {
            Console.Out.WriteLine("Reading data in to ServiceManager from file [" + file + "].");
        }

        // Create an XML reader for this file.
        using (XmlReader reader = XmlReader.Create(file))
        {
            // The variables should be overwritten when loading the information in.
            String name = "Undefined Name";
            int id = -1;
            double fee = -1.0;
            String description = "Undefined Description";

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
                    // If the read is a closing service tag
                    if (reader.Name == "Service")
                    {
                        // Create the service and add it to the list of services
                        Service service = new Service(name, id, fee, description);
                        serviceList.Add(service);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Creates a formatted string representation of this object.
    /// </summary>
    /// <returns>A string that represents the services in the service list</returns>
    public override string ToString()
    {
        string services = "";

        foreach (Service m in serviceList)
        {
            services += m.ToString();
        }

        return services;
    }
}
