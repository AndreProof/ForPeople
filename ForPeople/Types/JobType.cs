using System.ComponentModel.DataAnnotations;
using ForPeople.R;

namespace ForPeople.Types
{
    /// <summary>
    /// Должность.
    /// </summary>
    public enum JobTitle
    {
        /// <inheritdoc cref="CommonStrings.None"/>
        [Display(Name = nameof(CommonStrings.None), ResourceType = typeof(CommonStrings))]
        None = 0,

        /// <inheritdoc cref="CommonStrings.Trainee"/>
        [Display(Name = nameof(CommonStrings.Trainee), ResourceType = typeof(CommonStrings))]
        Trainee = 1,

        /// <inheritdoc cref="CommonStrings.Specialist"/>
        [Display(Name = nameof(CommonStrings.Specialist), ResourceType = typeof(CommonStrings))]
        Specialist = 2,

        /// <inheritdoc cref="CommonStrings.Manager"/>
        [Display(Name = nameof(CommonStrings.Manager), ResourceType = typeof(CommonStrings))]
        Manager = 3
    }
}
