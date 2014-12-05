using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

/// <summary>
/// UserInterface Class: 
/// The UserInerface class contains menus that display user options  
/// and methods that allow the system to interact with the user.
/// The user interface has minimal knowledge about the rest of the
/// system and only interacts with the PizzaAnonymous class.
///  
/// Author: Rob Perpich
/// Date Created: November 6, 2014
/// Last Modified By: Rob Perpich
/// Date Last Modified: November 16, 2014
/// </summary>
public class UserInterface
{
    private static UserInterface userInterface;
    private static PizzaAnonymous pizzaAnonymous;

    /// <summary>
    /// Private singleton constructor
    /// </summary>
    private UserInterface()
    {
        pizzaAnonymous = PizzaAnonymous.instance();

        // Prompt user to load saved Members, Providers, and Services
        if (yesOrNo("Do you want to load saved data?"))
        {
            pizzaAnonymous.load();
        }
    }

    /// <summary>
    /// Public singleton constructor
    /// </summary>
    /// <returns>Returns the reference to the only instance of the UI</returns>
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

    /// <summary>
    /// Displays the main menu of the program. Allows switching between 
    /// the manager menu and the provider menu.
    /// </summary>
    public void displayMainMenu()
    {
        bool done = false;		
        int choice;				    
        
        while (!done)
        {
            Console.WriteLine("                                         ");
            Console.WriteLine("        MAIN MENU                        ");
            Console.WriteLine("        Pizza-anonymous                  ");
            Console.WriteLine("                                         ");
            Console.WriteLine("        1. Display Manager Menu          ");
            Console.WriteLine("        2. Display Provider Menu         ");
            Console.WriteLine("        3. Quit                          ");
            Console.WriteLine("                                         ");

            choice = getInteger("Enter your choice: ");
            Console.WriteLine();

            switch (choice)
            {
                case  1:    displayManagerMenu();
                            break;
                case  2:    displayProviderMenu();
                            break;
                case  3:    done = true;
                            break;
                default:    Console.WriteLine("Not a valid choice! Please try again.\n");
                            break;     
            } 
        }   
    }

    /// <summary>
    /// Displays the manager menu which contains options that only
    /// a Pizza Anonymous employee would have access to.
    /// </summary>
    private void displayManagerMenu ()
    {
        bool done = false;		
        int choice;				    
        
        while (!done)
        {
            Console.WriteLine("                                         ");
        	Console.WriteLine("        MANAGER MENU                     ");
        	Console.WriteLine("        Pizza-anonymous                  ");
        	Console.WriteLine("                                         ");
        	Console.WriteLine("         1. Add Member                   ");
        	Console.WriteLine("         2. Edit Member                  ");
        	Console.WriteLine("         3. Delete Member                ");
        	Console.WriteLine("         4. Suspend Member               ");
            Console.WriteLine("         5. Print All Members            ");
            Console.WriteLine("                                         ");
        	Console.WriteLine("         6. Add Provider                 ");
            Console.WriteLine("         7. Edit Provider                ");
            Console.WriteLine("         8. Delete Provider              ");
            Console.WriteLine("         9. Add Service To Provider      ");
            Console.WriteLine("        10. Delete Service From Provider ");
            Console.WriteLine("        11. Print All Providers          ");
            Console.WriteLine("                                         ");
            Console.WriteLine("        12. Add Service                  ");
            Console.WriteLine("        13. Edit Service                 ");
            Console.WriteLine("        14. Delete Service               ");  
            Console.WriteLine("        15. Print All Services           ");
            Console.WriteLine("                                         ");
            Console.WriteLine("        16. Print Member Report          ");
            Console.WriteLine("        17. Print Weekly Members Report  ");
            Console.WriteLine("        18. Print Provider Report        ");
            Console.WriteLine("        19. Print Weekly Providers Report");
            Console.WriteLine("        20. Print Summary Report         ");
            Console.WriteLine("        21. Print EFT Report             ");
            Console.WriteLine("                                         ");
            Console.WriteLine("        22. Save                         ");
            Console.WriteLine("        23. Display Main Menu            ");
            Console.WriteLine("                                         ");

            choice = getInteger("Enter your choice: ", 1, 2);
            Console.WriteLine();
         
            switch (choice)
            {
                case  1:   	pizzaAnonymous.addMember();
                           	break;
                case  2:   	pizzaAnonymous.editMember();
                           	break;
                case  3:   	pizzaAnonymous.deleteMember();
                           	break;
                case  4:   	pizzaAnonymous.suspendMember();
                           	break;
                case  5:    pizzaAnonymous.printMembers();
                           	break;
                case  6:    pizzaAnonymous.addProvider(); 
                           	break;
                case  7:    pizzaAnonymous.editProvider(); 
                           	break;
                case  8:    pizzaAnonymous.deleteProvider();
                           	break;
                case  9:    pizzaAnonymous.addServiceToProvider(); 
                           	break;
                case 10:    pizzaAnonymous.deleteServiceFromProvider(); 
                           	break;
                case 11:    pizzaAnonymous.printProviders(); 
                			break;
                case 12:    pizzaAnonymous.addService();
    						break;
                case 13:    pizzaAnonymous.editService(); 
    						break;
                case 14:    pizzaAnonymous.deleteService();
                            break;
                case 15:    pizzaAnonymous.printServices();
                            break;
                case 16:    pizzaAnonymous.printMemberReport();
                            break;
                case 17:    pizzaAnonymous.printWeeklyMembersReport();
                            break;
                case 18:    pizzaAnonymous.printProviderReport();
                            break;
                case 19:    pizzaAnonymous.printWeeklyProvidersReport();
                            break;
                case 20:    pizzaAnonymous.printSummaryReport();
                            break;
                case 21:    pizzaAnonymous.printEFTReport();
                            break;
                case 22:    pizzaAnonymous.save();
                            break;        
                case 23:   	done = true;
     		   				break;
                default:   	Console.WriteLine("Not a valid choice! Please try again.\n");
                           	break;     
            }     
        }  
    }

    /// <summary>
    /// Displays the provider menu. This will prompt the user to "login" as 
    /// an existing provider and simulate the provider terminal.
    /// </summary>
    private void displayProviderMenu ()
    {
        bool done = false;
        int choice;

        int providerId = getInteger("Login. Enter Provider ID: ", 9, 9);

        if (!pizzaAnonymous.validateProviderId(providerId))
        {
            Console.WriteLine("Provider ID [" + providerId + "] is not valid.");
            return;
        }

        Console.WriteLine("Login Success.");

        while (!done)
        {
            Console.WriteLine("                                         ");
        	Console.WriteLine("        PROVIDER MENU                    ");
        	Console.WriteLine("        Pizza-anonymous                  ");
        	Console.WriteLine("                                         ");
        	Console.WriteLine("        1. Authenticate Member           ");
        	Console.WriteLine("        2. Provide Service               ");
        	Console.WriteLine("        3. Generate Report               ");
            Console.WriteLine("        4. Service Lookup                ");
        	Console.WriteLine("        5. Display Main Menu             ");
            Console.WriteLine("                                         ");

            choice = getInteger("Enter your choice: ");
            Console.WriteLine();
         
            switch (choice)
            {
                case  1:    pizzaAnonymous.validateMember();
                            break;
                case  2:    pizzaAnonymous.captureService(providerId);
                            break;
                case  3:    pizzaAnonymous.printProviderReport(providerId);
                            break;
                case  4:    pizzaAnonymous.serviceLookup(providerId);
                            break;
                case  5:    done = true;
                            break;
                default:    Console.WriteLine("Not a valid choice! Please try again.\n");
                            break;     
            }    
        }  
    }

    /// <summary>
    /// Prompts the user to enter a string into the console.
    /// </summary>
    /// <param name="prompt">The string that will prompt the user with what they should enter</param>
    /// <param name="min">The minimum number of characters the string must be</param>
    /// <param name="max">The maximum number of characters the string must be</param>
    /// <returns>The string input by the user</returns>
    public static String getString(String prompt, int min = 1, int max = 1000)
	{
        Console.Write(prompt);
        String input = Console.In.ReadLine(); 

        // While the string isn't in the desired range
        while (input.Length < min || input.Length > max)
        {
            Console.Write("Please input a string with length between [" + min + "] and [" + max + "] characters: ");
            input = Console.In.ReadLine(); 
        }

        return input;
	}

    /// <summary>
    /// Prompts the user to enter an integer into the console.
    /// </summary>
    /// <param name="prompt">The string that will prompt the user with what they should enter</param>
    /// <param name="min">The minimum number of digits that the integer must be</param>
    /// <param name="max">The maximum number of digits that the integer must be</param>
    /// <returns>The integer input by the user</returns>
    public static int getInteger(String prompt, int min = 1, int max = 20)
	{
        int number;

        Console.Write(prompt);
        String input = Console.ReadLine();

        // While the input isn't an integer or isn't in the desired range
        while (!Int32.TryParse(input, out number) || input.Length < min || input.Length > max)
        {
            Console.Write("Please input an integer with length between [" + min + "] and [" + max + "] digits: ");
            input = Console.In.ReadLine();

        }

        return number;
	}

    /// <summary>
    /// Prompts the user to enter a double into the console.
    /// </summary>
    /// <param name="prompt">The string that will prompt the user with what they should enter</param>
    /// <param name="min">The minimum value the double must be</param>
    /// <param name="max">The maximum value the double must be</param>
    /// <param name="places">The number of places after the decimal to round the double to</param>
    /// <returns>The double input by the user, rounded to the place specified by the caller</returns>
    public static double getDouble(String prompt, double min = 0.0, double max = 999.99, int places = 2)
	{
        double number;

        Console.Write(prompt);
        String input = Console.ReadLine();

        // While the input is not a double or isn't within the desired range
        while (!double.TryParse(input, out number) || number < min || number > max)
        {
            Console.Write("Please enter a decimal with value between [" + min + "] and [" + max + "]: ");
            input = Console.ReadLine();
        }
            
        // Round the number to the the number of places given by *places*
        number = (double)Math.Round(number * (Math.Pow(10, places))) / Math.Pow(10, places);
            
        return number;
	}

    /// <summary>
    /// Prompts the user to enter a date into the console.
    /// </summary>
    /// <param name="prompt">The string that will prompt the user with what they should enter</param>
    /// <param name="allowFuture">Determines whether the function will accept a future date as input</param>
    /// <returns>The date entered by the user in the form of a string (MM-DD-YYYY)</returns>
    public static String getDate(String prompt, bool allowFuture = false)
    {
        // Regular expression. Matches as string in the form MM-DD-YYYY where M,D, and Y are digits
        Regex regex = new Regex(@"^\d{2}-\d{2}-\d{4}$");

        Console.Write(prompt);
        String input = Console.In.ReadLine();
        Match match = regex.Match(input);

        bool done = false;

        // While the user input doesn't match the date format
        while (!done)
        {
            if (match.Success && (Convert.ToDateTime(input) > DateTime.Today && allowFuture == false))
            {
                Console.Write("Please enter a date that isn't in the future: ");
            }
            else if (!match.Success)
            {
                Console.Write("Please enter a date in the format MM-DD-YYYY: ");
            }
            else
            {
                break;
            }

            input = Console.In.ReadLine();
            match = regex.Match(input);
        }

        return input;
    }

    /// <summary>
    /// Prompts the user to enter yes or no into the console.
    /// </summary>
    /// <param name="prompt">The string that will prompt the user with what they should enter</param>
    /// <returns>A boolean value, true for yes and false for no</returns>
    public static bool yesOrNo(String prompt)
    {
        String more = getString(prompt + " (Y|y)[es] or anything else for no: ");

        // If the first letter of the input wasn't the letter 'Y'
        if (more.ElementAt(0) != 'y' && more.ElementAt(0) != 'Y')
        {
            return false;
        }

        return true;
    }
}
