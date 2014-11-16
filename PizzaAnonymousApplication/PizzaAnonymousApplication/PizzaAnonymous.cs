using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace PizzaAnonymousApplication
{
    public class PizzaAnonymous
    {
        private static PizzaAnonymous pizzaAnonymous;
        private static MemberManager memberManager;
        private static ProviderManager providerManager;
        private static ServiceManager serviceManager;

        private PizzaAnonymous()
        {
            memberManager = MemberManager.instance();
            providerManager = ProviderManager.instance();
            serviceManager = ServiceManager.instance();
        }

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

        public void printProviders()
        {
            String providersString = providerManager.toString();

            if (providersString == "")
            {
                Console.Out.WriteLine("No providers in the database.");
            }
            else
            {
                Console.Out.WriteLine(providersString);
            }
        }

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

        public bool validateProviderId(int providerId)
        {
            return providerManager.validateProvider(providerId);
        }

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

        public void serviceLookup(int providerId)
        {
            Provider provider = providerManager.getProviderById(providerId);
            List<int> providerServices = provider.getServiceList();

            if (providerServices.Count == 0)
            {
                Console.Out.WriteLine("This provider doesn't provide any services.");
            } 

            foreach (int service in providerServices)
            {
                Console.Out.WriteLine(service);
            }
        }

        public void addMember()
        {
            String name = UserInterface.getString("Enter the member's name: ", 1, 25);
            String streetAddress = UserInterface.getString("Enter the member's street address: ", 1, 25);
            String city = UserInterface.getString("Enter the member's city: ", 1, 14);
            String state = UserInterface.getString("Enter the member's state: ", 2, 2);
            int zipCode = UserInterface.getInteger("Enter the member's ZIP code: ", 5, 5);

            memberManager.addMember(name, streetAddress, city, state, zipCode);

            Console.Out.WriteLine("Successfully added member.");
        }

        public void printMembers()
        {
            String membersString = memberManager.toString();

            if (membersString == "")
            {
                Console.Out.WriteLine("No members in the database.");
            }
            else
            {
                Console.Out.WriteLine(membersString);
            }
        }

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
                Console.Out.WriteLine("Unable to find member [" + memberId +"]");
            }
        }

        public void editMember()
        {
            int memberId = UserInterface.getInteger("Enter provider ID: ");

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

        public void addService()
        {
            String name = UserInterface.getString("Enter the service's name: ", 1, 25);
            double fee = UserInterface.getDouble("Enter the service's fee: ", 0.0, 999.99, 2);
            String description = UserInterface.getString("Enter the service's description: ", 1, 100);

            serviceManager.addService(name, fee, description);
            Console.Out.WriteLine("Successfully added service.");
        }

        public void printServices()
        {
            String servicesString = serviceManager.toString();

            if (servicesString == "")
            {
                Console.Out.WriteLine("No services in the database.");
            }
            else
            {
                Console.Out.WriteLine(servicesString);
            }
        }

        public void deleteService()
        {
            int serviceId = UserInterface.getInteger("Enter the service ID: ");

            if (serviceManager.validateService(serviceId))
            {
                foreach (Provider provider in providerManager.getProviderList())
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

        public void save()
        {
            memberManager.save();
            providerManager.save();
            serviceManager.save();
        }

        public void load()
        {
            memberManager.load();
            providerManager.load();
            serviceManager.load();
        }

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
                            xmlRoot = new XElement("CapturedServices");
                            doc.Add(xmlRoot);
                        }

                        XElement capturedService = 
                            new XElement("CapturedService",
                                new XElement("CurrentDate", currentTime),
                                new XElement("DateOfService", dateOfService),
                                new XElement("ProviderNumber", providerId),
                                new XElement("MemberNumber", memberId),
                                new XElement("ServiceCode", serviceId),
                                new XElement("Comments", comments));

                        xmlRoot.Add(capturedService);
                        doc.Save(file);
                        Console.Out.WriteLine(System.IO.File.ReadAllText(file));
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
        }
    }
}
