﻿using System.Collections.ObjectModel;

namespace ViewModel
{
    public abstract class TreeViewItem
    {
        public abstract string Name { get; }
        public ObservableCollection<TreeViewItem> Children { get; } = new ObservableCollection<TreeViewItem>();
        private bool isExpanded;

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
        }

        protected void FinishedLoadingChildren()
        {
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
