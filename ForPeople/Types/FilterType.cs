using System.ComponentModel.DataAnnotations;
using ForPeople.R;

namespace ForPeople.Types
{
    /// <summary>
    /// Тип фильтра.
    /// </summary>
    public enum FilterType
    {
        /// <inheritdoc cref="CommonStrings.Age"/>
        [Display(Name = nameof(CommonStrings.Age), ResourceType = typeof(CommonStrings))]
        Age,

        /// <inheritdoc cref="CommonStrings.YearOfBirth"/>
        [Display(Name = nameof(CommonStrings.YearOfBirth), ResourceType = typeof(CommonStrings))]
        YearOfBirth
    }
}
