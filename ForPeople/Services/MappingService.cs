using ForPeople.DAL;
using ForPeople.DAL.Dto;
using ForPeople.Model.Company;
using ForPeople.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ForPeople.Services
{
    /// <summary>
    /// Сервис работы с базой данных.
    /// </summary>
    public static class MappingService
    {
        /// <summary>
        /// Путь до базы данных.
        /// </summary>
        private static readonly string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "R", "DB.db");

        /// <summary>
        /// Обновить запись в БД.
        /// </summary>
        /// <typeparam name="T">Тип дто.</typeparam>
        /// <param name="model">Модель записи.</param>
        public static void Update<T>(IModelWithDto<T> model) where T : class
        {
            using var unitOfWork = new DatabaseManager(path);
            unitOfWork.GetRepository<T>().Update(model.GetDto());
            unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Добавить запись в БД.
        /// </summary>
        /// <typeparam name="T">Тип дто.</typeparam>
        /// <param name="model">Модель записи.</param>
        public static void Add<T>(IModelWithDto<T> model) where T : class
        {
            using var unitOfWork = new DatabaseManager(path);
            var newId = unitOfWork.GetRepository<T>().Add(model.GetDto());
            model.Id = newId;
            unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Удалить запись из БД.
        /// </summary>
        /// <typeparam name="T">Тип дто.</typeparam>
        /// <param name="model">Модель записи.</param>
        public static void Delete<T>(IModelWithDto<T> model) where T : class
        {
            using var unitOfWork = new DatabaseManager(path);
            unitOfWork.GetRepository<T>().Delete(model.GetDto());
            unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Открыть и зачитать базу данных.
        /// </summary>
        /// <returns>Коллекция компаний.</returns>
        public static ICollection<CompanyModel> Open()
        {
            var result = new List<CompanyModel>();

            using var unitOfWork = new DatabaseManager(path);
            var companyDtos = unitOfWork.GetRepository<CompanyDto>().Get();
            var departmentDtos = unitOfWork.GetRepository<DepartmentDto>().Get();
            var employeeDto = unitOfWork.GetRepository<EmployeeDto>().Get();

            foreach (var companyDto in companyDtos)
            {
                var companyModel = new CompanyModel
                {
                    Id = companyDto.Id,
                    Date = DateTime.Parse(companyDto.Date, CultureInfo.InvariantCulture),
                    Name = companyDto.Name,
                    LegalAddress = companyDto.LegalAddress
                };

                result.Add(companyModel);

                var companyDepartments = departmentDtos.Where(x => x.CompanyId == companyModel.Id);
                foreach (var companyDepartmentDto in companyDepartments)
                {
                    var departmentModel = new DepartmentModel
                    {
                        Id = companyDepartmentDto.Id,
                        Name = companyDepartmentDto.Name
                    };

                    companyModel.Departments.Add(departmentModel);

                    var employers = employeeDto.Where(x => x.DepartmentId == companyDepartmentDto.Id);
                    foreach (var employer in employers)
                    {
                        departmentModel.Employers.Add(new EmployeeModel
                        {
                            Id = employer.Id,
                            Department = departmentModel,
                            DateOfBirth = DateTime.Parse(employer.DateOfBirth, CultureInfo.InvariantCulture),
                            DateOfEmployment = DateTime.Parse(employer.DateOfEmployment, CultureInfo.InvariantCulture),
                            JobTitle = (JobTitle) employer.JobTitle,
                            Salary = employer.Salary,
                            Name = employer.Name,
                            Surname = employer.Surname,
                            SecondName = employer.SecondName
                        });
                    }

                    departmentModel.Manager =
                        departmentModel.Employers.FirstOrDefault(x => x.Id == companyDepartmentDto.ManagerId);
                }
            }

            return result;
        }
    }
}
