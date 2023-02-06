using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace ForPeople.ViewModel.Tree
{
    /// <summary>
    /// Базовый класс для узла дерева.
    /// </summary>
    public class TreeNodeBase : BindableBase, ITreeNode
    {
        #region ITreeNode

        /// <inheritdoc />
        public object Context { get; protected set; }

        /// <inheritdoc />
        public virtual string Text { get; }

        /// <inheritdoc />
        public bool IsExpanded { get; set; } = true;

        private bool isSelected = false;

        /// <inheritdoc />
        public bool IsSelected
        {
            get => this.isSelected;

            set
            {
                if (this.isSelected == value)
                {
                    return;
                }

                this.isSelected = value;
                this.RaisePropertyChanged();
            }
        }

        /// <inheritdoc />
        public ObservableCollection<ITreeNode> Items { get; } = new ObservableCollection<ITreeNode>();

        #endregion
    }
}
