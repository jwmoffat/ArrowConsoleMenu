﻿using System;
using System.Collections.Generic;

namespace ArrowConsoleMenu
{
    public class Menu : IMenuItem
    {
        const string Separator = "- - - - - - - - - - - - - - - - - - - - - - - - ";

        private readonly string _title;
        protected bool ExitOnSuccessfulSelect;
        protected Func<List<IMenuItem>> MenuItemsFunc = null;
        protected List<IMenuItem> MenuItems = new List<IMenuItem>();
        protected int CurrItemIndex = 1;

        public int PageSize { get; set; } = 10;

        public Menu(string title) : this(title, exitOnSuccessfulSelect: false) { }

        protected Menu(string title, bool exitOnSuccessfulSelect)
        {
            _title = title;
            ExitOnSuccessfulSelect = exitOnSuccessfulSelect;
        }

        public void Show()
        {
            var invalidChoice = false;
            var navigationButtonPressed = false;

            var firstItemIndexOnPage = 1;

            do
            {
                var menuItems = MenuItemsFunc == null ? MenuItems : MenuItemsFunc();

                navigationButtonPressed = false;
                Console.Clear();
                Console.WriteLine(Separator);
                Console.WriteLine($"{_title}");
                Console.WriteLine(Separator);
                for (var i = firstItemIndexOnPage; i <= menuItems.Count && i < (firstItemIndexOnPage + PageSize); i++)
                {
                    Console.WriteLine($"{(i == CurrItemIndex ? '>' : ' ')} {i}. {menuItems[i - 1].Description}");
                }
                Console.WriteLine(Separator);
                Console.WriteLine();
                if (invalidChoice)
                {
                    Console.WriteLine(" *** Your previous choice was invalid *** ");
                    Console.WriteLine();
                }

                Console.Write("Please choose an option using arrow keys.  Right to select item, Left to exit.  ");
                var inputKeyInfo = Console.ReadKey(false);
                var inputKeyVal = inputKeyInfo.KeyChar.ToString();
                Console.WriteLine();

                int menuChoice;

                switch (inputKeyInfo.Key)
                {
                    case ConsoleKey.X:
                    case ConsoleKey.Q:
                    case ConsoleKey.Backspace:
                    case ConsoleKey.LeftArrow:
                        return;
                    case ConsoleKey.UpArrow:
                        CurrItemIndex--;
                        if (CurrItemIndex < 1) CurrItemIndex = 1;
                        if (CurrItemIndex < firstItemIndexOnPage) firstItemIndexOnPage = CurrItemIndex;
                        navigationButtonPressed = true;
                        break;
                    case ConsoleKey.DownArrow:
                        CurrItemIndex++;
                        if (CurrItemIndex > menuItems.Count) CurrItemIndex = menuItems.Count;
                        if (CurrItemIndex >= firstItemIndexOnPage + PageSize && CurrItemIndex <= menuItems.Count)
                        {
                            // want to bump the first item on the page
                            firstItemIndexOnPage++;
                        }
                        navigationButtonPressed = true;
                        break;
                    case ConsoleKey.PageDown:
                        {
                            var wantToJumpTo = firstItemIndexOnPage + PageSize;
                            var maxFirstItem = menuItems.Count - PageSize + 1; // 12 items, page size = 10, 1-10, 2-11, 3-12
                            if (maxFirstItem < 1) maxFirstItem = 1;
                            if (wantToJumpTo > maxFirstItem) wantToJumpTo = maxFirstItem;
                            firstItemIndexOnPage = wantToJumpTo;
                            CurrItemIndex = firstItemIndexOnPage;
                        }
                        navigationButtonPressed = true;
                        break;
                    case ConsoleKey.PageUp:
                        firstItemIndexOnPage = firstItemIndexOnPage - PageSize;
                        if (firstItemIndexOnPage < 1) firstItemIndexOnPage = 1;
                        CurrItemIndex = firstItemIndexOnPage;
                        navigationButtonPressed = true;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.RightArrow:
                        //Console.WriteLine("enter pressed");
                        inputKeyVal = CurrItemIndex.ToString();
                        break;
                }

                if (navigationButtonPressed)
                {
                    invalidChoice = false;
                }
                else if (!int.TryParse(inputKeyVal, out menuChoice) || menuChoice < 1 || menuChoice > menuItems.Count)
                {
                    invalidChoice = true;
                }
                else
                {
                    menuItems[menuChoice - 1].RunAction();
                    invalidChoice = false;
                }
            } while (invalidChoice || navigationButtonPressed || !ExitOnSuccessfulSelect);
        }

        public void AddChoices<T>(MenuChoices<T> menuChoices)
        {
            MenuItems.Add(menuChoices);
        }

        public void AddCommand(string commandName, Action action, bool wait = true)
        {
            MenuItems.Add(new MenuItem(commandName, action, pauseAtEndOfAction: wait));
        }

        public void AddSubMenu(Menu subMenu)
        {
            MenuItems.Add(subMenu);
        }

        public string Description => _title;
        public void RunAction()
        {
            Show();
        }
    }
}