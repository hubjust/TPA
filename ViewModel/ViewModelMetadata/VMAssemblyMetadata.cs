using Model;
using Logger;

namespace ViewModel.ViewModelMetadata
{
    public class VMAssemblyMetadata : TreeViewItem
    {
        public AssemblyMetadata assemblyMetadata { get; }
        public override string Name => ToString();

        public VMAssemblyMetadata(AssemblyMetadata assembly, ITracer tracer)
        {
            base.tracer = tracer;
            assemblyMetadata = assembly;
            if (CanLoadChildren())
                Children.Add(null);
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            foreach (NamespaceMetadata namespaceMetadata in assemblyMetadata.Namespaces.OrEmptyIfNull())
                Children.Add(new VMNamespaceMetadata(namespaceMetadata, tracer));
            FinishedLoadingChildren();
        }

        public override string ToString()
        {
            return "Assembly: " + assemblyMetadata.Name;
        }

        protected override bool CanLoadChildren()
        {
            return !assemblyMetadata.Namespaces.IsNullOrEmpty();
        }
    }
}