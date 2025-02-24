using System.ComponentModel.DataAnnotations;

namespace ASS1.ViewModels
{
    public class RegisterVM
    {
        public string? AccountName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? AccountEmail { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password don't match.")]
        public string? ConfirmPassword { get; set; }
    }
}
