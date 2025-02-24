using System.ComponentModel.DataAnnotations;

namespace ASS1.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? AccountEmail { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [DataType(DataType.Password)]
        public string? AccountPassword { get; set; }
    }
}
