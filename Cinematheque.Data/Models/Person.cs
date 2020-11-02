using Cinematheque.Utils;
using System;
using System.Globalization;

namespace Cinematheque.Data
{
    public class Person : Entity//<T> where T : class, IEquatable<Person>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birth { get; set; }

        public DateTime? Death { get; set; }

        public Gender Gender { get; set; }

        public RegionInfo Country { get; set; } 

        public Person() : base()
        {
            //Validate(this);
        }

        public int GetAge()
        {
            var lastAlive = Death ?? DateTime.Now;
            var difInYears = lastAlive.Year - Birth.Year;

            return (lastAlive.Date < Birth.Date) ? difInYears-- : difInYears;
        }

        public string GetFullName()
        {
            return Name + " " + Surname;
        }

        private static void Validate(Person p)
        {
            if (!Validator.IsNameValid(p.Name, p.Surname))
            {
                throw new Exception("Name is not valid");
            }

            if (!Validator.IsDatePast(p.Birth))
            {
                throw new Exception("Birth date is not valid");
            }

            if (p.Death.HasValue && !Validator.IsDatePast(p.Death.Value))
            {
                throw new Exception("Death date is not valid");
            }
        }
    }
}
