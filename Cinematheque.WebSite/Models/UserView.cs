using System.ComponentModel.DataAnnotations;

namespace Cinematheque.WebSite.Models
{
    public class UserView : EntityView
    {
        [Required]
        [MinLength(3)]
        public string Login { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmedPassword { get; set; }
    }
}