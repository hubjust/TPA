using Microsoft.Win32;

using ViewModel;

namespace GUI
{
    class FileSelector : IFileSelector
    {
        public string FileToOpen()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            { Filter = "Dynamic Library File(*.dll)| *.dll" };
            dialog.ShowDialog();
            return dialog.FileName;
        }

        public string FileToSave()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            { Filter = "Dynamic Library File(*.dll)| *.dll" };

            dialog.ShowDialog();
            return dialog.FileName;
        }
    }
}
