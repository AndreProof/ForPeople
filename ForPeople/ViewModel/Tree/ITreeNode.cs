using System.Collections.ObjectModel;

namespace ForPeople.ViewModel.Tree
{
    /// <summary>
    /// Интерфейс узла дерева.
    /// </summary>
    public interface ITreeNode
    {
        #region Поля и свойства

        /// <summary>
        /// Объект.
        /// </summary>
        object Context { get; }

        /// <summary>
        /// Текст узла.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Признак открытости узла.
        /// </summary>
        bool IsExpanded { get; set; }

        /// <summary>
        /// Признак выбранности узла.
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Дочерние узлы.
        /// </summary>
        ObservableCollection<ITreeNode> Items { get; }

        #endregion
    }
}
