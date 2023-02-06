namespace ForPeople.Model.Report
{
    /// <summary>
    /// Модель записи для списка сотрудников.
    /// </summary>
    public class ListOfEmployeesRecordModel
    {
        /// <summary>
        /// Компания.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Подразделение.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Работник.
        /// </summary>
        public string Employee { get; set; }

        /// <summary>
        /// Опты.
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Возраст.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Год рождения.
        /// </summary>
        public int YearOfBirth { get; set; }
    }
}
