using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models.ViewModels
{
    public class UserProfileViewModel
    {
        // Make required strings non-nullable
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty; // Initialize to empty

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty; // Initialize to empty

        // Nullable int with Range is fine
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int? Age { get; set; } // Keep nullable if 0 is invalid or it can be omitted

        [Display(Name = "Favorite Genre")]
        public string? FavoriteGenre { get; set; } // Keep nullable if optional
    }
}