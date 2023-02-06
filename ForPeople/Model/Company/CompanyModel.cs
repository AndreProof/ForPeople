using ForPeople.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ForPeople.Model.Company
{
    /// <summary>
    /// Модель компании.
    /// </summary>
    public class CompanyModel : IModelWithDto<CompanyDto>
    {
        #region Поля и свойства

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Юридический адрес.
        /// </summary>
        public string LegalAddress { get; set; }

        /// <summary>
        /// Департамены.
        /// </summary>
        public ICollection<DepartmentModel> Departments { get; } = new HashSet<DepartmentModel>();

        #endregion

        #region IModelWithDto

        /// <inheritdoc />
        public CompanyDto GetDto()
        {
            return new CompanyDto
            {
                Id = this.Id,
                Name = this.Name,
                Date = this.Date.ToString(CultureInfo.InvariantCulture),
                LegalAddress = this.LegalAddress
            };
        }

        #endregion
    }
}
