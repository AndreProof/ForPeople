using ForPeople.Model.Company;
using ForPeople.R;
using ForPeople.Services;
using ForPeople.ViewModel.Tree;
using Prism.Commands;
using System;

namespace ForPeople.ViewModel.Company
{
    /// <summary>
    /// Модель представления компании.
    /// </summary>
    public class CompanyViewModel : TreeNodeBase
    {
        #region Поля и свойства

        /// <summary>
        /// Модель компании.
        /// </summary>
        private readonly CompanyModel model;

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name
        {
            get => this.model?.Name ?? string.Empty;

            set
            {
                if (this.model == null || this.model.Name == value)
                {
                    return;
                }

                this.model.Name = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.Text));

                MappingService.Update(this.model);
            }
        }

        /// <summary>
        /// Юридический адрес.
        /// </summary>
        public string LegalAddress
        {
            get => this.model?.LegalAddress ?? string.Empty;

            set
            {
                if (this.model == null || this.model.LegalAddress == value)
                {
                    return;
                }

                this.model.LegalAddress = value;
                this.RaisePropertyChanged();

                MappingService.Update(this.model);
            }
        }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime Date
        {
            get => this.model?.Date ?? DateTime.MinValue;

            set
            {
                if (this.model == null || this.model.Date == value)
                {
                    return;
                }

                this.model.Date = value;
                this.RaisePropertyChanged();

                MappingService.Update(this.model);
            }
        }

        #endregion

        #region Команды

        /// <summary>
        /// Команда добавить подразделение.
        /// </summary>
        public DelegateCommand AddCommand { get; }

        /// <summary>
        /// Команда удалить подразделение.
        /// </summary>
        public DelegateCommand<ITreeNode> RemoveCommand { get; }

        /// <summary>
        /// Команда двойного клика.
        /// </summary>
        public DelegateCommand<ITreeNode> DoubleClickCommand { get; }

        /// <summary>
        /// Команда смены выбранного элемента.
        /// </summary>
        public DelegateCommand SelectedItemChangedCommand { get; }

        #endregion

        #region ITreeNode

        /// <inheritdoc />
        public override string Text => this.Name;

        #endregion

        #region Методы

        /// <summary>
        /// Обработчик команды добавления подразделения.
        /// </summary>
        private void AddCommandHandler()
        {
            var newDepartment = new DepartmentModel
            {
                Name = CommonStrings.NewDepartment
            };

            this.model.Departments.Add(newDepartment);
            this.Items.Add(new DepartmentViewModel(newDepartment, this.Items));

            MappingService.Add(newDepartment);
        }

        /// <summary>
        /// Обработчик команды удаления подразделения.
        /// </summary>
        /// <param name="node">Узел.</param>
        private void RemoveCommandHandler(ITreeNode node)
        {
            if (node?.Context is DepartmentModel departmentModel)
            {
                this.model.Departments.Remove(departmentModel);
                this.Items.Remove(node);
                this.RaisePropertyChanged(nameof(this.Items));

                MappingService.Delete(departmentModel);
            }
        }

        /// <summary>
        /// Получить признак возможности удаления компании.
        /// </summary>
        /// <returns>Признак возможности удаления компании</returns>
        private bool CanRemoveCommandHandlerExecute(ITreeNode node) => node is DepartmentViewModel;

        /// <summary>
        /// Обработчик изменения выбранного элемента.
        /// </summary>
        /// <param name="node"></param>
        private void SelectedItemChangedCommandHandler()
        {
            this.RemoveCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Обработчик двойного клика.
        /// </summary>
        /// <param name="node">Узел.</param>
        private void DoubleClickCommandHandler(ITreeNode node)
        {
            if (node != null)
            {
                this.IsSelected = false;
                node.IsSelected = true;
            }
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="model">Модель компании.</param>
        public CompanyViewModel(CompanyModel model)
        {
            this.model = model;
            this.Context = model;

            if (this.model?.Departments != null)
            {
                foreach (var modelDepartment in this.model.Departments)
                {
                    this.Items.Add(new DepartmentViewModel(modelDepartment, this.Items));
                }
            }

            this.AddCommand = new DelegateCommand(this.AddCommandHandler);
            this.RemoveCommand = new DelegateCommand<ITreeNode>(this.RemoveCommandHandler, this.CanRemoveCommandHandlerExecute);
            this.DoubleClickCommand = new DelegateCommand<ITreeNode>(this.DoubleClickCommandHandler);
            this.SelectedItemChangedCommand = new DelegateCommand(this.SelectedItemChangedCommandHandler);
        }

        #endregion
    }
}
