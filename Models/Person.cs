using System;
using System.Collections.Generic;

namespace Demo.ObjectComparer.UnitTests.Models
{
    public class Person : IEquatable<Person>
    {
        #region Public Properties

        public List<Person> Children { get; set; }

        public DateTime DateCreated { get; set; }

        public string Name { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override bool Equals(object obj)
        {
            return Equals(obj as Person);
        }

        public bool Equals(Person other)
        {
            return other != null &&
                   DateCreated == other.DateCreated &&
                   Name == other.Name &&
                   EqualityComparer<List<Person>>.Default.Equals(Children, other.Children);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DateCreated, Name, Children);
        }

        #endregion Public Methods
    }
}