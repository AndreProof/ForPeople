using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ForPeople.Extension
{
    /// <summary>
    /// Расширение для объектов.
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Получить имя из DisplayAttribute.
        /// </summary>
        /// <param name="enumValue">Значение перечисления.</param>
        /// <returns>Имя.</returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            var result = enumValue.ToString();
            var type = enumValue.GetType();
            var fieldInfo = type.GetField(enumValue.ToString());
            if (fieldInfo != null)
            {
                var descriptionAttribute =
                    (fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[])
                    ?.FirstOrDefault();
                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.GetName();
                }
            }

            return result;
        }

        /// <summary>
        /// Получить возраст.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <returns>Возраст.</returns>
        public static int GetAge(this DateTime date)
        {
            var now = DateTime.Now;
            var years = now.Year - date.Year;

            if (now.Month < date.Month || (now.Month == date.Month && now.Day < date.Day))
            {
                years--;
            }

            return years;
        }
    }
}
