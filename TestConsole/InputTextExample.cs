using System;
using ArrowConsoleMenu;

namespace TestConsole
{
    internal class InputTextExample
    {
        public static Menu GetExampleMenu()
        {
            var menuTextInputChoice = new MenuTextChoice("Email Address", "bob@localhost");

            var menu = new Menu("Set Text Menu");
            menu.AddTextInput(menuTextInputChoice);
            menu.AddCommand("Print current email address", () => { Console.WriteLine($"Email address = '{menuTextInputChoice.Value}'");});
            return menu;
        }
    }
}