using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Mini_bot_sushi
{
    public class Menu
    {
        public string NameOfSushi { get; set; }
        public string Composition { get; set; }
        public int Weight { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public int Calories { get; set; }
        public double Price { get; set; }
        private protected List<Menu> menuBase = new List<Menu>();
        private protected HashSet<string> menuSet = new HashSet<string>();

        public void MenuItemCreation(string nameOfSushi, string composition, int weight, double proteins, double fats,
            double carbohydrates, int calories, double price)
        {
            Menu newItem = new Menu()
            {
                NameOfSushi = nameOfSushi,
                Composition = composition,
                Weight = weight,
                Proteins = proteins,
                Fats = fats,
                Carbohydrates = carbohydrates,
                Calories = calories,
                Price = price,
            };
            menuBase.Add(newItem);
            menuSet.Add(newItem.NameOfSushi);
            string jsonString;
            string jsonStringSet;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            jsonString = JsonSerializer.Serialize(newItem, options);
            jsonStringSet = JsonSerializer.Serialize(menuSet, options);
            File.WriteAllText($"{newItem.NameOfSushi}.json", jsonString);
            File.WriteAllText("Menu.json", jsonStringSet);
        }

        public void MenuLoading()
        {
            string jsonString;
            string jsonStringSet;
            Menu newItem = new Menu();
            jsonStringSet = File.ReadAllText("Menu.json");
            menuSet = JsonSerializer.Deserialize<HashSet<string>>(jsonStringSet);
            foreach (var x in menuSet)
            {
                jsonString = File.ReadAllText($"{x}.json");
                newItem = (JsonSerializer.Deserialize<Menu>(jsonString));
                menuBase.Add(newItem);
            }
        }

        public void PrintBriefMenu()
        {
            int counter = 1;
            foreach (var x in menuBase)
            {
                Console.WriteLine($"{counter}: {x.NameOfSushi}");
                counter++;
            }
        }

        public void PrintMenu()
        {
            foreach (var x in menuBase)
            {
                Console.WriteLine($"Name: {x.NameOfSushi}\n" +
                    $"Composition: {x.Composition}\n" +
                    $"Weight: {x.Weight}\n" +
                    $"Proteins: {x.Proteins}\n" +
                    $"Fats: {x.Fats}\n" +
                    $"Carbohydrates: {x.Carbohydrates}\n" +
                    $"Calories: {x.Calories}\n" +
                    $"Price: {x.Price}\n");
            }
        }

        public void PrintInfoAboutProduct(int productNumber)
        {
            int tempProductNumber = productNumber - 1;
            Console.WriteLine($"Name: {menuBase[tempProductNumber].NameOfSushi}\n" +
                $"Composition: {menuBase[tempProductNumber].Composition}\n" +
                $"Weight: {menuBase[tempProductNumber].Weight}\n" +
                $"Proteins: {menuBase[tempProductNumber].Proteins}\n" +
                $"Fats: {menuBase[tempProductNumber].Fats}\n" +
                $"Carbohydrates: {menuBase[tempProductNumber].Carbohydrates}\n" +
                $"Calories: {menuBase[tempProductNumber].Calories}\n" +
                $"Price: {menuBase[tempProductNumber].Price}");
        }

        public void GetInfoAboutProduct(Menu menu, int productNumber)
        {
            menu.PrintInfoAboutProduct(productNumber);
        }

        public Menu GetProductForOrder(Menu menu, int productNumber)
        {
            Menu tempMenu = new Menu();
            tempMenu = menuBase[productNumber - 1];
            return tempMenu;
        }
    }
}
