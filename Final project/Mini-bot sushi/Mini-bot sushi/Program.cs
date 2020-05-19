using System;
using System.Reflection;
using System.Threading;

namespace Mini_bot_sushi
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Info("Program start!", Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.Namespace, MethodBase.GetCurrentMethod(), Thread.CurrentThread);
            Menu newMenu = new Menu();
            //newMenu.MenuItemCreation("tuna nigiri", "tuna, tuna flakes, mayonnaise, thai sweet chili sauce, wasabi",
            //    38, 12.40, 2.90, 37.40, 232, 3.10);
            //newMenu.MenuItemCreation("nigiri with perch", "sea bass, mayonnaise, thai sweet chili sauce, cashew nut, wasabi",
            //    33, 6.30, 3.80, 42.90, 221, 1.90);
            //newMenu.MenuItemCreation("salmon nigiri", "salmon, pickled takuan radish, thai sweet chili sauce, mayonnaise, nori seaweed, wasabi",
            //    39, 10.30, 6.30, 36.90, 249, 2.90);
            //newMenu.MenuItemCreation("yasai", "avocado, cucumber, iceberg salad, italian sauce, wasabi",
            //    40, 3.50, 7.50, 37.70, 232, 1.30);
            //newMenu.MenuItemCreation("tobiko red", "flying fish roe - red, wasabi",
            //    35, 8.40, 1.30, 37.20, 198, 2.70);
            //newMenu.MenuItemCreation("tobiko black", "flying fish roe - black, wasabi",
            //    35, 8.40, 1.30, 37.30, 197, 2.60);
            //newMenu.MenuItemCreation("california poppies 8 units", "crab sticks, avocado, cucumber, mayonnaise, orange flying fish roe, wasabi",
            //    232, 6.70, 3.70, 35.90, 206, 11.00);
            //newMenu.MenuItemCreation("philadelphia maki 8 units", "salmon, curd cheese, avocado, cucumber, sesame seeds, wasabi",
            //    226, 7.70, 10.00, 37.00, 271, 10.00);
            //newMenu.MenuItemCreation("tokyo maki 8 units", "tiger shrimp, curd cheese, tomato, avocado, flying red fish roe, wasabi",
            //    260, 7.90, 5.50, 31.40, 208, 16.10);
            User user = new User();
            user.GetName();
            user.AddUserInfo(user);
            //user.PrintUserBase();
            Console.WriteLine($"\nGood {user.TimeOfDay}, {user.Name}!\n");
            Console.WriteLine($"{user.Name}, voila our delicious menu!\n");
            newMenu.MenuLoading();
            newMenu.PrintBriefMenu();
            //newMenu.PrintMenu();
            Events newEvent = new Events();
            user.Notify += newEvent.Message;
            user.GetOrder(newMenu, user);
            user.GetEmail();
            user.UpdateUserInfo(user);
            user.GetAddress();
            user.UpdateUserInfo(user);
            string stringOrder = user.StringUserOrder(user);
            Console.WriteLine(stringOrder);
            //user.PrintUserBase();
            //user.SendEmail(user, stringOrder);
        }
    }
}
