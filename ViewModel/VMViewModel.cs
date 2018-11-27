using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;

using Model;
using Model.Repository;
using ViewModel.ViewModelMetadata;
using Logger;
using Serializers;

using System.Threading.Tasks;

namespace ViewModel
{
    public class VMViewModel : BaseViewModel
    {
        private VMAssemblyMetadata assemblyMetadata;
        private AssemblyMetadata assemblyMet;
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }

        private IFileSelector fileSelector;
        private ITracer tracer;
        public ICommand OpenDLL { get; }
        public string pathVariable { get; set; }

        private TypeRepository typeRepository = new TypeRepository();
        public ICommand Click_Save { get; }

        public VMViewModel(IFileSelector selector)
        {
            tracer = new FileTracer("GraphicalUserInterface.log", TraceLevel.Info);
            fileSelector = selector;

            tracer.TracerLog(TraceLevel.Verbose, "ViewModel initialization started");
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();

            OpenDLL = new RelayCommand(Open);
            tracer.TracerLog(TraceLevel.Verbose, "ViewModel initialization finished");

            Click_Save = new RelayCommand(Save);
            tracer.TracerLog(TraceLevel.Verbose, "ViewModel serialization finished");
        }

        private void LoadXML()
        {
            pathVariable = fileSelector.FileToOpen();
            OnPropertyChanged(nameof(pathVariable));

            tracer.TracerLog(TraceLevel.Info, "XML loading.");
            if (pathVariable.Substring(pathVariable.Length - 4) == ".xml")
            {
                DataContext dataContext = new XmlDeserializer(pathVariable, tracer).Deserialize();
                assemblyMet = dataContext.AssemblyMetadata;
                assemblyMetadata = new VMAssemblyMetadata(assemblyMet, tracer);
                typeRepository.AssemblyMetadata = assemblyMet;

                foreach (var item in dataContext.Dictionary)
                {
                    if (!TypeRepository.storedTypes.ContainsKey(item.Key))
                        TypeRepository.storedTypes.Add(item.Key, item.Value);
                }

                LoadTreeView();
            }
            else
            {
                tracer.TracerLog(TraceLevel.Error, "XML error.");
            }
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
                    //assemblyMetadata = new AssemblyMetadata(Assembly.LoadFrom(pathVariable));
                    //tracer.Trace(TraceEventType.Stop, "dll loaded");
                    //assemblyMetadataViewModel = new AssemblyMetadataViewModel(assemblyMetadata);

                    assemblyMetadata = new VMAssemblyMetadata(new AssemblyMetadata(Assembly.LoadFrom(pathVariable)), tracer);
                    typeRepository.AssemblyMetadata = new AssemblyMetadata(Assembly.LoadFrom(pathVariable));
                    LoadTreeView();
                }
                else if (pathVariable.Contains(".xml"))
                {
                    assemblyMetadata = new VMAssemblyMetadata(new AssemblyMetadata(Assembly.LoadFrom(pathVariable)), tracer);
                    LoadXML();
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
            tracer.TracerLog(TraceLevel.Info, "File saving.");
            //fileSelector.FileToSave();
            SaveFileDialog dialog = new SaveFileDialog() { Filter = "XML File (*.xml)|*.xml" };
            dialog.ShowDialog();

            if (dialog.FileName.Length == 0)
            {
                tracer.TracerLog(TraceLevel.Info, "No file saving.");
            }
            else
            {
                pathVariable = dialog.FileName;
                DataContext data = new DataContext(typeRepository);
                Task.Run(() => new XmlSerializer(pathVariable, tracer).Serialize(data));
            }
            tracer.TracerLog(TraceLevel.Info, "No file saving.");
        }
    }
}
