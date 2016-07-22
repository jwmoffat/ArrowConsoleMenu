using System;
using ArrowConsoleMenu;

namespace TestConsole
{
    static class SimpleMenu
    {
        public static Menu GetExampleMenu()
        {
            var menu = new Menu("Basic Menu");
            menu.AddCommand("Print 'Hello World'", () => { Console.WriteLine("Hello World"); }, wait: true);
            menu.AddCommand("Show current date",   () => { Console.WriteLine(DateTime.Now); } , wait: true);
            menu.AddCommand("Send me an email",    () => { Emailer.SendEmail(); });                            // wait = false by default
            return menu;
        }
        
        private static class Emailer
        {
            public static void SendEmail() { /* TODO */ }
        }
    }
}