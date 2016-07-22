using ArrowConsoleMenu;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu("Choose a menu example");
            menu.AddSubMenu(SimpleMenu.GetExampleMenu());
            menu.AddSubMenu(SelectOptionMenu.GetExampleMenu());
            menu.AddSubMenu(DependentChoiceOptions.GetExampleMenu());
            menu.AddSubMenu(NonStringClassExample.GetExampleMenu());
            menu.AddSubMenu(TooManyChoiceOptions.GetExampleMenu());
            menu.Show();
        }
    }
}
