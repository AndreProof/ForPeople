using ForPeople.Extension;
using ForPeople.Model.Company;
using ForPeople.Services;
using ForPeople.Types;
using ForPeople.ViewModel.Tree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ForPeople.ViewModel.Company
{
    /// <summary>
    /// Модель представления сотрудника.
    /// </summary>
    public class EmployeeViewModel : TreeNodeBase
    {
        #region Поля и свойства

        /// <summary>
        /// Модель сотрудника.
        /// </summary>
        private readonly EmployeeModel model;

        /// <summary>
        /// Коллекция моделей представления подразделений.
        /// </summary>
        private readonly ICollection<ITreeNode> departmentViewModels;

        /// <summary>
        /// Полное имя с местом работы.
        /// </summary>
        public string FullNameWithJob => $"{this.FullName} ({this.JobTitle.GetDisplayName()})";

        /// <summary>
        /// Полное имя.
        /// </summary>
        public string FullName => this.model?.FullName;

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
                this.RaisePropertyChanged(nameof(this.FullName));
                this.RaisePropertyChanged(nameof(this.FullNameWithJob));

                MappingService.Update(this.model);
            }
        }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string SecondName
        {
            get => this.model?.SecondName ?? string.Empty;

            set
            {
                if (this.model == null || this.model.SecondName == value)
                {
                    return;
                }

                this.model.SecondName = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.Text));
                this.RaisePropertyChanged(nameof(this.FullName));
                this.RaisePropertyChanged(nameof(this.FullNameWithJob));

                MappingService.Update(this.model);
            }
        }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string Surname
        {
            get => this.model?.Surname ?? string.Empty;

            set
            {
                if (this.model == null || this.model.Surname == value)
                {
                    return;
                }

                this.model.Surname = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.Text));
                this.RaisePropertyChanged(nameof(this.FullName));
                this.RaisePropertyChanged(nameof(this.FullNameWithJob));

                MappingService.Update(this.model);
            }
        }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime DateOfBirth
        {
            get => this.model?.DateOfBirth ?? DateTime.MinValue;

            set
            {
                if (this.model == null || this.model.DateOfBirth == value)
                {
                    return;
                }

                this.model.DateOfBirth = value;
                this.RaisePropertyChanged();

                MappingService.Update(this.model);
            }
        }

        /// <summary>
        /// Дата трудоустройства.
        /// </summary>
        public DateTime DateOfEmployment
        {
            get => this.model?.DateOfEmployment ?? DateTime.MinValue;

            set
            {
                if (this.model == null || this.model.DateOfEmployment == value)
                {
                    return;
                }

                this.model.DateOfEmployment = value;
                this.RaisePropertyChanged();

                MappingService.Update(this.model);
            }
        }

        /// <summary>
        /// Руководитель.
        /// </summary>
        private DepartmentViewModel department;

        /// <summary>
        /// Руководитель.
        /// </summary>
        public DepartmentViewModel Department
        {
            get => this.departmentViewModels?.OfType<DepartmentViewModel>()
                .FirstOrDefault(x => x.Context == this.model?.Department);

            set
            {
                if (this.model == null)
                {
                    return;
                }

                this.department?.RemoveCommand.Execute(this);

                this.department = value;

                this.department?.AddEmployee(this);

                this.model.Department = this.department?.Context as DepartmentModel;

                if (this.model.Department?.Manager != null)
                {
                    this.JobTitle = JobTitle.None;
                }

                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.Items));

                MappingService.Update(this.model);
            }
        }

        /// <summary>
        /// Подразделения.
        /// </summary>
        public ObservableCollection<DepartmentViewModel> Departments =>
            new ObservableCollection<DepartmentViewModel>(this.departmentViewModels.OfType<DepartmentViewModel>());

        /// <summary>
        /// Должность.
        /// </summary>
        public JobTitle JobTitle
        {
            get => this.model?.JobTitle ?? JobTitle.None;

            set
            {
                if (this.model == null || this.model.JobTitle == value)
                {
                    return;
                }

                if (this.model.JobTitle == JobTitle.Manager && this.department?.Manager != null)
                {
                    this.model.JobTitle = value;
                    this.department.Manager = null;
                }

                this.model.JobTitle = value;

                if (this.model.JobTitle == JobTitle.Manager && this.department != null)
                {
                    this.department.Manager = this;
                }

                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.FullNameWithJob));

                MappingService.Update(this.model);
            }
        }

        /// <summary>
        /// Варианты должностей.
        /// </summary>
        public IEnumerable<JobTitle> JobTitles => Enum.GetValues(typeof(JobTitle)).Cast<JobTitle>();

        /// <summary>
        /// Зарплата.
        /// </summary>
        public double Salary
        {
            get => this.model?.Salary ?? double.NaN;

            set
            {
                if (this.model == null || Math.Abs(this.model.Salary - value) < 1e-4)
                {
                    return;
                }

                this.model.Salary = value;
                this.RaisePropertyChanged();

                MappingService.Update(this.model);
            }
        }

        #endregion

        #region ITreeNode

        /// <inheritdoc />
        public override string Text => this.FullName;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="departmentViewModels">Существующие департамены.</param>
        /// <param name="parent">Родительский департамент.</param>
        public EmployeeViewModel(EmployeeModel model, ICollection<ITreeNode> departmentViewModels, DepartmentViewModel parent)
        {
            this.model = model;
            this.Context = model;
            this.departmentViewModels = departmentViewModels;
            this.department = parent;
        }

        #endregion
    }
}
