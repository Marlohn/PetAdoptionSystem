using System.ComponentModel.DataAnnotations;

namespace PetAdoptionSystem.Application.Dtos
{
    public class PetRequestDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [StringLength(50, ErrorMessage = "Type length can't be more than 50.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Breed is required.")]
        [StringLength(50, ErrorMessage = "Breed length can't be more than 50.")]
        public string Breed { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Sex must be either 'Male' or 'Female'.")]
        public string Sex { get; set; }
    }
}
