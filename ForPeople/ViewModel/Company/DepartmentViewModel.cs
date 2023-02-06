using ForPeople.Model.Company;
using ForPeople.R;
using ForPeople.Services;
using ForPeople.Types;
using ForPeople.ViewModel.Tree;
using Prism.Commands;
using System.Collections.Generic;

namespace ForPeople.ViewModel.Company
{
    /// <summary>
    /// Модель представления подразделения.
    /// </summary>
    public class DepartmentViewModel : TreeNodeBase
    {
        #region Поля и свойства

        /// <summary>
        /// Модель подразделения.
        /// </summary>
        private readonly DepartmentModel model;

        /// <summary>
        /// Коллекция моделей представления подразделений.
        /// </summary>
        private readonly ICollection<ITreeNode> departmentViewModels;

        /// <summary>
        /// Название.
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
        /// Руководитель.
        /// </summary>
        private EmployeeViewModel manager;

        /// <summary>
        /// Руководитель.
        /// </summary>
        public EmployeeViewModel Manager
        {
            get => this.manager;

            set
            {
                if (this.manager != null)
                {
                    this.manager.JobTitle = JobTitle.None;
                }

                this.manager = value;

                if (this.manager != null)
                {
                    this.manager.JobTitle = JobTitle.Manager;
                }

                this.model.Manager = this.manager?.Context as EmployeeModel;

                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.Items));

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
        /// Обработчик команды добавления сотрудника.
        /// </summary>
        private void AddCommandHandler()
        {
            var newEmployee = new EmployeeModel
            {
                Name = CommonStrings.Name,
                SecondName = CommonStrings.SecondName,
                Surname = CommonStrings.Surname,
                Department = this.model
            };

            this.model.Employers.Add(newEmployee);
            var employeeViewModel = new EmployeeViewModel(newEmployee, departmentViewModels, this);
            this.Items.Add(employeeViewModel);

            this.Manager ??= employeeViewModel;
            this.RaisePropertyChanged(nameof(this.Items));

            MappingService.Add(newEmployee);
        }

        /// <summary>
        /// Обработчик команды удаления сотрудника.
        /// </summary>
        /// <param name="node">Узел.</param>
        private void RemoveCommandHandler(ITreeNode node)
        {
            if (node?.Context is EmployeeModel employeeModel)
            {
                this.model.Employers.Remove(employeeModel);
                if (this.Manager == node)
                {
                    this.Manager = null;
                }

                this.Items.Remove(node);
                this.RaisePropertyChanged(nameof(this.Items));

                MappingService.Delete(employeeModel);
            }
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

        /// <summary>
        /// Получить признак возможности удаления компании.
        /// </summary>
        /// <returns>Признак возможности удаления компании</returns>
        private bool CanRemoveCommandHandlerExecute(ITreeNode node) => node is EmployeeViewModel;

        /// <summary>
        /// Обработчик изменения выбранного элемента.
        /// </summary>
        /// <param name="node"></param>
        private void SelectedItemChangedCommandHandler()
        {
            this.RemoveCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Добавить сотрудника.
        /// </summary>
        /// <param name="employee">Сотрудник.</param>
        public void AddEmployee(EmployeeViewModel employee)
        {
            if (employee.Context is EmployeeModel employeeModel)
            {
                this.model.Employers.Add(employeeModel);
                this.Items.Add(employee);

                MappingService.Add(employeeModel);
            }
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="departmentViewModels">Модели представления департаментов.</param>
        public DepartmentViewModel(DepartmentModel model, ICollection<ITreeNode> departmentViewModels)
        {
            this.model = model;
            this.Context = model;
            this.departmentViewModels = departmentViewModels;

            if (this.model?.Employers != null)
            {
                foreach (var modelEmployer in this.model.Employers)
                {
                    var employeeViewModel = new EmployeeViewModel(modelEmployer, departmentViewModels, this);
                    this.Items.Add(employeeViewModel);

                    if (this.model.Manager == modelEmployer)
                    {
                        this.manager = employeeViewModel;
                    }
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
