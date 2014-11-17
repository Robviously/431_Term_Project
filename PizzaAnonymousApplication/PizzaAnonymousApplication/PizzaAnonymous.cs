using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

/// <summary>
/// PizzaAnonymous Class: 
/// The PizzaAnonymous class is responsible for interfacing between 
/// the user interface and the rest of the system.
///  
/// Author: Rob Perpich
/// Date Created: November 6, 2014
/// Last Modified By: Rob Perpich
/// Date Last Modified: November 16, 2014
/// </summary>
public class PizzaAnonymous
{
    private static PizzaAnonymous pizzaAnonymous;
    private static MemberManager memberManager;
    private static ProviderManager providerManager;
    private static ServiceManager serviceManager;
    private static Report report;

    /// <summary>
    /// Private singleton constructor
    /// </summary>
    private PizzaAnonymous()
    {
        memberManager = new MemberManager();
        providerManager = new ProviderManager();
        serviceManager = new ServiceManager();
        report = Report.getInstance;
    }

    /// <summary>
    /// Public singleton constructor
    /// </summary>
    /// <returns>Returns the reference to the only instance of PizzaAnonymous</returns>
    public static PizzaAnonymous instance()
    {
        if (pizzaAnonymous == null)
        {
            return pizzaAnonymous = new PizzaAnonymous();
        }
        else
        {
            return pizzaAnonymous;
        }
    }

    /// <summary>
    /// Prompts the user for the parameters required to add a member to the system. Then
    /// calls the member manager method that will create the member.
    /// </summary>
    public void addMember()
    {
        String name = UserInterface.getString("Enter the member's name: ", 1, 25);
        String streetAddress = UserInterface.getString("Enter the member's street address: ", 1, 25);
        String city = UserInterface.getString("Enter the member's city: ", 1, 14);
        String state = UserInterface.getString("Enter the member's state: ", 2, 2);
        int zipCode = UserInterface.getInteger("Enter the member's ZIP code: ", 5, 5);

        // Add the member to the system with the given attributes
        memberManager.addMember(name, streetAddress, city, state, zipCode);

        Console.Out.WriteLine("Successfully added member.");
    }

    /// <summary>
    /// Prompts the user for a member ID and a field to edit for that member. If the user enters
    /// "name", the user can then input a new name for the member. If the user enters "address",
    /// the user can then enter the components that will make up the member's new address. The
    /// method will then update the member with the given ID to have the new name or address.
    /// </summary>
    public void editMember()
    {
        int memberId = UserInterface.getInteger("Enter member ID: ");

        if (memberManager.validateMember(memberId))
        {
            String choice = UserInterface.getString("Enter field to edit(name or address): ");

            if (choice.ToLower().Equals("name"))
            {
                String name = UserInterface.getString("Enter new member name: ", 1, 25);

                memberManager.editMemberName(memberId, name);
                Console.Out.WriteLine("Successfully edited member name.");

            }
            else if (choice.ToLower().Equals("address"))
            {
                String streetAddress = UserInterface.getString("Enter member street address: ", 1, 25);
                String city = UserInterface.getString("Enter member city: ", 1, 14);
                String state = UserInterface.getString("Enter member state: ", 2, 2);
                int zipCode = UserInterface.getInteger("Enter member ZIP code: ", 5, 5);

                memberManager.editMemberAddress(memberId, streetAddress, city, state, zipCode);
                Console.Out.WriteLine("Successfully edited member address.");
            }
            else
            {
                Console.Out.WriteLine("Unknown field entered. Valid fields: name, address.");
            }
        }
        else
        {
            Console.Out.WriteLine("Unable to find member [" + memberId + "].");
        }
    }

    /// <summary>
    /// Prompts the user for a member ID. If the member exists in the system,
    /// the method will call the member manager method to delete the member.
    /// </summary>
    public void deleteMember()
    {
        int memberId = UserInterface.getInteger("Enter the member ID: ");

        if (memberManager.validateMember(memberId))
        {
            memberManager.deleteMember(memberId);
            Console.Out.WriteLine("Successfully deleted member.");
        }
        else
        {
            Console.Out.WriteLine("Unable to find member [" + memberId + "]");
        }
    }

    /// <summary>
    /// Prompts the user to enter a member ID. If the member exists in the system,
    /// the method will output whether the member is suspended or not. Otherwise,
    /// the method will print that the member doesn't exist.
    /// </summary>
    public void validateMember()
    {
        int memberId = UserInterface.getInteger("Enter the member's ID: ");
        Member member = memberManager.getMemberById(memberId);

        if (member != null)
        {
            if (member.Suspended)
            {
                Console.Out.WriteLine("SUSPENDED - Member exists but is suspended.");
            }

            Console.Out.WriteLine("VALID - Member exists and is not suspended.");
        }
        else
        {
            Console.Out.WriteLine("INVALID - Member does not exist.");
        }
    }

    /// <summary>
    /// Displays all members in the system to the console in a nice format.
    /// </summary>
    public void printMembers()
    {
        String membersString = memberManager.ToString();

        if (membersString == "")
        {
            Console.Out.WriteLine("No members in the database.");
        }
        else
        {
            Console.Out.WriteLine(membersString);
        }
    }

    /// <summary>
    /// Prompts the user for the parameters required to add a provider to the system. Then
    /// calls the provider manager method that will create the provider.
    /// </summary>
    public void addProvider()
    {
        String name = UserInterface.getString("Enter the provider's name: ", 1, 25);
        String streetAddress = UserInterface.getString("Enter the provider's street address: ", 1, 25);
        String city = UserInterface.getString("Enter the provider's city: ", 1, 14);
        String state = UserInterface.getString("Enter the provider's state: ", 2, 2);
        int zipCode = UserInterface.getInteger("Enter the provider's ZIP code: ", 5, 5);

        providerManager.addProvider(name, streetAddress, city, state, zipCode);

        Console.Out.WriteLine("Successfully added provider.");
    }

    /// <summary>
    /// Prompts the user for a provider ID and a field to edit for that provider. If the user enters
    /// "name", the user can then input a new name for the provider. If the user enters "address",
    /// the user can then enter the components that will make up the provider's new address. The
    /// method will then update the provider with the given ID to have the new name or address.
    /// </summary>
    public void editProvider()
    {
        int providerId = UserInterface.getInteger("Enter provider ID: ");

        if (providerManager.validateProvider(providerId))
        {
            String choice = UserInterface.getString("Enter field to edit(name or address): ");

            if (choice.ToLower().Equals("name"))
            {
                String name = UserInterface.getString("Enter a new provider name: ", 1, 25);

                providerManager.editProviderName(providerId, name);
                Console.Out.WriteLine("Successfully edited provider name.");
       
            }
            else if (choice.ToLower().Equals("address"))
            {
                String streetAddress = UserInterface.getString("Enter provider street address: ", 1, 25);
                String city = UserInterface.getString("Enter provider city: ", 1, 14);
                String state = UserInterface.getString("Enter provider state: ", 2, 2);
                int zipCode = UserInterface.getInteger("Enter provider ZIP code: ", 5, 5);

                providerManager.editProviderAddress(providerId, streetAddress, city, state, zipCode);
                Console.Out.WriteLine("Successfully edited provider address.");
            }
            else
            {
                Console.Out.WriteLine("Unknown field entered. Valid fields: name, address.");
            }
        }
        else
        {
            Console.Out.WriteLine("Unable to find provider [" + providerId + "].");
        }
    }

    /// <summary>
    /// Prompts the user for a provider ID. If the provider exists in the system,
    /// the method will call the provider manager method to delete the provider.
    /// </summary>
    public void deleteProvider()
    {
        int providerId = UserInterface.getInteger("Enter the provider ID: ");

        if (providerManager.validateProvider(providerId))
        {
            providerManager.deleteProvider(providerId);
            Console.Out.WriteLine("Successfully deleted provider.");
        }
        else
        {
            Console.Out.WriteLine("Unable to find provider [" + providerId + "].");
        }
    }

    /// <summary>
    /// Prompts the user for a provider ID. If the provider exists, the
    /// method will prompt the user for a service ID. If the service exists,
    /// the method will then call the provider manager method that will
    /// add the service to the list of services that the provider provides.
    /// </summary>
    public void addServiceToProvider()
    {
        int providerId = UserInterface.getInteger("Enter provider ID: ");

        if (providerManager.validateProvider(providerId))
        {
            int serviceId = UserInterface.getInteger("Enter service ID: ");

            if (serviceManager.validateService(serviceId))
            {
                providerManager.addService(providerId, serviceId);
            }
            else
            {
                Console.Out.WriteLine("Service ID [" + serviceId + "] is not valid.");
            }
        }
        else
        {
            Console.Out.WriteLine("Provider ID [" + providerId + "] is not valid.");
        }
    }

    /// <summary>
    /// Prompts the user for a provider ID. If the provider exists, the
    /// method will prompt the user for a service ID. If the service is
    /// provided by that provider,the method will then call the provider 
    /// manager method that will remove the service from the list of services 
    /// that the provider provides.
    /// </summary>
    public void deleteServiceFromProvider()
    {
        int providerId = UserInterface.getInteger("Enter provider ID: ");

        if (providerManager.validateProvider(providerId))
        {
            int serviceId = UserInterface.getInteger("Enter service ID: ");

            if (serviceManager.validateService(serviceId))
            {
                providerManager.deleteService(providerId, serviceId);
            }
            else
            {
                Console.Out.WriteLine("Service ID [" + serviceId + "] is not valid.");
            }
        }
        else
        {
            Console.Out.WriteLine("Provider ID [" + providerId + "] is not valid.");
        }
    }

    /// <summary>
    /// Displays the services codes and names of the services provided by
    /// the provider with the given provider ID.
    /// </summary>
    /// <param name="providerId">Provider ID</param>
    public void serviceLookup(int providerId)
    {
        Provider provider = providerManager.getProviderById(providerId);
        List<int> providerServices = provider.getServiceList;
        Service service;

        if (providerServices.Count == 0)
        {
            Console.Out.WriteLine("This provider doesn't provide any services.");
        }

        foreach (int serviceID in providerServices)
        {
            service = serviceManager.getServiceById(serviceID);
            Console.Out.WriteLine(serviceID + "   " + service.Name);
        }
    }

    /// <summary>
    /// Records a service being provided by a provider for a member. The information
    /// captured will be stored in a file of similar records. 
    /// </summary>
    /// <param name="providerId"></param>
    public void captureService(int providerId)
    {
        String file = "CapturedServices.xml";
        XDocument doc;
        XElement xmlRoot;
        int memberId = UserInterface.getInteger("Enter the member's ID: ");

        if (memberManager.validateMember(memberId))
        {
            String dateOfService = UserInterface.getDate("Enter the date the service was provided (Format: MM-DD-YYYY): ");
            int serviceId = UserInterface.getInteger("Enter the ID of the service provided: ");

            if (providerManager.validateService(providerId, serviceId))
            {
                if (UserInterface.yesOrNo("Is [" + serviceManager.getServiceById(serviceId).Name + "] the correct service? "))
                {
                    String comments = UserInterface.getString("Enter comments [optional]: ", 0, 100);
                    String currentTime = DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Year.ToString() +
                        " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

                    if (File.Exists(file))
                    {
                        doc = XDocument.Load(file);
                        xmlRoot = doc.Root;
                    }
                    else
                    {
                        doc = new XDocument();
                        xmlRoot = new XElement("services");
                        doc.Add(xmlRoot);
                    }

                    Provider provider = providerManager.getProviderById(providerId);
                    Member member = memberManager.getMemberById(memberId);
                    Service service = serviceManager.getServiceById(serviceId);

                    XElement capturedService =
                        new XElement("service",
                            new XElement("providerID", providerId),
                            new XElement("memberID", memberId),
                            new XElement("memberName", member.Name),
                            new XElement("providerName", provider.Name),
                            new XElement("serviceDate", dateOfService),
                            new XElement("serviceCode", service.Id),
                            new XElement("serviceFee", service.Fee),
                            new XElement("serviceName", service.Name),
                            new XElement("comments", comments),
                            new XElement("currentDate", currentTime),
                            new XElement("mStrtAddr", member.StreetAddress),
                            new XElement("mState", member.State),
                            new XElement("mCity", member.City),
                            new XElement("mZip", member.ZipCode),
                            new XElement("pStrtAddr", provider.StreetAddress),
                            new XElement("pState", provider.State),
                            new XElement("pCity", provider.City),
                            new XElement("pZip", provider.ZipCode));

                    xmlRoot.Add(capturedService);
                    doc.Save(file);
                }
                else
                {
                    Console.Out.WriteLine("Exiting to menu. No service was captured.");
                    return;
                }
            }
            else
            {
                Console.Out.WriteLine("Service [" + serviceId + "] is not listed as a service provided by provider [" + providerId + "].");
            }
        }
        else
        {
            Console.Out.WriteLine("That member doesn't exist.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    public bool validateProviderId(int providerId)
    {
        return providerManager.validateProvider(providerId);
    }

    /// <summary>
    /// Displays all providers in the system to the console in a nice format.
    /// </summary>
    public void printProviders()
    {
        String providersString = providerManager.ToString();

        if (providersString == "")
        {
            Console.Out.WriteLine("No providers in the database.");
        }
        else
        {
            Console.Out.WriteLine(providersString);
        }
    }

    /// <summary>
    /// Prompts the user for the parameters required to add a service to the system. Then
    /// calls the service manager method that will create the service.
    /// </summary>
    public void addService()
    {
        String name = UserInterface.getString("Enter the service's name: ", 1, 25);
        double fee = UserInterface.getDouble("Enter the service's fee: ", 0.0, 999.99, 2);
        String description = UserInterface.getString("Enter the service's description: ", 1, 100);

        serviceManager.addService(name, fee, description);
        Console.Out.WriteLine("Successfully added service.");
    }

    /// <summary>
    /// Prompts the user for a service ID and a field to edit for that service. If the user enters
    /// "name", the user can then input a new name for the service. If the user enters "fee",
    /// the user can then input a new fee for the service. If the user enters "description", the
    /// user can then input a new description for the service. The method will then update the 
    /// service with the given ID to have the new name, fee, or description.
    /// </summary>
    public void editService()
    {
        int serviceId = UserInterface.getInteger("Enter service ID: ");

        if (serviceManager.validateService(serviceId))
        {
            String choice = UserInterface.getString("Enter field to edit(name, fee, description): ");

            if (choice.ToLower().Equals("name"))
            {
                String name = UserInterface.getString("Enter new service name: ", 1, 25);

                serviceManager.editServiceName(serviceId, name);
                Console.Out.WriteLine("Successfully edited service name.");
            }
            else if (choice.ToLower().Equals("fee"))
            {
                double fee = UserInterface.getDouble("Enter new service fee: ", 0.0, 999.99, 2);

                serviceManager.editServiceFee(serviceId, fee);
                Console.Out.WriteLine("Successfully edited service name.");
            }
            else if (choice.ToLower().Equals("description"))
            {
                String description = UserInterface.getString("Enter new service description: ", 1, 100);

                serviceManager.editServiceDescription(serviceId, description);
                Console.Out.WriteLine("Successfully edited service description.");
            }
            else
            {
                Console.Out.WriteLine("Unknown field entered. Valid fields: name, fee, description");
            }
        }
        else
        {
            Console.Out.WriteLine("Unable to find service [" + serviceId + "].");
        }
    }

    /// <summary>
    /// Prompts the user for a service ID. If the service exists in the system,
    /// the method will call the service manager method to delete the service.
    /// </summary>
    public void deleteService()
    {
        int serviceId = UserInterface.getInteger("Enter the service ID: ");

        if (serviceManager.validateService(serviceId))
        {
            foreach (Provider provider in providerManager.ProviderList)
            {
                provider.removeService(serviceId);
            }

            serviceManager.deleteService(serviceId);
            Console.Out.WriteLine("Service deleted.");
        }
        else
        {
            Console.Out.WriteLine("Unable to find service [" + serviceId + "].");
        }
    }

    /// <summary>
    /// Displays all services in the system to the console in a nice format.
    /// </summary>
    public void printServices()
    {
        String servicesString = serviceManager.ToString();

        if (servicesString == "")
        {
            Console.Out.WriteLine("No services in the database.");
        }
        else
        {
            Console.Out.WriteLine(servicesString);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void printMemberReport()
    {
        // ****ERROR CHECK
        int memberId = UserInterface.getInteger("Enter the member's ID: ");

        report.getMemberReport(memberId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="providerId"></param>
    public void printProviderReport(int providerId = -1)
    {
        // ****ERROR CHECK
        if (providerId == -1)
        {
            providerId = UserInterface.getInteger("Enter the provider's ID: ");
        }

        report.getProviderReport(providerId);
    }

    /// <summary>
    /// 
    /// </summary>
    public void printSummaryReport()
    {
        report.getProviderSummary();
    }

    /// <summary>
    /// 
    /// </summary>
    public void printEFTReport()
    {
        report.createEFT();
    }

    /// <summary>
    /// 
    /// </summary>
    public void save()
    {
        memberManager.save();
        providerManager.save();
        serviceManager.save();
    }

    /// <summary>
    /// 
    /// </summary>
    public void load()
    {
        memberManager.load();
        providerManager.load();
        serviceManager.load();
    }
}
