using Model;

namespace ViewModel.ViewModelMetadata
{
    public class VMNamespaceMetadata : TreeViewItem
    {
        private NamespaceMetadata namespaceMetadata;
        public override string Name => ToString();

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
            //if(namespaceMetadata != null)
            //{
            //    if(namespaceMetadata.Types != null)
            //    {
            //        try
            //        {
            //            foreach (TypeMetadata typeMetadata in namespaceMetadata.Types)
            //                Children.Add(new VMTypeMetadata(typeMetadata));
            //        } 
            //        catch (System.Exception e)
            //        {
            //            System.Console.WriteLine(e.StackTrace);
            //        }

            //    }

            //    FinishedLoadingChildren();
            //}
        }

        public override string ToString()
        {
            return "Namespace: " + this.namespaceMetadata.Name;
        }

        protected override bool CanLoadChildren()
        {
            return !namespaceMetadata.Types.IsNullOrEmpty();
        }
    }
}
