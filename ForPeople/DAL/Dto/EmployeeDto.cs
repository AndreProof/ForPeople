using Dapper.Contrib.Extensions;

namespace ForPeople.DAL.Dto
{
    /// <summary>
    /// Дто сотрудника.
    /// </summary>
    [Table(nameof(EmployeeDto))]
    public class EmployeeDto
    {
        /// <summary>
        /// Ключ.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Ключ компании.
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Дата трудоустройства.
        /// </summary>
        public string DateOfEmployment { get; set; }

        /// <summary>
        /// Должность.
        /// </summary>
        public int JobTitle { get; set; }

        /// <summary>
        /// Зарплата.
        /// </summary>
        public double Salary { get; set; }
    }
}