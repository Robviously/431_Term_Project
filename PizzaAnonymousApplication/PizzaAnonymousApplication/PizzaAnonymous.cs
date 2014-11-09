using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAnonymousApplication
{
    class PizzaAnonymous
    {
        private static PizzaAnonymous pizzaAnonymous;
        private static ProviderManager providerManager;
        //private static MemberManager memberManager;
        //private static ServiceManager serviceManager;

        private PizzaAnonymous()
        {
            providerManager = new ProviderManager();
            //memberManager = new MemberManager();
            //serviceManager = new ServiceManager();
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
            String name = UserInterface.getString("Enter the provider's name: ");
            String streetAddress = UserInterface.getString("Enter the provider's street address: ");
            String city = UserInterface.getString("Enter the provider's city: ");
            String state = UserInterface.getString("Enter the provider's state: ");
            int zipCode = UserInterface.getInteger("Enter the provider's ZIP code: ");

            providerManager.addProvider(name, streetAddress, city, state, zipCode);

            Console.Out.WriteLine("Success adding provider!");
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
                Console.Out.WriteLine("Provider deleted.");
            }
            else
            {
                Console.Out.WriteLine("Unable to find provider.");
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
                    String name = UserInterface.getString("Enter provider name: ");

                    providerManager.editProviderName(providerId, name);
                    Console.Out.WriteLine("Provider name edited.");
       
                }
                else if (choice.ToLower().Equals("address"))
                {
                    String streetAddress = UserInterface.getString("Enter provider street address: ");
                    String city = UserInterface.getString("Enter provider city: ");
                    String state = UserInterface.getString("Enter provider state: ");
                    int zipCode = UserInterface.getInteger("Enter provider ZIP code: ");

                    providerManager.editProviderAddress(providerId, streetAddress, city, state, zipCode);
                    Console.Out.WriteLine("Provider address edited.");
                }
                else
                {
                    Console.Out.WriteLine("Unknown field entered. Valid fields: name, address.");
                }
            }
            else
            {
                Console.Out.WriteLine("Unable to find provider");
            }
        }

        public void addMember()
        {
            Console.Out.WriteLine("addMember Method");
            /*
            String name = UserInterface.getString("Enter the member's name: ");
            String streetAddress = UserInterface.getString("Enter the member's street address: ");
            String city = UserInterface.getString("Enter the member's city: ");
            String state = UserInterface.getString("Enter the member's state: ");
            int zipCode = UserInterface.getInteger("Enter the member's ZIP code: ");

            memberManager.addMember(name, streetAddress, city, state, zipCode);

            Console.Out.WriteLine("Success adding member!");
             */
        }

        public void printMembers()
        {
            Console.Out.WriteLine("printMembers Method");
            /*
            String membersString = memberManager.toString();

            if (membersString == "")
            {
                Console.Out.WriteLine("No members in the database.");
            }
            else
            {
                Console.Out.WriteLine(membersString);
            }
             * */
        }

        public void deleteMember()
        {
            Console.Out.WriteLine("deleteMember Method");
            /*
            int memberId = UserInterface.getInteger("Enter the member ID: ");

            if (memberManager.validateMember(memberId))
            {
                memberManager.deleteMember(memberId);
                Console.Out.WriteLine("Member deleted.");
            }
            else
            {
                Console.Out.WriteLine("Unable to find member");
            }
             * */
        }

        public void editMember()
        {
            Console.Out.WriteLine("editMember Method");
            /*
            int memberId = UserInterface.getInteger("Enter provider ID: ");

            if (memberManager.validateMember(memberId))
            {
                String choice = UserInterface.getString("Enter field to edit(name or address): ");

                if (choice.ToLower().Equals("name"))
                {
                    String name = UserInterface.getString("Enter member name: ");

                    memberManager.editMemberName(memberId, name);
                    Console.Out.WriteLine("Member name edited.");

                }
                else if (choice.ToLower().Equals("address"))
                {
                    String streetAddress = UserInterface.getString("Enter member street address: ");
                    String city = UserInterface.getString("Enter member city: ");
                    String state = UserInterface.getString("Enter member state: ");
                    int zipCode = UserInterface.getInteger("Enter member ZIP code: ");

                    memberManager.editMemberAddress(memberId, streetAddress, city, state, zipCode);
                    Console.Out.WriteLine("Member address edited.");
                }
                else
                {
                    Console.Out.WriteLine("Unknown field entered. Valid fields: name, address.");
                }
            }
            else
            {
                Console.Out.WriteLine("Unable to find member.");
            }
             */
        }

        public void addService()
        {
            Console.Out.WriteLine("addService Method");
            /*
            String name = UserInterface.getString("Enter the service's name: ");
            float fee = UserInterface.getFloat("Enter the service's fee: ");
            String description = UserInterface.getString("Enter the service's description: ");

            serviceManager.addService(name, fee, description);
            Console.Out.WriteLine("Success adding service!");
            */
        }

        public void printServices()
        {
            Console.Out.WriteLine("printServices Method");
            /*
            String servicesString = serviceManager.toString();

            if (servicesString == "")
            {
                Console.Out.WriteLine("No services in the database.");
            }
            else
            {
                Console.Out.WriteLine(servicesString);
            }
            */
        }

        public void deleteService()
        {
            Console.Out.WriteLine("deleteService Method");
            /*
            int serviceId = UserInterface.getInteger("Enter the service ID: ");

            if (serviceManager.validateService(serviceId))
            {
                serviceManager.deleteService(serviceId);
                Console.Out.WriteLine("Service deleted.");
            }
            else
            {
                Console.Out.WriteLine("Unable to find service");
            }
            */
        }

        public void editService()
        {
            Console.Out.WriteLine("editService Method");
            /*
            int serviceId = UserInterface.getInteger("Enter service ID: ");

            if (serviceManager.validateService(serviceId))
            {
                String choice = UserInterface.getString("Enter field to edit(name, fee, description): ");

                if (choice.ToLower().Equals("name"))
                {
                    String name = UserInterface.getString("Enter service name: ");

                    serviceManager.editServiceName(serviceId, name);
                    Console.Out.WriteLine("Service name edited.");
                }
                else if (choice.ToLower().Equals("fee"))
                {
                    float fee = UserInterface.getFloat("Enter service fee: ");

                    serviceManager.editServiceFee(serviceId, fee);
                    Console.Out.WriteLine("Service fee edited.");
                }
                else if (choice.ToLower().Equals("description"))
                {
                    String description = UserInterface.getString("Enter service description");

                    serviceManager.editServiceDescription(serviceId, description);
                    Console.Out.WriteLine("Service description edited.");
                }
                else
                {
                    Console.Out.WriteLine("Unknown field entered. Valid fields: name, fee, description");
                }
            }
            else
            {
                Console.Out.WriteLine("Unable to find service");
            }
            */
        }
    }
}
