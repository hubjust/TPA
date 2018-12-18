using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Diagnostics;
using MEF;

using Model;
using ViewModel.ViewModelMetadata;
using Logger;
using Serializers;

namespace ViewModel
{
    public class VMViewModel : BaseViewModel
    {
        private VMAssemblyMetadata assemblyMetadata;
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }

//        [ImportMany(typeof(IFileSupplier))]
        private IFileSelector fileSelector;
        private ITracer tracer;
        public ICommand OpenButton { get; }
        public ICommand SaveButton { get; }
        public string PathVariable { get; set; }

        private ISerializer serializer;

        public VMViewModel(IFileSelector selector, ISerializer serializer)
        {
            this.serializer = serializer;

            tracer = new FileTracer("GraphicalUserInterface.log", TraceLevel.Info);
            fileSelector = selector;

            tracer.TracerLog(TraceLevel.Verbose, "ViewModel initialization started");
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();

            OpenButton = new RelayCommand(Open);
            tracer.TracerLog(TraceLevel.Verbose, "ViewModel initialization finished");

            SaveButton = new RelayCommand(Save);
            tracer.TracerLog(TraceLevel.Verbose, "ViewModel serialization finished");
        }

        private void Open()
        {
            PathVariable = fileSelector.FileToOpen();
            OnPropertyChanged(nameof(PathVariable));

            try
            {
                tracer.TracerLog(TraceLevel.Info, "Open DLL button clicked.");
                if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
                {
                    tracer.TracerLog(TraceLevel.Info, "Loading from DLL.");
                    assemblyMetadata = new VMAssemblyMetadata(new AssemblyMetadata(Assembly.LoadFrom(PathVariable)), tracer);
                    LoadTreeView();
                }
                else if (PathVariable.Substring(PathVariable.Length - 4) == ".xml")
                {
                    tracer.TracerLog(TraceLevel.Info, "Loading from XML.");
                    assemblyMetadata = new VMAssemblyMetadata(serializer.Deserialize<AssemblyMetadata>(PathVariable), tracer);
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

        private void Save()
        {
            tracer.TracerLog(TraceLevel.Info, "Saving to XML...");
            serializer.Serialize(fileSelector.FileToSave(), assemblyMetadata.assemblyMetadata);
        }
    }
}
