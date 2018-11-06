using System.Collections.ObjectModel;

namespace ViewModel
{
    public class ITreeViewItem
    {
        public ObservableCollection<ITreeViewItem> Children { get; set; }
        public ITreeViewItemBuilder Hierarchy { get; set; }
        public string Name { get; set; }
        private bool wasBuilt;
        private bool isExpanded;

        public ITreeViewItem()
        {
            Children = new ObservableCollection<ITreeViewItem>();
            Children.Add(null);
            this.wasBuilt = false;
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                if (wasBuilt)
                    return;
                Children.Clear();
                BuildMyself();
                wasBuilt = true;
            }
        }

        public virtual void BuildMyself()
        {
            Hierarchy.Build(Children);
        }
    }
}
