using System;
using System.Collections.Generic;
using System.Linq;

namespace ArrowConsoleMenu
{
    public class MenuChoices<T> : Menu, IMenuItem
    {
        private readonly Func<List<T>> _listFunction;
        public T SelectedItem => _listFunction()[CurrItemIndex - 1];

        private readonly string _baseDescription;
        public string Description => $"{_baseDescription} [{SelectedItem}]";

        public MenuChoices(string description, List<T> list) : base(description, exitOnSuccessfulSelect: true)
        {
            _baseDescription = description;
            _listFunction = () => list;
            MenuItems = list.Select(x => new MenuItem(x.ToString(), () => { }, pauseAtEndOfAction: false)).ToList<IMenuItem>();
        }

        public MenuChoices(string description, Func<List<T>> listFunction) : base(description, exitOnSuccessfulSelect: true)
        {
            _baseDescription = description;
            _listFunction = listFunction;
            MenuItemsFunc = () => { return listFunction().Select(x => new MenuItem(x.ToString(), () => { }, pauseAtEndOfAction: false)).ToList<IMenuItem>(); };
        }
        
        public void RunAction()
        {
            this.Show();
        }
    }
}