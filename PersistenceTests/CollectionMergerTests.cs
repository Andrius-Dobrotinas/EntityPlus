using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AndrewD.EntityPlus.Persistence
{
    [TestClass]
    public partial class CollectionMergerTests
    {
        ICollectionMerger merger;

        [TestInitialize]
        public void Setup()
        {
            IEntityComparerByKeys comparer = new EntityComparerByNonForeignKeys();
            merger = new CollectionMerger(comparer);
        }
                
        [TestMethod]
        public void EachEntryInResultingCollectionMustHaveNoMoreThanOneMatchInTheNewOrOriginalCollection()
        {
            var originalCollection = GetOriginalCollection();
            var keyProperties = GetKeyPropertiesMock();
            var newCollection = new List<FakeClass>()
            {
                new FakeClass { Id = 1,Value = "target 1" }
            };


            // Run test
            var resultingCollection = Merge(originalCollection, newCollection, keyProperties);

            // Make sure each entry in Resulting collection has no more than one match in the New or Original collection
            foreach (var item in resultingCollection)
            {
                Assert.IsFalse(newCollection.Count(x => x == item) > 1, "Entry in Resulting collection has more than 1 match in New collection");
                Assert.IsFalse(originalCollection.Count(x => x == item) > 1, "Entry in Resulting collection has more than 1 match in Original collection");

                Assert.IsNotNull(newCollection.SingleOrDefault(x => x == item)
                    ?? originalCollection.SingleOrDefault(x => x == item),
                    "Resulting collection doesn't contain items that aren't present in the new collection");
            }
        }

        [TestMethod]
        public void MustRemoveItemsFromCollection()
        {
            var originalCollection = GetOriginalCollection();
            var keyProperties = GetKeyPropertiesMock();
            var newCollection = GetNewCollection_OneItem();
            var expectedNumberOfRemovedItems = 2;

            // Run test
            IList<FakeClass> removedEntries = null;
            var resultingCollection = Merge(originalCollection, newCollection, keyProperties, ref removedEntries);

            Assert.IsNotNull(removedEntries, "Entries are supposed to be removed");

            Assert.AreEqual(newCollection.Count, resultingCollection.Count, "Resulting collection is supposed to have the same number of entries as the New Collection");

            Assert.AreEqual(expectedNumberOfRemovedItems, removedEntries.Count, "Removed wrong number of entries");

            // Make sure removed entries are not present in the Resulting collection
            foreach (var item in removedEntries)
            {
                Assert.AreEqual(0, resultingCollection.Count(x => x == item), "Resulting collection may not contain entries that are in the collection of removed entries");
            }
        }

        [TestMethod]
        public void MustRemoveItemsFromCollection_OnlyItemsInOriginalCollectionMayBeRemoved()
        {
            var originalCollection = GetOriginalCollection();
            var keyProperties = GetKeyPropertiesMock();
            var newCollection = GetNewCollection_OneItem();

            // Run test
            IList<FakeClass> removedEntries = null;
            var resultingCollection = Merge(originalCollection, newCollection, keyProperties, ref removedEntries);
            
            foreach (var item in removedEntries)
            {
                Assert.IsTrue(newCollection.Count(x => x == item) == 0, "Collection of removed entries is not supposed to contain entries from the New collection");
            }
        }

        [TestMethod]
        public void RemoveAllAndAdd1()
        {
            var originalCollection = GetOriginalCollection();
            var keyProperties = GetKeyPropertiesMock();
            
            // New collectain contains an items that's not present in the original collection (different Id)
            var newCollection = new List<FakeClass>()
            {
                new FakeClass { Id = 0,Value = "target 2" }
            };

            // Run test
            var resultingCollection = Merge(originalCollection, newCollection, keyProperties);

            Assert.AreEqual(1, resultingCollection.Count, "Resulting collection is supposed to contain exactly one item");
            Assert.AreEqual(newCollection[0], resultingCollection[0], "Resulting collection is supposed to contain the new item");
        }

        [TestMethod]
        public void MustNotRemoveAnyEntriesWhenUpdating()
        {
            var originalCollection = GetOriginalCollection();
            var keyProperties = GetKeyPropertiesMock();
            var newCollection = GetOriginalCollection();

            var newValue = "New value";
            newCollection[1].Value = newValue;

            // Run test
            IList<FakeClass> removedEntries = null;
            var resultingCollection = Merge(originalCollection, newCollection, keyProperties, ref removedEntries);

            Assert.IsNull(removedEntries, "Not supposed to remove any entries when simply updating existing entries");
        }

        [TestMethod]
        public void MustUpdate1Item()
        {
            var originalCollection = GetOriginalCollection();
            var keyProperties = GetKeyPropertiesMock();
            var newCollection = GetOriginalCollection();

            var newValue = "New value";
            newCollection[1].Value = newValue;
                    
            // Run test
            var resultingCollection = Merge(originalCollection, newCollection, keyProperties);

            Assert.IsNotNull(resultingCollection.SingleOrDefault(x => x.Value == newValue), "Resulting collection must contain the entry with the new value");
        }

        [TestMethod]
        public void MustUpdateMultipleItems()
        {
            var originalCollection = GetOriginalCollection();
            var keyProperties = GetKeyPropertiesMock();
            var newCollection = GetOriginalCollection();

            var newValue = "New value";
            var newValue2 = "New value Too";
            newCollection[1].Value = newValue;
            newCollection[2].Value = newValue2;

            // Run test
            var resultingCollection = Merge(originalCollection, newCollection, keyProperties);
            
            Assert.IsNotNull(resultingCollection.SingleOrDefault(x => x.Value == newValue), "Resulting collection must contain the entry with the new value");
            Assert.IsNotNull(resultingCollection.SingleOrDefault(x => x.Value == newValue2), "Resulting collection must contain the entry with the new value");
        }

        [TestMethod]
        public void MustNotRemoveItemsWhenAdding()
        {
            var originalCollection = GetOriginalCollection();
            var keyProperties = GetKeyPropertiesMock();
            var newCollection = GetOriginalCollection();

            var newEntry = new FakeClass { Value = "New entry" };
            newCollection.Add(newEntry);

            // Run test
            IList<FakeClass> removedEntries = null;
            var resultingCollection = Merge(originalCollection, newCollection, keyProperties, ref removedEntries);

            Assert.IsNull(removedEntries, "Not supposed to remove any entries when simply adding new entries");
        }

        [TestMethod]
        public void MustAdd1Item()
        {
            var originalCollection = GetOriginalCollection();
            var keyProperties = GetKeyPropertiesMock();
            var newCollection = GetOriginalCollection();

            var newEntry = new FakeClass { Value = "New entry" };
            newCollection.Add(newEntry);

            // Run test
            var resultingCollection = Merge(originalCollection, newCollection, keyProperties);

            Assert.AreEqual(4, resultingCollection.Count);
            Assert.IsTrue(resultingCollection.Contains(newEntry), "Resulting collection is supposed to contain the new entry");
        }

        [TestMethod]
        public void MustAddMultipleItems()
        {
            var originalCollection = GetOriginalCollection();
            var keyProperties = GetKeyPropertiesMock();
            var newCollection = GetOriginalCollection();

            var newEntry = new FakeClass { Value = "New entry" };
            var newEntryToo = new FakeClass { Value = "New entry, Too" };
            newCollection.Add(newEntry);
            newCollection.Add(newEntryToo);

            // Run test
            var resultingCollection = Merge(originalCollection, newCollection, keyProperties);

            Assert.AreEqual(5, resultingCollection.Count);
            Assert.IsTrue(resultingCollection.Contains(newEntry), "Resulting collection is supposed to contain the new entry");
            Assert.IsTrue(resultingCollection.Contains(newEntryToo), "Resulting collection is supposed to contain the second new entry");
        }
    }
}