using Dapper.Contrib.Extensions;

namespace ForPeople.DAL.Dto
{
    /// <summary>
    /// Дто подразделения.
    /// </summary>
    [Table(nameof(DepartmentDto))]
    public class DepartmentDto
    {
        /// <summary>
        /// Ключ.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Ключ компании.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Ключ руководителя.
        /// </summary>
        public int ManagerId { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }
    }
}
