using System;
using System.Data.Common;
using System.Data.SQLite;

namespace ForPeople.DAL
{
    /// <summary>
    /// Контекст данных.
    /// </summary>
    public class SQLiteDataContext : IDisposable
    {
        #region Поля и свойства

        /// <summary>
        /// Соединение.
        /// </summary>
        public DbConnection Connection { get; }

        #endregion

        #region IDisposable

        private bool disposed = false;

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Connection.Close();
                    this.Connection.Dispose();
                }
            }

            this.disposed = true;
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dbPath">Путь к файлу базы данных.</param>
        public SQLiteDataContext(string dbPath)
        {
            this.Connection = new SQLiteConnection("Data Source=" + dbPath + ";foreign keys=true", new Uri(dbPath).IsUnc);
            this.Connection.Open();
        }

        #endregion
    }
}