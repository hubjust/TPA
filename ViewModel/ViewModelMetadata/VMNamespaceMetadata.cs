using Model;

namespace ViewModel.ViewModelMetadata
{
    public class VMNamespaceMetadata : TreeViewItem
    {
        private NamespaceMetadata namespaceMetadata;

        public VMNamespaceMetadata(NamespaceMetadata namespaceMetadata)
        {
            this.namespaceMetadata = namespaceMetadata;
            if (CanLoadChildren())
                Children.Add(null);
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            foreach (TypeMetadata typeMetadata in namespaceMetadata.Types.OrEmptyIfNull())
                Children.Add(new VMTypeMetadata(typeMetadata));
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
