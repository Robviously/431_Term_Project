using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PizzaAnonymousApplication
{
    class UserInterface
    {
        private static UserInterface userInterface;
        private static PizzaAnonymous pizzaAnonymous;

        private UserInterface()
        {
            pizzaAnonymous = PizzaAnonymous.instance();

            if (yesOrNo("Do you want to load saved data?"))
            {
                pizzaAnonymous.load();
            }
        }

        public static UserInterface instance()
        {
            if (userInterface == null)
            {
                return userInterface = new UserInterface();
            }
            else
            {
                return userInterface;
            }
        }

        public void displayMainMenu()
        {
            bool done = false;		
            int choice;				    
        
            while (!done)
            {
                Console.Out.WriteLine("                                     ");
                Console.Out.WriteLine("        MAIN MENU                    ");
                Console.Out.WriteLine("        Pizza-anonymous              ");
                Console.Out.WriteLine("                                     ");
                Console.Out.WriteLine("        1. Display Manager Menu      ");
                Console.Out.WriteLine("        2. Display Provider Menu     ");
                Console.Out.WriteLine("        3. Quit                      ");
                Console.Out.WriteLine("                                     ");
                Console.Out.WriteLine("        4. Save                      ");

                choice = getInteger("Enter your choice: ");

                switch (choice)
                {
                    case 1:   displayManagerMenu();
                              break;

                    case 2:   displayProviderMenu();
                              break;

                    case 3:   done = true;
                              break;

                    case 4:   pizzaAnonymous.save();
                              break;

                    default:  Console.Out.WriteLine("Not a valid choice! Please try again.\n");
                              break;     
                } 
            }   
        }

        private void displayManagerMenu ()
        {
            bool done = false;		
            int choice;				    
        
            while (!done)
            {
                Console.Out.WriteLine("                                     ");
        	    Console.Out.WriteLine("        MANAGER MENU                 ");
        	    Console.Out.WriteLine("        Pizza-anonymous              ");
        	    Console.Out.WriteLine("                                     ");
        	    Console.Out.WriteLine("         1. Add Member               ");
        	    Console.Out.WriteLine("         2. Edit Member              ");
        	    Console.Out.WriteLine("         3. Delete Member            ");
        	    Console.Out.WriteLine("         4. Add Provider             ");
                Console.Out.WriteLine("         5. Edit Provider            ");
                Console.Out.WriteLine("         6. Delete Provider          ");
                Console.Out.WriteLine("         7. Add Service              ");
                Console.Out.WriteLine("         8. Edit Service             ");
                Console.Out.WriteLine("         9. Delete Service           "); 
                Console.Out.WriteLine("        10. Show Members             "); 
                Console.Out.WriteLine("        11. Show Providers           "); 
                Console.Out.WriteLine("        12. Show Services            ");
                Console.Out.WriteLine("        13. Add Service To Provider  ");
                Console.Out.WriteLine("        14. Display Main Menu        ");
                Console.Out.WriteLine("                                     ");

                choice = getInteger("Enter your choice: ");
         
                switch (choice)
                {
                    case  1:   	pizzaAnonymous.addMember();
                           	    break;

                    case  2:   	pizzaAnonymous.editMember();
                           	    break;

                    case  3:   	pizzaAnonymous.deleteMember();
                           	    break; 
                           
                    case  4:   	pizzaAnonymous.addProvider();
                           	    break;  
                           
                    case  5:   	pizzaAnonymous.editProvider();
                           	    break; 
                           
                    case  6:   	pizzaAnonymous.deleteProvider();
                           	    break;            
                           
                    case  7:   	pizzaAnonymous.addService();
                           	    break;            
                           
                    case  8:   	pizzaAnonymous.editService();
                           	    break;            
                           
                    case  9:   	pizzaAnonymous.deleteService();
                           	    break;  
                           
                    case 10:   	pizzaAnonymous.printMembers();
                			    break;
                			
                    case 11:   	pizzaAnonymous.printProviders();
    						    break;
    						
                    case 12:   	pizzaAnonymous.printServices();
    						    break;

                    case 13:    pizzaAnonymous.addServiceToProvider();
                                break;
                           
                    case 14:   	done = true;
     		   				    break;    

                    default:   	Console.Out.WriteLine("Not a valid choice! Please try again.\n");
                           	    break;     
                }     
            }  
        }

        private void displayProviderMenu ()
        {
            bool done = false;
            int choice;

            int providerId = getInteger("Enter Provider ID: ");

            if (!pizzaAnonymous.validateProviderId(providerId))
            {
                Console.Out.WriteLine("Provider ID [" + providerId + "] is not valid.");
                return;
            }

            while (!done)
            {
                Console.Out.WriteLine("                                     ");
        	    Console.Out.WriteLine("        PROVIDER MENU                ");
        	    Console.Out.WriteLine("        Pizza-anonymous              ");
        	    Console.Out.WriteLine("                                     ");
        	    Console.Out.WriteLine("        1. Authenticate Member       ");
        	    Console.Out.WriteLine("        2. Provide Service           ");
        	    Console.Out.WriteLine("        3. Generate Report           ");
        	    Console.Out.WriteLine("        4. Display Main Menu         ");
                Console.Out.WriteLine("                                     ");

                choice = getInteger("Enter your choice: ");
         
                switch (choice)
                {
                    case 1:    int memberId = getInteger("Enter the member's ID: ");
                               pizzaAnonymous.validateMember(memberId);
                               break;

                    case  2:   pizzaAnonymous.captureService(providerId);
                               break;
                            
                    case  3:   Console.Out.WriteLine("Generate Report Method"); //generateReport();
                               break;

                    case  4:   done = true;
                               break;

                    default:   Console.Out.WriteLine("Not a valid choice! Please try again.\n");
                               break;     
                }    
            }  
        }

        public static String getString(String prompt, int min = 1, int max = 1000)
	    {
            Console.Out.WriteLine(prompt);
            String input = Console.In.ReadLine(); 

            while (input.Length < min || input.Length > max)
            {
                Console.Out.WriteLine("Please input a string with length between [" + min + "] and [" + max + "] characters: ");
                input = Console.In.ReadLine(); 
            }

            return input;
	    }

        public static int getInteger(String prompt, int min = 1, int max = 20)
	    {
            int number;

            Console.Out.Write(prompt);
            String input = Console.ReadLine();

            while (!Int32.TryParse(input, out number) || input.Length < min || input.Length > max)
            {
                Console.Out.WriteLine("Please input an integer with length between [" + min + "] and [" + max + "] digits: ");
                input = Console.In.ReadLine();

            }

            return number;
	    }

        public static double getDouble(String prompt, double min = 0.0, double max = 999.99, int places = 2)
	    {
            double number;

            Console.Out.Write(prompt);
            String input = Console.ReadLine();

            while (!double.TryParse(input, out number) || number < min || number > max)
            {
                Console.WriteLine("Please enter a decimal with value between [" + min + "] and [" + max + "]: ");
                input = Console.ReadLine();
            }
            
            // Round the number to the the number of places given by *places*
            number = (double)Math.Round(number * (Math.Pow(10, places))) / Math.Pow(10, places);
            
            return number;
	    }

        public static String getDate(String prompt)
        {
            Regex regex = new Regex(@"\d{2}-\d{2}-\d{4}");

            Console.Out.WriteLine(prompt);
            String input = Console.In.ReadLine();
            Match match = regex.Match(input);

            while (!match.Success)
            {
                Console.Out.WriteLine("Please enter a date in the format MM-DD-YYYY: ");
                input = Console.In.ReadLine();
                match = regex.Match(input);
            }

            return input;
        }

        public static bool yesOrNo(String prompt)
        {
            String more = getString(prompt + " (Y|y)[es] or anything else for no");
            if (more.ElementAt(0) != 'y' && more.ElementAt(0) != 'Y')
            {
                return false;
            }
            return true;
        }
    }
}
