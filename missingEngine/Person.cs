using System;

namespace missingEngine
{
    public class Person : IEquatable<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Equals(Person other)
        {
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public Person Trim()
        {
            FirstName.Trim();
            LastName.Trim();
            return this;
        }
        // public string Email { get; set; }
    }
}
