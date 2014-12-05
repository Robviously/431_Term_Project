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
    /// This constructor is used for Testing Purposes Only
    /// </summary>
    /// <param name="mm">Member Manager Object</param>
    /// <param name="pm">Provider Manager Object</param>
    /// <param name="sm">Service Manager Object</param>
    public PizzaAnonymous(MemberManager mm = null, ProviderManager pm = null, ServiceManager sm = null)
    {
        memberManager = mm;
        providerManager = pm;
        serviceManager = sm;
        report = Report.getInstance;
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

        Console.WriteLine("Successfully added member.");
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

        // Validate the member exists before proceeding
        if (memberManager.validateMember(memberId))
        {
            String choice = UserInterface.getString("Enter field to edit(name or address): ");

            // If the user wants to edit the name
            if (choice.ToLower().Equals("name"))
            {
                String name = UserInterface.getString("Enter new member name: ", 1, 25);

                memberManager.editMemberName(memberId, name);
                Console.WriteLine("Successfully edited member name.");

            }
            // If the user wants to edit the address
            else if (choice.ToLower().Equals("address"))
            {
                String streetAddress = UserInterface.getString("Enter member street address: ", 1, 25);
                String city = UserInterface.getString("Enter member city: ", 1, 14);
                String state = UserInterface.getString("Enter member state: ", 2, 2);
                int zipCode = UserInterface.getInteger("Enter member ZIP code: ", 5, 5);

                memberManager.editMemberAddress(memberId, streetAddress, city, state, zipCode);
                Console.WriteLine("Successfully edited member address.");
            }
            // If the user didn't enter "name" or "address"
            else
            {
                Console.WriteLine("Unknown field entered. Valid fields: name, address.");
            }
        }
        else
        {
            Console.WriteLine("Unable to find member [" + memberId + "].");
        }
    }

    /// <summary>
    /// Prompts the user for a member ID. If the member exists in the system,
    /// the method will call the member manager method to delete the member.
    /// </summary>
    public void deleteMember()
    {
        int memberId = UserInterface.getInteger("Enter the member ID: ", 9, 9);

        // Validate the member exists before proceeding
        if (memberManager.validateMember(memberId))
        {
            memberManager.deleteMember(memberId);
            Console.WriteLine("Successfully deleted member.");
        }
        else
        {
            Console.WriteLine("Unable to find member [" + memberId + "]");
        }
    }

    /// <summary>
    /// Prompts the user for a member ID. If the member exists in the system,
    /// the method will call the member manager method to suspend the member.
    /// </summary>
    public void suspendMember()
    {
        int memberId = UserInterface.getInteger("Enter the member ID: ", 9, 9);

        // Validate the member exists before proceeding
        if (memberManager.validateMember(memberId))
        {
            memberManager.suspendMember(memberId);
            Console.WriteLine("Successfully suspended member.");
        }
        else
        {
            Console.WriteLine("Unable to find member [" + memberId + "]");
        }
    }

    /// <summary>
    /// Prompts the user to enter a member ID. If the member exists in the system,
    /// the method will output whether the member is suspended or not. Otherwise,
    /// the method will print that the member doesn't exist.
    /// </summary>
    public void validateMember()
    {
        int memberId = UserInterface.getInteger("Enter the member's ID: ", 9, 9);
        Member member = memberManager.getMemberById(memberId);

        // If the member exists in the system
        if (member != null)
        {
            // If the member is suspended
            if (member.Suspended)
            {
                Console.WriteLine("SUSPENDED - Member exists but is suspended.");
            }
            else
            {
                Console.WriteLine("VALID - Member exists and is not suspended.");
            }
        }
        else
        {
            Console.WriteLine("INVALID - Member does not exist.");
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
            Console.WriteLine("No members in the database.");
        }
        else
        {
            Console.WriteLine(membersString);
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

        // Add the provider to the system with the given attributes
        providerManager.addProvider(name, streetAddress, city, state, zipCode);

        Console.WriteLine("Successfully added provider.");
    }

    /// <summary>
    /// Prompts the user for a provider ID and a field to edit for that provider. If the user enters
    /// "name", the user can then input a new name for the provider. If the user enters "address",
    /// the user can then enter the components that will make up the provider's new address. The
    /// method will then update the provider with the given ID to have the new name or address.
    /// </summary>
    public void editProvider()
    {
        int providerId = UserInterface.getInteger("Enter provider ID: ", 9, 9);

        // Validate the provider exists before proceeding
        if (providerManager.validateProvider(providerId))
        {
            String choice = UserInterface.getString("Enter field to edit(name or address): ");

            // If the user wants to edit the name
            if (choice.ToLower().Equals("name"))
            {
                String name = UserInterface.getString("Enter a new provider name: ", 1, 25);

                providerManager.editProviderName(providerId, name);
                Console.WriteLine("Successfully edited provider name.");
       
            }
            // If the user wants to edit the address
            else if (choice.ToLower().Equals("address"))
            {
                String streetAddress = UserInterface.getString("Enter provider street address: ", 1, 25);
                String city = UserInterface.getString("Enter provider city: ", 1, 14);
                String state = UserInterface.getString("Enter provider state: ", 2, 2);
                int zipCode = UserInterface.getInteger("Enter provider ZIP code: ", 5, 5);

                providerManager.editProviderAddress(providerId, streetAddress, city, state, zipCode);
                Console.WriteLine("Successfully edited provider address.");
            }
            // If the user didn't enter "name" or "address"
            else
            {
                Console.WriteLine("Unknown field entered. Valid fields: name, address.");
            }
        }
        else
        {
            Console.WriteLine("Unable to find provider [" + providerId + "].");
        }
    }

    /// <summary>
    /// Prompts the user for a provider ID. If the provider exists in the system,
    /// the method will call the provider manager method to delete the provider.
    /// </summary>
    public void deleteProvider()
    {
        int providerId = UserInterface.getInteger("Enter the provider ID: ", 9, 9);

        // Validate the provider exists before proceeding
        if (providerManager.validateProvider(providerId))
        {
            providerManager.deleteProvider(providerId);
            Console.WriteLine("Successfully deleted provider.");
        }
        else
        {
            Console.WriteLine("Unable to find provider [" + providerId + "].");
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
        int providerId = UserInterface.getInteger("Enter provider ID: ", 9, 9);

        // Validate the provider exists before proceeding
        if (providerManager.validateProvider(providerId))
        {
            int serviceId = UserInterface.getInteger("Enter service ID: ", 6, 6);

            // Validate the service exists before adding it to the provider
            if (serviceManager.validateService(serviceId))
            {
                providerManager.addService(providerId, serviceId);

                Console.WriteLine("Successfully added service to provider.");
            }
            else
            {
                Console.WriteLine("Service ID [" + serviceId + "] is not valid.");
            }
        }
        else
        {
            Console.WriteLine("Provider ID [" + providerId + "] is not valid.");
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
        int providerId = UserInterface.getInteger("Enter provider ID: ", 9, 9);

        // Validate the provider exists before proceeding
        if (providerManager.validateProvider(providerId))
        {
            int serviceId = UserInterface.getInteger("Enter service ID: ", 6, 6);

            // Validate the service is provided by the provider before deleting it
            if (serviceManager.validateService(serviceId))
            {
                providerManager.deleteService(providerId, serviceId);

                Console.WriteLine("Successfully deleted service from provider.");
            }
            else
            {
                Console.WriteLine("Service ID [" + serviceId + "] is not valid.");
            }
        }
        else
        {
            Console.WriteLine("Provider ID [" + providerId + "] is not valid.");
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

        // If the provider doesn't provide any services
        if (providerServices.Count == 0)
        {
            Console.WriteLine("This provider doesn't provide any services.");
        }

        // Display each service ID and name foreach service provided by the provider
        foreach (int serviceID in providerServices)
        {
            service = serviceManager.getServiceById(serviceID);
            Console.WriteLine(serviceID + "   " + service.Name);
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
        int memberId = UserInterface.getInteger("Enter the member's ID: ", 9, 9);
        Member member = memberManager.getMemberById(memberId);

        // Validate the member exists before proceeding
        if (member != null && member.Suspended == false)
        {
            String dateOfService = UserInterface.getDate("Enter the date the service was provided (Format: MM-DD-YYYY): ");
            int serviceId = UserInterface.getInteger("Enter the ID of the service provided: ");

            // Validate the service is provided by the provider before proceeding
            if (providerManager.validateService(providerId, serviceId))
            {
                // Verify with the user that the service is the correct one
                if (UserInterface.yesOrNo("Is [" + serviceManager.getServiceById(serviceId).Name + "] the correct service? "))
                {
                    // Allow the user to enter an optional comment about the service provided
                    String comments = UserInterface.getString("Enter comments [optional]: ", 0, 100);

                    String currentTime = DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Year.ToString() +
                     " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();

                    // Check to make sure the captured services file exists before loading it. If not, create it
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

                    // Get the entity object associated with this capture
                    Provider provider = providerManager.getProviderById(providerId);
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

                    Console.WriteLine("Successfully captured service.");
                }
                // If the user selects no when prompted if the service is correct
                else
                {
                    Console.WriteLine("Exiting to menu. No service was captured.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Service [" + serviceId + "] is not listed as a service provided by provider [" + providerId + "].");
            }
        }
        else if (member != null && member.Suspended == true)
        {
            Console.WriteLine("Member [" + memberId + "] is suspended. Cannot provide a service to a suspended member.");
        }
        else
        {
            Console.WriteLine("Member [" + memberId + "] doesn't exist in the system.");
        }
    }

    /// <summary>
    /// Determines whether a provider with the given provider ID exists in the system
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns>True if the provider exists, False otherwise</returns>
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
            Console.WriteLine("No providers in the database.");
        }
        else
        {
            Console.WriteLine(providersString);
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

        // Add the service to the system with the given attributes
        serviceManager.addService(name, fee, description);

        Console.WriteLine("Successfully added service.");
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
        int serviceId = UserInterface.getInteger("Enter service ID: ", 6, 6);

        // Verify the service exists before proceeding
        if (serviceManager.validateService(serviceId))
        {
            String choice = UserInterface.getString("Enter field to edit(name, fee, description): ");

            // If the user wants to edit the name
            if (choice.ToLower().Equals("name"))
            {
                String name = UserInterface.getString("Enter new service name: ", 1, 25);

                serviceManager.editServiceName(serviceId, name);
                Console.WriteLine("Successfully edited service name.");
            }
            // If the user wants to edit the fee
            else if (choice.ToLower().Equals("fee"))
            {
                double fee = UserInterface.getDouble("Enter new service fee: ", 0.0, 999.99, 2);

                serviceManager.editServiceFee(serviceId, fee);
                Console.WriteLine("Successfully edited service name.");
            }
            // If the user wants to edit the description
            else if (choice.ToLower().Equals("description"))
            {
                String description = UserInterface.getString("Enter new service description: ", 1, 100);

                serviceManager.editServiceDescription(serviceId, description);
                Console.WriteLine("Successfully edited service description.");
            }
            // If the user didn't enter name, fee, or description
            else
            {
                Console.WriteLine("Unknown field entered. Valid fields: name, fee, description");
            }
        }
        else
        {
            Console.WriteLine("Unable to find service [" + serviceId + "].");
        }
    }

    /// <summary>
    /// Prompts the user for a service ID. If the service exists in the system,
    /// the method will call the service manager method to delete the service.
    /// </summary>
    public void deleteService()
    {
        int serviceId = UserInterface.getInteger("Enter the service ID: ", 6, 6);

        // Validate the service exists before deleting it
        if (serviceManager.validateService(serviceId))
        {
            // Delete the service from each provider that provides it
            foreach (Provider provider in providerManager.ProviderList)
            {
                provider.removeService(serviceId);
            }

            // Delete the service from the complete list of services
            serviceManager.deleteService(serviceId);
            Console.WriteLine("Service deleted.");
        }
        // 
        else
        {
            Console.WriteLine("Unable to find service [" + serviceId + "].");
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
            Console.WriteLine("No services in the database.");
        }
        else
        {
            Console.WriteLine(servicesString);
        }
    }

    /// <summary>
    /// Prompts the user to enter a member ID and asks the report class to run a report for that member.
    /// </summary>
    public void printMemberReport()
    {
        int memberId = UserInterface.getInteger("Enter the member's ID: ", 9, 9);

        report.getMemberReport(memberId);
    }

    /// <summary>
    /// Asks the report class to generate a report for each member in the system.
    /// </summary>
    public void printWeeklyMembersReport()
    {
        report.getWeeklyMembersReport();
    }

    /// <summary>
    /// Prompts the user to enter a provider ID and asks the report class to run a report for that provider.
    /// If the optional parameter is not specified, the user will be prompted to enter a provider ID.
    /// </summary>
    /// <param name="providerId">Optional parameter that specifies the provider to run the report on</param>
    public void printProviderReport(int providerId = -1)
    {
        if (providerId == -1)
        {
            providerId = UserInterface.getInteger("Enter the provider's ID: ", 9, 9);
        }

        report.getProviderReport(providerId);
    }

    /// <summary>
    /// Asks the report class to generate a report for each provider in the system.
    /// </summary>
    public void printWeeklyProvidersReport()
    {
        report.getWeeklyProvidersReport();
    }

    /// <summary>
    /// Asks the report class to generate a summary of all the provider reports
    /// </summary>
    public void printSummaryReport()
    {
        report.getProviderSummary();
    }

    /// <summary>
    /// Asks the report class to generate an Electronic Funds Transfer file of all providers for the account manager
    /// </summary>
    public void printEFTReport()
    {
        report.createEFT();
    }

    /// <summary>
    /// Saves the current member, provider, and service manager's information into separate files
    /// </summary>
    public void save()
    {
        memberManager.save();
        providerManager.save();
        serviceManager.save();

        Console.WriteLine("Saved member, provider, and service information to disk.");
    }

    /// <summary>
    /// Loads the stored member, provider, and service manager's information into memory
    /// </summary>
    public void load()
    {
        memberManager.load();
        providerManager.load();
        serviceManager.load();
    }
}
