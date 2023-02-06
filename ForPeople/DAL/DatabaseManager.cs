using System;
using System.Data.Common;

namespace ForPeople.DAL
{
    /// <summary>
    /// Менеджер базы данных.
    /// </summary>
    public class DatabaseManager : IDisposable
    {
        #region Поля и свойства

        /// <summary>
        /// Контекст данных.
        /// </summary>
        private SQLiteDataContext dataContext;

        /// <summary>
        /// Транзакция.
        /// </summary>
        private DbTransaction transaction;

        /// <summary>
        /// Имя файла.
        /// </summary>
        public string FileName { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Получить репозиторий.
        /// </summary>
        /// <typeparam name="T">Тип репозитория.</typeparam>
        /// <returns>Репозиторий.</returns>
        public Repository<T> GetRepository<T>()
            where T : class
        {
            return new Repository<T>(this.dataContext, this.transaction);
        }

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public void SaveChanges()
        {
            this.transaction?.Commit();
        }

        #endregion

        #region IDisposable

        private bool disposed = false;

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.transaction?.Dispose();
                    this.dataContext?.Dispose();
                }
            }

            this.disposed = true;
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="fileName">Имя файла базы данных.</param>
        public DatabaseManager(string fileName)
        {
            this.FileName = fileName;
            this.dataContext = new SQLiteDataContext(fileName);
            this.transaction = this.dataContext?.Connection?.BeginTransaction();
        }

        #endregion
    }
}
