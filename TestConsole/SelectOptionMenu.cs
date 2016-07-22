using System;
using System.Collections.Generic;
using ArrowConsoleMenu;

namespace TestConsole
{
    static class SelectOptionMenu
    {
        public static Menu GetExampleMenu()
        {
            var setNameChoice = new MenuChoices<string>("Set Name", new List<string> { "Adam", "Bob", "Sally" });
            var menu = new Menu("Select Options Menu");
            menu.AddChoices(setNameChoice);
            menu.AddCommand("Print Selected Name", () => { Console.WriteLine($"Current name = {setNameChoice.SelectedItem}"); });
            return menu;
        }
    }
}