using System;
using System.Collections.Generic;
using ArrowConsoleMenu;

namespace TestConsole
{
    internal static class TooManyChoiceOptions
    {
        public static Menu GetExampleMenu()
        {
            var setNameChoice = new MenuChoices<string>("Set Placement", new List<string>
            {
                "First", "Second", "Third", "Fourth", "Fifth",
                "Sixth", "Seventh", "Eighth", "Ninth", "Tenth",
                "Eleventh", "Twelveth"
            });
            setNameChoice.PageSize = 5;

            var menu = new Menu("Too Many Choices Menu");
            menu.AddChoices(setNameChoice);
            menu.AddCommand("Print Placement", () => { Console.WriteLine($"You placed {setNameChoice.SelectedItem}"); });
            return menu;
        }
    }
}