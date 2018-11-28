using Microsoft.Win32;

using ViewModel;

namespace GUI
{
    class GUIFileSelector : IFileSelector
    {
        public string FileToOpen()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            { Filter = "Dynamic Library File(*.dll)| *.dll| XML File(*.xml)| *.xml" };
            dialog.ShowDialog();
            return dialog.FileName;
        }

        public string FileToSave()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            { Filter = "XML File(*.xml)| *.xml" };

            dialog.ShowDialog();
            return dialog.FileName;
        }
    }
}
