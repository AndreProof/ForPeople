using ForPeople.DAL.Dto;
using ForPeople.Types;
using System;
using System.Globalization;

namespace ForPeople.Model.Company
{
    /// <summary>
    /// Сотрудник.
    /// </summary>
    public class EmployeeModel : IModelWithDto<EmployeeDto>
    {
        #region Поля и свойства

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Полное имя.
        /// </summary>
        public string FullName => $"{this.SecondName} {this.Name} {this.Surname}";

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
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Дата трудоустройства.
        /// </summary>
        public DateTime DateOfEmployment { get; set; }

        /// <summary>
        /// Департамент.
        /// </summary>
        public DepartmentModel Department { get; set; }

        /// <summary>
        /// Должность.
        /// </summary>
        public JobTitle JobTitle { get; set; }

        /// <summary>
        /// Зарплата.
        /// </summary>
        public double Salary { get; set; }

        #endregion

        #region IModelWithDto

        /// <inheritdoc />
        public EmployeeDto GetDto()
        {
            return new EmployeeDto
            {
                Id = this.Id,
                DepartmentId = this.Department?.Id ?? 0,
                Name = this.Name,
                Surname = this.Surname,
                SecondName = this.SecondName,
                JobTitle = (int)this.JobTitle,
                DateOfBirth = this.DateOfBirth.ToString(CultureInfo.InvariantCulture),
                DateOfEmployment = this.DateOfEmployment.ToString(CultureInfo.InvariantCulture),
                Salary = this.Salary
            };
        }

        #endregion
    }
}
