using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace ForPeople.DAL
{
    /// <summary>
    /// Репозиторий дто.
    /// </summary>
    /// <typeparam name="T">Тип объекта.</typeparam>
    public class Repository<T>
        where T : class
    {
        #region Поля и свойства

        /// <summary>
        /// Сервис данных.
        /// </summary>
        private readonly SQLiteDataContext dataContext;

        /// <summary>
        /// Транзакция.
        /// </summary>
        private readonly DbTransaction transaction;
        #endregion

        #region IRepository

        /// <summary>
        /// Получить все записи.
        /// </summary>
        /// <returns>Коллекция записей.</returns>
        public IReadOnlyCollection<T> Get()
        {
            return this.dataContext.Connection.GetAll<T>(this.transaction).ToList();
        }
        
        /// <summary>
        /// Обновить.
        /// </summary>
        /// <param name="obj">Дто.</param>
        public void Update(T obj)
        {
            this.dataContext.Connection.Update(obj, this.transaction);
        }

        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="obj">Дто.</param>
        /// <returns>Ключ записи.</returns>
        public int Add(T obj)
        {
            return (int)this.dataContext.Connection.Insert(obj, this.transaction);
        }

        /// <summary>
        /// Удалить запись.
        /// </summary>
        /// <param name="obj">Дто.</param>
        public void Delete(T obj)
        {
            this.dataContext.Connection.Delete(obj, this.transaction);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dataContext">Сервис данных.</param>
        public Repository(SQLiteDataContext dataContext, DbTransaction transaction)
        {
            this.dataContext = dataContext;
            this.transaction = transaction;
        }

        #endregion
    }
}
