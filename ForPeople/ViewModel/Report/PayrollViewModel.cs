using ForPeople.Model.Company;
using ForPeople.Model.Report;
using System.Collections.Generic;
using System.Linq;

namespace ForPeople.ViewModel.Report
{
    /// <summary>
    /// Модель представления ведомости по зарплате.
    /// </summary>
    public class PayrollViewModel
    {
        #region Поля и свойства

        /// <summary>
        /// Записи.
        /// </summary>
        public ICollection<PayrollRecordViewModel> Items { get; } = new List<PayrollRecordViewModel>();

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="companies">Компании.</param>
        public PayrollViewModel(IEnumerable<CompanyModel> companies)
        {
            foreach (var company in companies)
            {
                var companyRecord = new PayrollRecordModel
                {
                    Text = company.Name
                };

                foreach (var department in company.Departments)
                {
                    var departmentRecord = new PayrollRecordModel
                    {
                        Text = department.Name
                    };

                    foreach (var employee in department.Employers)
                    {
                        departmentRecord.Items.Add(new PayrollRecordModel
                        {
                            Text = employee.FullName,
                            Salary = employee.Salary
                        });
                    }

                    departmentRecord.Salary = departmentRecord.Items.Sum(x => x.Salary);

                    companyRecord.Items.Add(departmentRecord);
                }

                companyRecord.Salary = companyRecord.Items.Sum(x => x.Salary);

                this.Items.Add(new PayrollRecordViewModel(companyRecord));
            }
        }

        #endregion
    }
}
