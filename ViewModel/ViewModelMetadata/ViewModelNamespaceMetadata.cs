using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.ViewModelMetadata
{
    public class ViewModelNamespaceMetadata : ViewModelMetadata, ITreeViewItemBuilder
    {
        public List<TypeMetadata> Types { get; set; }

        public ViewModelNamespaceMetadata(NamespaceMetadata namespaceMetadata) : base(namespaceMetadata.Name)
        {
            Types = namespaceMetadata.Types;
        }

        public void Build(ObservableCollection<ITreeViewItem> children)
        {
            if (Types != null)
                Add(Types, children);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
