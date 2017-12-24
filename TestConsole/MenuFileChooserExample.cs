using ArrowConsoleMenu;
using System;
using System.IO;

namespace TestConsole
{
    static class MenuFileChooserExample
    {
        public static Menu GetExampleMenu()
        {
            var directoryMenu = new MenuFileChooser("Select configuration file", new DirectoryInfo(@"c:\tmp"), "*.txt");
            
            var menu = new Menu("Menu File Selection Example");
            menu.AddFileMenu(directoryMenu);
            menu.AddCommand("Show selected file", () => { Console.WriteLine(directoryMenu.SelectedFile?.FullName); });
            return menu; 
        }
    }
}
