using Model;
using Logger;

namespace ViewModel.ViewModelMetadata
{
    public class VMNamespaceMetadata : TreeViewItem
    {
        private NamespaceMetadata namespaceMetadata;
        public override string Name => ToString();

        public VMNamespaceMetadata(NamespaceMetadata namespaceMetadata, Tracer tracer)
        {
            this.tracer = tracer;
            this.namespaceMetadata = namespaceMetadata;
            if (CanLoadChildren())
                Children.Add(null);
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            foreach (TypeMetadata typeMetadata in namespaceMetadata.Types.OrEmptyIfNull())
                Children.Add(new VMTypeMetadata(typeMetadata, tracer));
            FinishedLoadingChildren();
        }

        public override string ToString()
        {
            return "Namespace: " + namespaceMetadata.Name;
        }

        protected override bool CanLoadChildren()
        {
            return !namespaceMetadata.Types.IsNullOrEmpty();
        }
    }
}
