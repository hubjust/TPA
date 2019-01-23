using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

using Model;
using ViewModel.ViewModelMetadata;
using Interfaces;
using MEF;
using DBCore.Model;

namespace ViewModel
{
    public class VMViewModel : BaseViewModel
    {
        private VMAssemblyMetadata assemblyMetadata;

        private Repository Repo;

        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }

        [ImportMany(typeof(IFileSelector))]
        private ImportSelector<IFileSelector> fileSelector;

        [ImportMany(typeof(ISerializer<AssemblyBase>))]
        private ImportSelector<ISerializer<AssemblyBase>> serializer;

        [ImportMany(typeof(ITracer))]
        private ImportSelector<ITracer> tracer;

        public ICommand LoadButton { get; }
        public ICommand OpenButton { get; }
        public ICommand SaveButton { get; }

        public string PathVariable { get; set; }

        public VMViewModel()
        {
            new Bootstrapper().ComposeApplication(this);
            tracer.GetImport().TracerLog(TraceLevel.Verbose, "ViewModel initialization started");

            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            Repo = new Repository();
            LoadButton = new RelayCommand(Load);
            OpenButton = new RelayCommand(Open);
            SaveButton = new RelayCommand(Save);

            tracer.GetImport().TracerLog(TraceLevel.Verbose, "ViewModel initialization finished");
        }

        private void Load()
        {
            PathVariable = fileSelector.GetImport().FileToOpen("Dynamic Library File(*.dll) | *.dll");
            if (PathVariable != null && !PathVariable.Equals(""))
            {
                Repo.CreateFromFile(PathVariable);
                assemblyMetadata = new VMAssemblyMetadata(Repo.Metadata);
                LoadTreeView();
            }
            OnPropertyChanged(nameof(PathVariable));
        }

        private async void Open()
        {
            await Task.Run(() =>
            {
                try
                {
                    tracer.GetImport().TracerLog(TraceLevel.Info, "Open button clicked.");
                    Repo.Load(fileSelector.GetImport());
                    assemblyMetadata = new VMAssemblyMetadata(Repo.Metadata);
                    
                }
                catch (Exception e)
                {
                    tracer.GetImport().TracerLog(TraceLevel.Error, "File must be selected in case of load: " + e.Message);
                }
            });
            LoadTreeView();
        }  

        private async void Save()
        {
            await Task.Run(() =>
            {
                try
                {
                    tracer.GetImport().TracerLog(TraceLevel.Info, "Saving assembly.");
                    Repo.Save(fileSelector.GetImport());
                    tracer.GetImport().TracerLog(TraceLevel.Info, "Saving succesfull.");
                }
                catch (Exception e)
                {
                    tracer.GetImport().TracerLog(TraceLevel.Error, "Error while saving: " + e.Message);
                }
            });
        }

        private void LoadTreeView()
        {
            tracer.GetImport().TracerLog(TraceLevel.Info, "TreeView loading...");
            HierarchicalAreas.Add(assemblyMetadata);
            tracer.GetImport().TracerLog(TraceLevel.Info, "TreeView loaded.");
        }
    }
}
