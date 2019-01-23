using Microsoft.Win32;
using System.ComponentModel.Composition;

using Interfaces;

namespace GUI
{
    [Export(typeof(IFileSelector))]
    class GUIFileSelector : IFileSelector
    {
        public string FileToOpen(string filter = null)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            { Filter = "Dynamic Library File(*.dll)| *.dll| XML File(*.xml)| *.xml" };
            dialog.ShowDialog();
            return dialog.FileName;
        }

        public string FileToSave(string filter = null)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            { Filter = "XML File(*.xml)| *.xml" };

            dialog.ShowDialog();
            return dialog.FileName;
        }
    }
}
