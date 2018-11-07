﻿using System.Windows.Forms;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reflection;

using Model;
using ViewModel.ViewModelMetadata;
using Tracer;
using System.Diagnostics;

namespace ViewModel
{
    public class VMViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand Click_Open { get; }
        public string pathVariable { get; set; }

        private static ITracer tracer = new FileTracer("GraphicalUserInterface.log", TraceLevel.Info);

        private AssemblyMetadata assemblyMetadata;
        private ViewModelAssemblyMetadata viewModelAssemblyMetadata;

        public ObservableCollection<ITreeViewItem> HierarchicalAreas { get; set; }

        public VMViewModel()
        {
            tracer.TracerLog(TraceLevel.Verbose, "Initialization started");
            HierarchicalAreas = new ObservableCollection<ITreeViewItem>();
            Click_Open = new Command(Open);
        }

        public virtual void RaisePropertyChanged(string propertyNam)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyNam));
        }

        private void Open()
        {
            tracer.TracerLog(TraceLevel.Info, "Opening");
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Dynamic Library File(*.dll) | *.dll|"
                       + "All files (*.*)| *.*"
            };

            fileDialog.ShowDialog();

            if (fileDialog.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
                tracer.TracerLog(TraceLevel.Warning, "No file selected");
            }
            
            else
            {
                pathVariable = fileDialog.FileName;
                RaisePropertyChanged("pathVariable");
                LoadDLL();
            }
        }

        private void LoadDLL()
        {
            if (pathVariable.Substring(pathVariable.Length - 4) == ".dll")
            {
                assemblyMetadata = new AssemblyMetadata(Assembly.LoadFrom(pathVariable));
                viewModelAssemblyMetadata = new ViewModelAssemblyMetadata(assemblyMetadata);
                LoadTreeView();
            }

        }

        private void LoadTreeView()
        {
            tracer.TracerLog(TraceLevel.Info, "TreeView");

            HierarchicalAreas.Add(new ITreeViewItem
            {
                Name = viewModelAssemblyMetadata.Name,
                Hierarchy = viewModelAssemblyMetadata
            });
        }
    }
}
