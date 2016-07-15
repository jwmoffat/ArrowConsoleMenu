using System;
using System.Collections.Generic;

namespace ArrowConsoleMenu
{
    public class Menu
    {
        private readonly string _title;
        private readonly bool _exitOnSuccessfulSelect;
        const string SEPARATOR = "- - - - - - - - - - - - - - - - - - - - - - - - ";

        public List<MenuItem> MenuItems { get; set; }

        private int currItemIndex = 1;

        public Menu(string title) : this(title, exitOnSuccessfulSelect: false)
        {
            
        }

        protected Menu(string title, bool exitOnSuccessfulSelect)
        {
            _title = title;
            _exitOnSuccessfulSelect = exitOnSuccessfulSelect;
        }

        public void Show()
        {
            var invalidChoice = false;
            var arrowPressed = false;

            do
            {
                arrowPressed = false;
                Console.Clear();
                Console.WriteLine(SEPARATOR);
                Console.WriteLine($"{_title}");
                Console.WriteLine(SEPARATOR);
                for (var i = 1; i <= MenuItems.Count; i++)
                {
                    Console.WriteLine($"{(i == currItemIndex ? '>' : ' ')} {i}. {MenuItems[i - 1].Description}");
                }
                Console.WriteLine(SEPARATOR);
                Console.WriteLine();
                if (invalidChoice)
                {
                    Console.WriteLine(" *** Your previous choice was invalid *** ");
                    Console.WriteLine();
                }

                Console.Write("Please choose an option (x to exit): ");
                var inputKeyInfo = Console.ReadKey(false);
                var inputKeyVal = inputKeyInfo.KeyChar.ToString();
                Console.WriteLine();

                int menuChoice;

                if (inputKeyInfo.Key == ConsoleKey.X || inputKeyInfo.Key == ConsoleKey.Q || inputKeyInfo.Key == ConsoleKey.Backspace || inputKeyInfo.Key == ConsoleKey.LeftArrow) return;

                if (inputKeyInfo.Key == ConsoleKey.UpArrow)
                {
                    currItemIndex--;
                    if (currItemIndex < 1) currItemIndex = 1;
                    arrowPressed = true;
                }
                else if (inputKeyInfo.Key == ConsoleKey.DownArrow)
                {
                    currItemIndex++;
                    if (currItemIndex > MenuItems.Count) currItemIndex = MenuItems.Count;
                    arrowPressed = true;
                }
                else if (inputKeyInfo.Key == ConsoleKey.Enter || inputKeyInfo.Key == ConsoleKey.RightArrow)
                {
                    //Console.WriteLine("enter pressed");
                    inputKeyVal = currItemIndex.ToString();
                }

                if (arrowPressed)
                {
                    invalidChoice = false;
                }
                else if (!int.TryParse(inputKeyVal, out menuChoice) || menuChoice < 1 || menuChoice > MenuItems.Count)
                {
                    invalidChoice = true;
                }
                else
                {
                    MenuItems[menuChoice - 1].RunAction();
                    invalidChoice = false;
                }
            } while (invalidChoice || arrowPressed || !_exitOnSuccessfulSelect);
        }
    }
}