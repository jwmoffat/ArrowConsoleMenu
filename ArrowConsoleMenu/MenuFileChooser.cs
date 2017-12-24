using System.IO;
using System.Linq;

namespace ArrowConsoleMenu
{
    public class MenuFileChooser : IMenuItem
    {
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        // IMenuItem specific
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        public string Description
        {
            get
            {
                if (SelectedFile == null) return description;

                return $"{description} [{SelectedFile.FullName}]";
            }
        }

        public void RunAction()
        {
            // Could put initialization of fileEntries here, which prevents picking an initial file. Though that may be useful.
            fileEntries.Show();
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        // MenuFileChooser specific
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        
        private readonly string description;
        private readonly DirectoryInfo baseDirectory;
        private readonly string fileFilter;
        private MenuChoices<FileInfo> fileEntries;
        
        public FileInfo SelectedFile => fileEntries?.SelectedItem;

        public MenuFileChooser(string description, DirectoryInfo baseDirectory, string fileFilter = "*")
        {
            this.description = description;
            this.baseDirectory = baseDirectory;
            this.fileFilter = fileFilter;
            fileEntries = new MenuChoices<FileInfo>("Choose a file", baseDirectory.GetFiles(fileFilter, SearchOption.TopDirectoryOnly).ToList());
        }
    }
}
