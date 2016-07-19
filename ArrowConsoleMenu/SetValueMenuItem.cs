using System;
using System.Collections.Generic;
using System.Linq;

namespace ArrowConsoleMenu
{
    public class SetValueMenuItem<T> : Menu, IMenuItem
    {
        private readonly List<T> _itemsToChooseFrom;
        public T SelectedEntry => _itemsToChooseFrom[base.currItemIndex - 1];


        public SetValueMenuItem(string displayText, List<T> itemsToChooseFrom) : base(displayText, true)
        {
            if (itemsToChooseFrom == null)
                throw new NullReferenceException("List of items for SetValueMenu item cannot be null");

            base.MenuItems = itemsToChooseFrom.Select(x => new MenuItem(x.ToString(), () => { }, false)).ToList<IMenuItem>();

            _itemsToChooseFrom = itemsToChooseFrom;
            _baseDescription = displayText;
        }

        private readonly string _baseDescription;
        public string Description => $"{_baseDescription} [{SelectedEntry}]";
        public void RunAction()
        {
            this.Show();
        }
    }
}