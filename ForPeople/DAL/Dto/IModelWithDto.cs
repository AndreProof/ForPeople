namespace ForPeople.DAL.Dto
{
    /// <summary>
    /// Интерфейс модели с Дто.
    /// </summary>
    /// <typeparam name="T">Тип дто.</typeparam>
    public interface IModelWithDto<T>
    {
        #region Поля и свойства

        /// <summary>
        /// Ключ.
        /// </summary>
        int Id { get; set; }

        #endregion

        #region Методы

        /// <summary>
        /// Получить дто.
        /// </summary>
        /// <returns>Дто.</returns>
        T GetDto();

        #endregion

    }
}
