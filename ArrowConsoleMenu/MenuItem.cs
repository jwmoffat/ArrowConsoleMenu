using System;

namespace ArrowConsoleMenu
{
    public class MenuItem
    {
        private readonly Func<string> _descriptionFunc;

        private readonly string _description;
        public string Description => _description ?? _descriptionFunc();
        private readonly Action _actionToRun;
        private readonly bool _pauseAtEndOfAction;

        public MenuItem(Func<string> descriptionFunc, Action actionToRun, bool pauseAtEndOfAction = true)
        {
            _description = null;
            _descriptionFunc = descriptionFunc;

            _pauseAtEndOfAction = pauseAtEndOfAction;
            _actionToRun = actionToRun;
        }

        public MenuItem(string description, Action actionToRun, bool pauseAtEndOfAction = true)
        {
            _pauseAtEndOfAction = pauseAtEndOfAction;
            _description = description;
            _actionToRun = actionToRun;
        }

        public void RunAction()
        {
            try
            {
                _actionToRun();
                if (!_pauseAtEndOfAction) return;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"An unexpected error occurred while executing '{Description}'\r\nDetails: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine($"Action '{Description}' has completed.  Press ENTER to continue.");
            Console.ReadLine();
        }

    }
}
