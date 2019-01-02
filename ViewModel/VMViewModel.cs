using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

using Model;
using ViewModel.ViewModelMetadata;
using Logger;
using MEF;
using Serializers;
using DBCore.Model;


namespace ViewModel
{
    public class VMViewModel : BaseViewModel
    {
        private VMAssemblyMetadata assemblyMetadata;
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }

        [ImportMany(typeof(IFileSelector))]
        private ImportSelector<IFileSelector> fileSelector;

        [ImportMany(typeof(ISerializer<AssemblyBase>))]
        private ImportSelector<ISerializer<AssemblyBase>> serializer;

        [ImportMany(typeof(ITracer))]
        private ImportSelector<ITracer> tracer;

        public ICommand OpenButton { get; }
        public ICommand SaveButton { get; }

        public string PathVariable { get; set; }

        public VMViewModel()
        {
            new Bootstrapper().ComposeApplication(this);
            tracer.GetImport().TracerLog(TraceLevel.Verbose, "ViewModel initialization started");

            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            OpenButton = new RelayCommand(Open);
            SaveButton = new RelayCommand(Save);

            tracer.GetImport().TracerLog(TraceLevel.Verbose, "ViewModel initialization finished");
        }

        private void Open()
        {
            PathVariable = fileSelector.GetImport().FileToOpen();
            OnPropertyChanged(nameof(PathVariable));

            try
            {
                tracer.GetImport().TracerLog(TraceLevel.Info, "Open DLL button clicked.");
                if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
                {
                    tracer.GetImport().TracerLog(TraceLevel.Info, "Loading from DLL.");
                    assemblyMetadata = new VMAssemblyMetadata(new AssemblyMetadata(Assembly.LoadFrom(PathVariable)));
                    LoadTreeView();
                }
                else if (PathVariable.Substring(PathVariable.Length - 4) == ".xml")
                {
                    tracer.GetImport().TracerLog(TraceLevel.Info, "Loading from XML.");
                    AssemblyBase assemblyBase = serializer.GetImport().Deserialize(PathVariable);
                    assemblyMetadata = new VMAssemblyMetadata(new serializer.GetImport().Deserialize(PathVariable));
                    LoadTreeView();
                }
            }
            catch (System.SystemException)
            {
                tracer.GetImport().TracerLog(TraceLevel.Error, "DLL must be selected in case of load.");
            }
        }  

        private void LoadTreeView()
        {
            tracer.GetImport().TracerLog(TraceLevel.Info, "TreeView loading...");
            HierarchicalAreas.Add(assemblyMetadata);
            tracer.GetImport().TracerLog(TraceLevel.Info, "TreeView loaded.");
        }

        private void Save()
        {
            Task.Run(() =>
            {
                try
                {
                    tracer.GetImport().TracerLog(TraceLevel.Verbose, "Saving assembly to XML");
                    string fileName = fileSelector.GetImport().FileToSave();
                    if (fileName != "")
                    {
                        serializer.GetImport().Serialize(fileName,
                            Model.DataTransferGraph.AssemblyBase(assemblyMetadata.AssemblyMetadata));
                    }
                    else
                    {
                        tracer.GetImport().TracerLog(TraceLevel.Warning, "No file selected");
                    }
                }
                catch (Exception e)
                {
                    tracer.GetImport().TracerLog(TraceLevel.Error, "Serialization threw an exception: " + e.Message);
                }
            });
        }
    }
}
