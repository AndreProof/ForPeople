using System;
using ForPeople.Model.Company;
using ForPeople.R;
using ForPeople.Services;
using ForPeople.ViewModel.Company;
using ForPeople.ViewModel.Report;
using ForPeople.ViewModel.Tree;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ForPeople.ViewModel
{
    /// <summary>
    /// Модель представления основного окна.
    /// </summary>
    public class MainViewModel : BindableBase
    {
        #region Поля и свойства

        /// <summary>
        /// Компании.
        /// </summary>
        private ICollection<CompanyModel> companies;

        /// <summary>
        /// Компании.
        /// </summary>
        public ICollection<ITreeNode> Companies { get; private set; }

        /// <summary>
        /// Выбранный узел.
        /// </summary>
        public ITreeNode SelectedNode { get; set; }

        /// <summary>
        /// Видимость обновления.
        /// </summary>
        public Visibility LoadVisibility { get; set; }

        #endregion

        #region Команды

        /// <summary>
        /// Команда добавления компании.
        /// </summary>
        public DelegateCommand AddCompanyCommand { get; }

        /// <summary>
        /// Команда удаления компании.
        /// </summary>
        public DelegateCommand<ITreeNode> RemoveCompanyCommand { get; }

        /// <summary>
        /// Команда Ведомость по зарплате.
        /// </summary>
        public DelegateCommand PayrollCommand { get; }

        /// <summary>
        /// Команда Список сотрудников.
        /// </summary>
        public DelegateCommand ListOfEmployeesCommand { get; }

        /// <summary>
        /// Команда изменения выбранного элемента.
        /// </summary>
        public DelegateCommand<ITreeNode> SelectedItemChangedCommand { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Обработчик команды добавления компании.
        /// </summary>
        private void AddCompanyCommandHandler()
        {
            var newCompany = new CompanyModel
            {
                Date = DateTime.Now,
                LegalAddress = CommonStrings.NewLegalAdress,
                Name = CommonStrings.NewCompany
            };

            this.companies.Add(newCompany);
            this.Companies.Add(new CompanyViewModel(newCompany));

            MappingService.Add(newCompany);
        }

        /// <summary>
        /// Обработчик команды удаления компании.
        /// </summary>
        /// <param name="node">Узел.</param>
        private void RemoveCompanyCommandHandler(ITreeNode node)
        {
            if (node?.Context is CompanyModel companyModel)
            {
                this.companies.Remove(companyModel);
                this.Companies.Remove(node);
                this.RaisePropertyChanged(nameof(this.Companies));

                MappingService.Delete(companyModel);
            }
        }

        /// <summary>
        /// Получить признак возможности удаления компании.
        /// </summary>
        /// <returns>Признак возможности удаления компании</returns>
        private bool CanRemoveCompanyCommandExecute(ITreeNode node) => node is CompanyViewModel;

        /// <summary>
        /// Обработчик изменения выбранного элемента.
        /// </summary>
        /// <param name="node"></param>
        private void SelectedItemChangedCommandHandler(ITreeNode node)
        {
            this.SelectedNode = node;
            this.RaisePropertyChanged(nameof(this.SelectedNode));
            this.RemoveCompanyCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Обработчик открытия ведомости по зарплате.
        /// </summary>
        private void PayrollCommandHandler()
        {
            var payrollViewModel = new PayrollViewModel(this.companies);
            WindowService.OpenWindow(CommonStrings.Payroll, payrollViewModel);
        }

        /// <summary>
        /// Обработчик открытия списка сотрудников.
        /// </summary>
        private void ListOfEmployeesCommandHandler()
        {
            var listOfEmployeesViewModel = new ListOfEmployeesViewModel(this.companies);
            WindowService.OpenWindow(CommonStrings.ListOfEmployees, listOfEmployeesViewModel);
        }

        /// <summary>
        /// Загрузить базу данных.
        /// </summary>
        private async void LoadDataBase()
        {
            await Task.Run(() =>
            {
                this.companies = MappingService.Open();
            }).ContinueWith((t) =>
            {
                this.Companies = new ObservableCollection<ITreeNode>();

                foreach (var companyModel in this.companies)
                {
                    this.Companies.Add(new CompanyViewModel(companyModel));
                }

                this.RaisePropertyChanged(nameof(this.Companies));
                this.SelectedNode = this.Companies.FirstOrDefault();
                this.LoadVisibility = Visibility.Collapsed;
                this.RaisePropertyChanged(nameof(this.SelectedNode));
                this.RaisePropertyChanged(nameof(this.LoadVisibility));
            });
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор.
        /// </summary>
        public MainViewModel()
        {
            this.LoadDataBase();

            this.SelectedItemChangedCommand =
                new DelegateCommand<ITreeNode>(this.SelectedItemChangedCommandHandler);
            this.PayrollCommand = new DelegateCommand(this.PayrollCommandHandler);
            this.ListOfEmployeesCommand = new DelegateCommand(this.ListOfEmployeesCommandHandler);
            this.AddCompanyCommand = new DelegateCommand(this.AddCompanyCommandHandler);
            this.RemoveCompanyCommand = new DelegateCommand<ITreeNode>(this.RemoveCompanyCommandHandler, this.CanRemoveCompanyCommandExecute);
        }

        #endregion
    }
}
