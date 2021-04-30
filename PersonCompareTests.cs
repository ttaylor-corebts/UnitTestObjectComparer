using Demo.ObjectComparer.UnitTests.Models;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Demo.ObjectComparer.UnitTests
{
    [TestClass]
    public class PersonCompareTests
    {
        #region Private Fields

        private static CompareLogic _compareLogic;

        #endregion Private Fields

        #region Public Methods

        [TestInitialize]
        public void Initialize()
        {
            _compareLogic = new CompareLogic();
            _compareLogic.Config = new ComparisonConfig
            {
                MaxDifferences = 10
            };
        }

        /// <summary>
        /// When the 'IgnoreCollectionOrder' configuration is 'true', the order
        /// of items in a collection will not matter but test result performance
        /// may suffer if the collection is large.
        /// </summary>
        [TestMethod]
        public void ChildCollectionCompareSortedTest()
        {
            var person1 = new Person
            {
                Name = "Greg",
                DateCreated = DateTime.Now,
                Children = new List<Person>
                {
                    new Person { Name = "Doogie" },
                    new Person { Name = "Penelope" }
                }
            };

            var person2 = new Person
            {
                Name = "Greg",
                DateCreated = person1.DateCreated,
                Children = new List<Person>
                {
                    new Person { Name = "Sweepee" },
                    new Person { Name = "Doogie" }
                }
            };

            _compareLogic.Config = new ComparisonConfig
            {
                IgnoreCollectionOrder = true, // Performance impact when set to 'true'
                MaxDifferences = 10
            };

            var compareResult = _compareLogic.Compare(person1, person2);

            if (!compareResult.AreEqual)
            {
                Assert.Fail(compareResult.DifferencesString);
            }
        }

        /// <summary>
        /// When the 'IgnoreCollectionOrder' configuration is 'false', the order
        /// of items in a collection will matter.
        /// </summary>
        [TestMethod]
        public void ChildCollectionCompareUnsortedTest()
        {
            var person1 = new Person
            {
                Name = "Greg",
                DateCreated = DateTime.Now,
                Children = new List<Person>
                {
                    new Person { Name = "Doogie" },
                    new Person { Name = "Penelope" }
                }
            };

            var person2 = new Person
            {
                Name = "Greg",
                DateCreated = person1.DateCreated,
                Children = new List<Person>
                {
                    new Person { Name = "Sweepee" },
                    new Person { Name = "Doogie" }
                }
            };

            _compareLogic.Config = new ComparisonConfig
            {
                IgnoreCollectionOrder = false,
                MaxDifferences = 10
            };

            var compareResult = _compareLogic.Compare(person1, person2);

            if (!compareResult.AreEqual)
            {
                Assert.Fail(compareResult.DifferencesString);
            }
        }

        /// <summary>
        /// Simple comparison of two objects who's properties are all different.
        /// </summary>
        [TestMethod]
        public void SimpleObjectCompareTest()
        {
            var person1 = new Person { Name = "Greg", DateCreated = DateTime.Now };

            var person2 = new Person { Name = "Doug", DateCreated = DateTime.UtcNow };

            var compareResult = _compareLogic.Compare(person1, person2);

            if (!compareResult.AreEqual)
            {
                Assert.Fail(compareResult.DifferencesString);
            }
        }

        #endregion Public Methods
    }
}