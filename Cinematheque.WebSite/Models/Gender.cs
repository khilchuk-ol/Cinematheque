using System.ComponentModel.DataAnnotations;

namespace Cinematheque.WebSite.Models
{
    public enum Gender
    {
        [Display(Name ="Not identified")]
        None = 0,
        
        Male = 1,

        Female = 2
    }
}
