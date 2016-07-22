using System;
using System.Collections.Generic;
using ArrowConsoleMenu;

namespace TestConsole
{
    static class NonStringClassExample
    {
        public static Menu GetExampleMenu()
        {
            // NB: Database class overrides ToString() so menu items are identifiable
            var databaseChoices = new MenuChoices<Database>("Set Selected Database", GetAllDatabases());
            
            var menu = new Menu("Non-String List Menu Example");
            menu.AddChoices(databaseChoices);
            menu.AddCommand("Run Report Against Selected Database", () => { RunReport(databaseChoices.SelectedItem); });     // passes database, not string
            return menu;
        }

        static void RunReport(Database currDatabase)
        {
            Console.WriteLine($"TODO: Implement code that runs against database {currDatabase.Name}");
        }

        static List<Database> GetAllDatabases()
        {
            return new List<Database>
            {
                new Database {Name = "Database 1"},
                new Database {Name = "Database 2"},
                new Database {Name = "Database 3"},  // etc
            };
        }

        internal class Database
        {
            // Add whatever else you might need.. this is just a basic example class
            public string Name { get; set; }
            
            // Needed to display nicely in console
            public override string ToString()
            {
                return Name;
            }
        }
    }
}