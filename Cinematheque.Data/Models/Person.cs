using Cinematheque.Data.Utils;
using System;
using System.Globalization;

namespace Cinematheque.Data
{
    public class Person<T> : Entity<T> where T : class//, IEquatable<Person>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birth { get; set; }

        public DateTime? Death { get; set; }

        public Gender Gender { get; set; }

        public RegionInfo Country { get; set; } 

        public Person(string name, string surname, DateTime birth, DateTime? death, Gender gender, RegionInfo country) : base()
        {
            Name = name;
            Surname = surname;
            Birth = birth;
            Death = death;
            Gender = gender;
            Country = country;

            Validate(this);
        }

        public Person() : base()
        { }

        public override string ToString()
        {
            return Name + " " + Surname + " (" + GetAge() + ") ";
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

        private static void Validate(Person<T> p)
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

        /*public bool Equals(Person other)
        {
            return Name.Equals(other.Name) &&
                      Surname.Equals(other.Surname) &&
                      Birth.Equals(other.Birth) &&
                      Death.Equals(other.Death) &&
                      Country.Equals(other.Country) &&
                      Gender.Equals(other.Gender);
        }*/
    }
}
