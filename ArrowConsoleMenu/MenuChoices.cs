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

        public new string Description
        {
            get
            {
                if (!previousList?.SequenceEqual(_listFunction()) ?? false)
                {
                    CurrItemIndex = 1;
                    FirstItemIndexOnPage = 1;
                }

                return $"{_baseDescription} [{SelectedItem}]";
            }
        }
        
        public MenuChoices(string description, List<T> list) : base(description, exitOnSuccessfulSelect: true)
        {
            _baseDescription = description;
            _listFunction = () => list;
            MenuItems = list.Select(x => new MenuItem(x.ToString(), () => { }, pauseAtEndOfAction: false)).ToList<IMenuItem>();
        }

        private List<T> previousList = null;

        public MenuChoices(string description, Func<List<T>> listFunction) : base(description, exitOnSuccessfulSelect: true)
        {
            _baseDescription = description;
            _listFunction = listFunction;

            MenuItemsFunc = GenerateItems;
        }
        
        public List<IMenuItem> GenerateItems()
        {
            var newList = _listFunction();

            if (!previousList?.SequenceEqual(newList) ?? false)
                base.CurrItemIndex = 1;
            else
            {
            }

            previousList = newList;

            return newList.Select(x => new MenuItem(x.ToString(), () => { }, pauseAtEndOfAction: false)).ToList<IMenuItem>();
        }
    }
}