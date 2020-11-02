using Cinematheque.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cinematheque.WebSite.Models
{
    public class PersonView : EntityView
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", 
            ErrorMessage = "Name is not valid.\nMust contain only alphabetical characters, hyphens, commas and spaces.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", 
            ErrorMessage = "Surname is not valid.\nMust contain only alphabetical characters, hyphens, commas and spaces.")]
        public string Surname { get; set; }

        public string FullName
        {
            get { return Name + " " + Surname; }
        }

        [Required]
        public DateTime Birth { get; set; }

        public DateTime? Death { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Country { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname + "(" + GetAge() + ")";
        }

        public int GetAge()
        {
            var lastAlive = Death ?? DateTime.Now;
            var difInYears = lastAlive.Year - Birth.Year;
            return (lastAlive.Date < Birth.Date) ? difInYears-- : difInYears;
        }

        public PersonView(Person data) : base(data)
        {
            Name = data.Name;
            Surname = data.Surname;
            Birth = data.Birth;
            Death = data.Death;
            Gender = (Gender)data.Gender;
            Country = data.Country?.EnglishName;
        }

        public PersonView() : base()
        {

        }
    }
}