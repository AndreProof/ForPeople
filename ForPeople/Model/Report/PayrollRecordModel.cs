using System.Collections.Generic;

namespace ForPeople.Model.Report
{
    /// <summary>
    /// Модель записи для ведомости по зарплате.
    /// </summary>
    public class PayrollRecordModel
    {
        /// <summary>
        /// Текст.
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Зарплата.
        /// </summary>
        public double Salary { get; set; }

        /// <summary>
        /// Дочерние записи.
        /// </summary>
        public ICollection<PayrollRecordModel> Items { get; } = new List<PayrollRecordModel>();
    }
}
