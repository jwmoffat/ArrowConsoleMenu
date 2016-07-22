using System;

namespace ArrowConsoleMenu
{
    public class MenuTextChoice : IMenuItem
    {
        private readonly string _variableNameDescription;
        public string Value { get; private set; }

        public MenuTextChoice(string variableNameDescription, string defaultValue)
        {
            _variableNameDescription = variableNameDescription;
            Value = defaultValue;
        }

        public string Description => $"{_variableNameDescription} [{Value}]";
        public void RunAction()
        {
            Console.Clear();
            Console.Write($"{Description}: ");
            var newEntry = Console.ReadLine();
            if (!string.IsNullOrEmpty(newEntry?.Trim()))
                Value = newEntry.Trim();
        }
    }
}