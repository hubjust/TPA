using System.Collections.ObjectModel;
using System.Diagnostics;
using Logger;

namespace ViewModel
{
    public abstract class TreeViewItem
    {
        public abstract string Name { get; }
        public ObservableCollection<TreeViewItem> Children { get; } = new ObservableCollection<TreeViewItem>();
        private bool isExpanded;
        protected ITracer tracer;

        public TreeViewItem()
        { }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                if (isExpanded)
                    LoadChildren();
            }
        }

        protected virtual void LoadChildren()
        {
            Children.Clear();
            tracer.TracerLog(TraceLevel.Info, "Started loading " + Name + " childrens.");
        }

        protected void FinishedLoadingChildren()
        {
            tracer.TracerLog(TraceLevel.Info, "Children loaded.");
        }

        protected virtual bool CanLoadChildren()
        {
            return true;
        }

        public bool IsExpandable()
        {
            return !Children.IsNullOrEmpty();
        }
    }
}
