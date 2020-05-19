using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Mini_bot_sushi
{
    public class User : UserBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<Menu> Order = new List<Menu>();
        public delegate void OrderExecution(string message);
        public event OrderExecution Notify;

        public void GetName()
        {
            try
            {
                Console.WriteLine("How can I call you?");
                Name = Console.ReadLine();
                char[] charsToTrim = { ' ', '.', ',', ';', '\'', '/', '`', '-', '=', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
                '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', ':', '"', '|', '\\', '<', '>', '?'};
                Name = Name.Trim(charsToTrim);
                Name = Name.ToLower();
                Name = char.ToUpper(Name[0]) + Name.Substring(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}\nField \"Name\" is empty!");
                Logger.Error(ex, Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.Namespace, MethodBase.GetCurrentMethod(), Thread.CurrentThread);
                Name = "John Doe";
            }
        }

        public void GetOrder(Menu menu, User user)
        {
            int tempProductNumber;
            Console.WriteLine($"\nWhat do you wish to do? v - view product information, any other - make a choice.");
            while (Console.ReadKey().Key == ConsoleKey.V)
            {
                Console.WriteLine($"\n\nWhat product information do you want to see? Enter № of product and press Enter");
                int.TryParse(Console.ReadLine(), out tempProductNumber);
                Console.Write("\n");
                menu.GetInfoAboutProduct(menu, tempProductNumber);
                Console.WriteLine($"\nWhat do you wish to do? v - view product information, any other - make a choice.");
            }
            int tempQuantity;
            Console.WriteLine($"\nEnter M to make a choice or any other to exit");
            while (Console.ReadKey().Key == ConsoleKey.M)
            {
                Console.WriteLine($"\n\nChoose your desired sushi and their quantity. Enter № of product and press Enter");
                int.TryParse(Console.ReadLine(), out tempProductNumber);
                Console.WriteLine($"\nEnter quantity and press Enter");
                int.TryParse(Console.ReadLine(), out tempQuantity);
                Console.Write("\n");
                for (int x = tempQuantity; x > 0; x--)
                {
                    user.AddToOrder(menu.GetProductForOrder(menu, tempProductNumber));
                }
                Console.WriteLine($"Enter M to make a choice or any other to exit");
            }
            Console.Write("\n");
            Notify?.Invoke($"The order is formed");
        }

        public void AddToOrder(Menu menu)
        {
            Order.Add(menu);
        }

        public void GetEmail()
        {
            Console.WriteLine("Enter your email:");
            Email = Console.ReadLine();
            Console.Write("\n");
            if (Email.Equals(""))
            {
                Console.WriteLine($"You moron!");
                Environment.Exit(666);
            }
        }

        public void GetAddress()
        {
            Console.WriteLine("Enter your address:");
            Address = Console.ReadLine();
            Console.Write("\n");
            if (Address.Equals(""))
            {
                Console.WriteLine($"You moron!");
                Environment.Exit(666);
            }
        }
    }
}
