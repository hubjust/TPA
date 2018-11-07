using System.Windows.Forms;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;

using Model;
using ViewModel.ViewModelMetadata;

namespace ViewModel
{
    public class VMViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand Click_Open { get; }
        public string PathVariable { get; set; }

        private AssemblyMetadata assemblyMetadata;
        private ViewModelAssemblyMetadata viewModelAssemblyMetadata;

        public ObservableCollection<ITreeViewItem> HierarchicalAreas { get; set; }


        public VMViewModel()
        {
            HierarchicalAreas = new ObservableCollection<ITreeViewItem>();
            Click_Open = new Command(Open);
        }

        private void Open()
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Dynamic Library File(*.dll) | *.dll|"
                       + "All files (*.*)| *.*"
            };

            fileDialog.ShowDialog();

            if (fileDialog.FileName.Length == 0)     
                MessageBox.Show("No files selected");
            
            else
            {
                PathVariable = fileDialog.FileName;
                RaisePropertyChanged("PathVariable");
            }
        }

        public virtual void RaisePropertyChanged(string propertyNam)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyNam));
        }
    }
}
