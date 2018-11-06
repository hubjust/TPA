using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.ViewModelMetadata
{
    class ViewModelAssemblyMetadata : ViewModelMetadata, ITreeViewItemBuilder
    {
        public List<NamespaceMetadata> Namespaces { get; set; }

        public ViewModelAssemblyMetadata(AssemblyMetadata assemblyMetadata) : base(assemblyMetadata.Name)
        {
            Namespaces = assemblyMetadata.Namespaces;
        }

        public void Build(ObservableCollection<ITreeViewItem> children)
        {
            if (Namespaces != null)
                Add(Namespaces, children);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
