using System.Windows;

namespace ForPeople.Services
{
    /// <summary>
    /// Сервис работы с окнами.
    /// </summary>
    public static class WindowService
    {
        /// <summary>
        /// Главное окно.
        /// </summary>
        public static Window MainWindow { get; set; }

        /// <summary>
        /// Открыть новое окно.
        /// </summary>
        /// <param name="text">Заголовок окна.</param>
        /// <param name="viewModel">Модель представления для окна.</param>
        public static void OpenWindow(string text, object viewModel)
        {
            var win = new Window
            {
                Content = viewModel,
                Owner = MainWindow,
                Width = 800,
                Height = 450,
                Title = text
            };
            win.ShowDialog();
        }
    }
}
