using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Diagnostics;

using Model;
using ViewModel.ViewModelMetadata;
using Logger;

namespace ViewModel
{
    public class VMViewModel : BaseViewModel
    {
        private VMAssemblyMetadata assemblyMetadata;
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }

        private IFileSelector fileSelector;
        private ITracer tracer;
        public ICommand OpenDLL { get; }
        public string pathVariable { get; set; }

        public VMViewModel(IFileSelector selector)
        {
            tracer = new FileTracer("GraphicalUserInterface.log", TraceLevel.Info);
            fileSelector = selector;

            tracer.TracerLog(TraceLevel.Verbose, "ViewModel initialization started");
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();

            OpenDLL = new RelayCommand(Open);
            tracer.TracerLog(TraceLevel.Verbose, "ViewModel initialization finished");
        }

        private void Open()
        {
            pathVariable = fileSelector.FileToOpen();
            OnPropertyChanged(nameof(pathVariable));

            try
            {
                tracer.TracerLog(TraceLevel.Info, "Open DLL button clicked.");
                if (pathVariable.Substring(pathVariable.Length - 4) == ".dll")
                {
                    assemblyMetadata = new VMAssemblyMetadata(new AssemblyMetadata(Assembly.LoadFrom(pathVariable)), tracer);
                    LoadTreeView();
                }
            }
            catch (System.SystemException)
            {
                tracer.TracerLog(TraceLevel.Error, "DLL must be selected in case of load.");
            }
        }

        private void LoadTreeView()
        {
            tracer.TracerLog(TraceLevel.Info, "TreeView loading...");
            HierarchicalAreas.Add(assemblyMetadata);
            tracer.TracerLog(TraceLevel.Info, "TreeView loaded.");
        }
    }
}
