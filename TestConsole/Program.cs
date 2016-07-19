using System;
using System.Collections.Generic;
using ArrowConsoleMenu;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu subMenu = CreateTestSubMenu();

            SetValueMenuItem<SearchProvider> setsearchProviderMenuItem = CreateSearchProviderMenuChooserItem();

            var baseMenu = new Menu("Base Menu")
            {
                MenuItems = new List<IMenuItem>
                {
                    new MenuItem("Print Alphabet", PrintAlphabet),
                    new MenuItem("Show Today's Date", () => { Console.Clear(); Console.WriteLine($"Today: {DateTime.Today:d}"); }),
                    new MenuItem("Sub Menu", () => subMenu.Show(), pauseAtEndOfAction: false),
                    setsearchProviderMenuItem,
                    new MenuItem("Query phone provider", () => { Console.WriteLine($"Do stuff on site {setsearchProviderMenuItem.SelectedEntry.Name} using URL {setsearchProviderMenuItem.SelectedEntry.Url} .." );}),
                }
            };

            baseMenu.Show();
        }

        private static Menu CreateTestSubMenu()
        {
            return new Menu("SubMenu Test")
            {
                MenuItems = new List<IMenuItem>
                {
                    new MenuItem("Test sub item 1", () => Console.WriteLine("Do some stuff for sub-item 1")),
                    new MenuItem("Test sub item 2", () => Console.WriteLine("Do stuff for sub-item 2!")),
                }
            };
        }

        private static SetValueMenuItem<SearchProvider> CreateSearchProviderMenuChooserItem()
        {
            var searchProviders = new List<SearchProvider>
            {
                new SearchProvider("google", "google.co.uk"),
                new SearchProvider("bing", "bing.com"),
                new SearchProvider("yahoo", "yahoo.ca")
            };

            return new SetValueMenuItem<SearchProvider>("Choose search provider", searchProviders);
        }

        private static void PrintAlphabet()
        {
            Console.Clear();
            Console.Write("English alphabet: ");
            for (var c = 'a'; c <= 'z'; c++)
            {
                Console.Write(c);
            }
        }
    }
}
