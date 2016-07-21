using System;
using System.Collections.Generic;
using ArrowConsoleMenu;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var setCountryChoice = new MenuChoices<string>("Set Country", new List<string> { "Canada", "USA" });
            var setLocalRegionChoice = new MenuChoices<string>("Set Local Region", () => GetLocalRegionsFor(setCountryChoice.SelectedItem));

            var menu = new Menu("My Menu");
            menu.AddChoices(setCountryChoice);
            menu.AddChoices(setLocalRegionChoice);
            menu.AddCommand("Print Current Region", () => { Console.WriteLine($"Current region = {setLocalRegionChoice.SelectedItem}"); }, wait: true);
            menu.Show();
        }

        static List<string> GetLocalRegionsFor(string country)
        {
            switch (country)
            {
                case "Canada":
                    return new List<string> { "BC", "AB" };    // etc..
                case "USA":
                    return new List<string> { "AB", "WA" };    // etc..
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
