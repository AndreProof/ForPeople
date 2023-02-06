using ForPeople.DAL.Dto;
using System.Collections.Generic;

namespace ForPeople.Model.Company
{
    public class DepartmentModel : IModelWithDto<DepartmentDto>
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
        /// Руководитель.
        /// </summary>
        public EmployeeModel Manager { get; set; }

        /// <summary>
        /// Департамены.
        /// </summary>
        public ICollection<EmployeeModel> Employers { get; } = new HashSet<EmployeeModel>();
        
        /// <summary>
        /// Компания.
        /// </summary>
        public CompanyModel Company { get; set; }

        #endregion

        #region IModelWithDto

        /// <inheritdoc />
        public DepartmentDto GetDto()
        {
            return new DepartmentDto
            {
                Id = this.Id,
                CompanyId = this.Company?.Id ?? 0,
                Name = this.Name,
                ManagerId = this.Manager?.Id ?? 0
            };
        }

        #endregion
    }
}
