using ForPeople.Model.Report;

namespace ForPeople.ViewModel.Report
{
    /// <summary>
    /// Модель представления записи списка сотрудников.
    /// </summary>
    public class ListOfEmployeesRecordViewModel
    {
        #region Поля и свойства

        /// <summary>
        /// Модель записи.
        /// </summary>
        private readonly ListOfEmployeesRecordModel model;

        /// <summary>
        /// Компания.
        /// </summary>
        public string Company => this.model?.Company;

        /// <summary>
        /// Подразделение.
        /// </summary>
        public string Department => this.model?.Department;

        /// <summary>
        /// Сотрудник.
        /// </summary>
        public string Employee => this.model?.Employee;

        /// <summary>
        /// Опыт.
        /// </summary>
        public int Experience => this.model?.Experience ?? -1;

        /// <summary>
        /// Возраст.
        /// </summary>
        public int Age => this.model?.Age ?? -1;

        /// <summary>
        /// Год рождения.
        /// </summary>
        public int YearOfBirth => this.model?.YearOfBirth ?? -1;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="model">Модель записи.</param>
        public ListOfEmployeesRecordViewModel(ListOfEmployeesRecordModel model)
        {
            this.model = model;
        }

        #endregion
    }
}
