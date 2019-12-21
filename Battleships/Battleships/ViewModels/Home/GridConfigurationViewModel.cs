using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Battleships.ViewModels.Home
{
    public class GridConfigurationViewModel : IValidatableObject
    {
        [Range(1, 100, ErrorMessage = "Number of rows not provided.")]
        public int Rows { get; set; }
        [Range(1, 100, ErrorMessage = "Number of columns not provided.")]
        public int Columns { get; set; }
        [Range(1, 100, ErrorMessage = "Number of battleships not provided.")]
        public int Battleships { get; set; }
        [Range(1, 100, ErrorMessage = "Number of destroyers not provided.")]
        public int Destroyers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Rows * Columns < 2 * ((4 * Destroyers) + (5 * Battleships)))
            {
                yield return new ValidationResult(
                    $"Too many ships for the grid size.");
            }
        }
    }
}
