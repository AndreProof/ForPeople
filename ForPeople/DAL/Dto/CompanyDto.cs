using Dapper.Contrib.Extensions;

namespace ForPeople.DAL.Dto
{
    /// <summary>
    /// Дто компании.
    /// </summary>
    [Table(nameof(CompanyDto))]
    public class CompanyDto
    {
        /// <summary>
        /// Ключ.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Юридический адрес.
        /// </summary>
        public string LegalAddress { get; set; }
    }
}
