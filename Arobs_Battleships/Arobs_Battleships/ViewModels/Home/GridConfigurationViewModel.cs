using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arobs_Battleships.ViewModels.Home
{
    public class GridConfigurationViewModel : IValidatableObject
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Battleships { get; set; }
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
