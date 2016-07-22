using System;
using ArrowConsoleMenu;

namespace TestConsole
{
    static class SimpleMenu
    {
        public static Menu GetExampleMenu()
        {
            var menu = new Menu("Basic Menu");
            menu.AddCommand("Print 'Hello World'", () => { Console.WriteLine("Hello World"); });
            menu.AddCommand("Show current date",   () => { Console.WriteLine(DateTime.Now); });
            menu.AddCommand("Send me an email",    () => { Emailer.SendEmail(); }, wait: false);      // wait = true by default
            return menu;
        }
        
        private static class Emailer
        {
            public static void SendEmail() { /* TODO */ }
        }
    }
}