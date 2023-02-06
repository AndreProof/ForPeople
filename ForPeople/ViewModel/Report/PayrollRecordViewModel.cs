using ForPeople.Model.Report;
using System.Collections.Generic;

namespace ForPeople.ViewModel.Report
{
    /// <summary>
    /// Модель представления записи ведомости по зарплате.
    /// </summary>
    public class PayrollRecordViewModel
    {
        #region Поля и свойства

        /// <summary>
        /// Модель.
        /// </summary>
        private readonly PayrollRecordModel model;

        /// <summary>
        /// Текст.
        /// </summary>
        public string Text => this.model.Text;

        /// <summary>
        /// Зарплата.
        /// </summary>
        public double Salary => this.model.Salary;

        /// <summary>
        /// Дочерние записи.
        /// </summary>
        public ICollection<PayrollRecordViewModel> Items { get; } = new List<PayrollRecordViewModel>();

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="model">Модель записи.</param>
        public PayrollRecordViewModel(PayrollRecordModel model)
        {
            this.model = model;

            if (this.model != null)
            {
                foreach (var payrollRecordModel in this.model.Items)
                {
                    this.Items.Add(new PayrollRecordViewModel(payrollRecordModel));
                }
            }
        }

        #endregion
    }
}
