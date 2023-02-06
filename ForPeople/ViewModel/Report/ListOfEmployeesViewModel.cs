using System;
using ForPeople.Extension;
using ForPeople.Model.Company;
using ForPeople.Model.Report;
using System.Collections.Generic;
using System.Linq;
using ForPeople.Types;
using Prism.Mvvm;

namespace ForPeople.ViewModel.Report
{
    /// <summary>
    /// Модель представления списка сотрудников.
    /// </summary>
    public class ListOfEmployeesViewModel : BindableBase
    {
        #region Поля и свойства

        /// <summary>
        /// Значение фильтра.
        /// </summary>
        private int filterValue = 18;

        /// <summary>
        /// Значение фильтра.
        /// </summary>
        public int FilterValue
        {
            get => this.filterValue;

            set
            {
                this.filterValue = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.Items));
            }
        }

        /// <summary>
        /// Доступные значения фильтра.
        /// </summary>
        public IEnumerable<int> FilterValues => this.filterType == FilterType.Age
            ? Enumerable.Range(18, 65 - 18)
            : Enumerable.Range(DateTime.Now.Year - 65, 65 - 18);

        /// <summary>
        /// Тип фильтра.
        /// </summary>
        private FilterType filterType;

        /// <summary>
        /// Тип фильтра.
        /// </summary>
        public FilterType FilterType
        {
            get => this.filterType;

            set
            {
                this.filterType = value;
                this.filterValue = this.FilterValues.FirstOrDefault();
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.FilterValues));
                this.RaisePropertyChanged(nameof(this.FilterValue));
                this.RaisePropertyChanged(nameof(this.Items));
            }
        }

        /// <summary>
        /// Доступные типы фильтра.
        /// </summary>
        public ICollection<FilterType> FilterTypes => Enum.GetValues<FilterType>();

        /// <summary>
        /// Компания.
        /// </summary>
        private string company;

        /// <summary>
        /// Компания.
        /// </summary>
        public string Company
        {
            get => this.company;

            set
            {
                this.company = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.Items));
            }
        }

        /// <summary>
        /// Доступные компании.
        /// </summary>
        public ICollection<string> Companies { get; } = new HashSet<string>();

        /// <summary>
        /// Опыт.
        /// </summary>
        private int experience;

        /// <summary>
        /// Опыт.
        /// </summary>
        public int Experience
        {
            get => this.experience;

            set
            {
                this.experience = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.Items));
            }
        }

        /// <summary>
        /// Доступные значения опыта.
        /// </summary>
        public ICollection<int> Experiences { get; } = new HashSet<int>();

        /// <summary>
        /// Записи.
        /// </summary>
        private ICollection<ListOfEmployeesRecordViewModel> items = new List<ListOfEmployeesRecordViewModel>();

        /// <summary>
        /// Отфильтрованные записи.
        /// </summary>
        public ICollection<ListOfEmployeesRecordViewModel> Items =>
            this.items.Where(x =>
            {
                if (this.FilterType == FilterType.Age && x.Age != this.filterValue)
                {
                    return false;
                }

                if (this.FilterType == FilterType.YearOfBirth && x.YearOfBirth != this.filterValue)
                {
                    return false;
                }

                return x.Company == this.Company && x.Experience == this.Experience;
            }).ToList();

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="companies">Модели компаний.</param>
        public ListOfEmployeesViewModel(IEnumerable<CompanyModel> companies)
        {
            foreach (var company in companies)
            {
                this.Companies.Add(company.Name);

                foreach (var department in company.Departments)
                {
                    foreach (var employee in department.Employers)
                    {
                        var recordModel = new ListOfEmployeesRecordModel
                        {
                            Company = company.Name,
                            Department = department.Name,
                            Employee = employee.FullName,
                            Age = employee.DateOfBirth.GetAge(),
                            YearOfBirth = employee.DateOfBirth.Year,
                            Experience = employee.DateOfEmployment.GetAge()
                        };

                        this.items.Add(new ListOfEmployeesRecordViewModel(recordModel));

                        if (!this.Experiences.Contains(recordModel.Experience))
                        {
                            this.Experiences.Add(recordModel.Experience);
                        }
                    }
                }
            }

            this.Company = this.Companies.FirstOrDefault();
            this.Experience = this.Experiences.FirstOrDefault();
        }

        #endregion
    }
}
