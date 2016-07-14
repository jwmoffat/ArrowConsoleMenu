using System;
using System.Collections.Generic;
using ArrowConsoleMenu;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var subMenu = new Menu("blah")
            {
                MenuItems = new List<MenuItem>
                {
                    new MenuItem("Test sub item 1", () => Console.WriteLine("This is a sub-menu item that does stuff")),
                    new MenuItem("Test sub item 2", () => Console.WriteLine("This is a sub-menu item 22222222222222222!!!!")),
                }
            };

            var baseMenu = new Menu("Base Menu")
            {
                MenuItems = new List<MenuItem>
                {
                    new MenuItem("Print Alphabet", PrintAlphabet),
                    new MenuItem("Show Today's Date", () => 
                    {
                        Console.Clear();
                        Console.WriteLine($"Today: {DateTime.Today:d}");
                    }),
                    new MenuItem("Sub Menu", () => subMenu.Show(), pauseAtEndOfAction: false),
                }
            };

            baseMenu.Show();
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

        /// <summary>
        /// 
        /// </summary>
        public class SubMenu : Menu
        {
            public SubMenu(string title) : base(title)
            {
                
            }
        }
    }
}
