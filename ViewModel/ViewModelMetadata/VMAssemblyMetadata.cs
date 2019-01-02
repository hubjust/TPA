using System.Collections.Generic;
using System.Linq;

using Model;
using DBCore.Model;

namespace ViewModel.ViewModelMetadata
{
    public class VMAssemblyMetadata : TreeViewItem
    {
        public IEnumerable<NamespaceMetadata> Namespaces;

        public AssemblyMetadata AssemblyMetadata { get; }

        public VMAssemblyMetadata(AssemblyMetadata assembly)
        {
            AssemblyMetadata = assembly;
            if (CanLoadChildren())
                Children.Add(null);
        }

        public VMAssemblyMetadata(AssemblyBase baseAssembly)
        {
            AssemblyMetadata = new AssemblyMetadata(baseAssembly);
            Name = baseAssembly.Name;
            Namespaces = baseAssembly.Namespaces?.Select(ns => new NamespaceMetadata(ns));
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            foreach (NamespaceMetadata namespaceMetadata in AssemblyMetadata.Namespaces.OrEmptyIfNull())
                Children.Add(new VMNamespaceMetadata(namespaceMetadata));
            FinishedLoadingChildren();
        }

        public override string ToString()
        {
            return "Assembly: " + AssemblyMetadata.Name;
        }

        protected override bool CanLoadChildren()
        {
            return !AssemblyMetadata.Namespaces.IsNullOrEmpty();
        }
    }
}