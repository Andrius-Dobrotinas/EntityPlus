using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AndrewD.EntityPlus.Persistence
{
    public partial class CollectionMergerTests
    {
        private List<FakeClass> GetOriginalCollection()
        {
            return new List<FakeClass>()
            {
                new FakeClass { Id = 1, Value = "target 1" },
                new FakeClass { Id = 2, Value = "target 2" },
                new FakeClass { Id = 3, Value = "target 3" }
            };
        }

        private List<FakeClass> GetNewCollection_OneItem()
        {
            return new List<FakeClass>()
            {
                new FakeClass { Id = 1,Value = "target 1" }
            };
        }

        private List<IEntityKeyPropertyInfo> GetKeyPropertiesMock()
        {
            var keyMoq = new Moq.Mock<IEntityKeyPropertyInfo>();

            var idProperty = typeof(FakeClass).GetProperty(nameof(FakeClass.Id));
            keyMoq.Setup(x => x.PropertyInfo).Returns(idProperty);
            keyMoq.Setup(x => x.IsForeignKey).Returns(false);

            var keyProperties = new List<IEntityKeyPropertyInfo>
            {
                keyMoq.Object
            };

            return keyProperties;
        }

        /// <summary>
        /// Performs a simple collection merge and returns the resulting collection
        /// </summary>
        /// <param name="removedEntries">Collection of entries that have been removed from the Original collection</param>
        /// <returns>New collection which is a result of a merge</returns>
        private List<FakeClass> Merge(IList<FakeClass> original, IList<FakeClass> newCollection, IList<IEntityKeyPropertyInfo> keyPropertyInfo, ref IList<FakeClass> removedEntries)
        {
            IList<FakeClass> removed = null;
            var result = merger.MergeCollections<FakeClass>(original, newCollection,
                keyPropertyInfo,
                (origEntity, newEntity) =>
                {
                    origEntity.Value = newEntity.Value;
                    return origEntity;
                },
                newEntity => newEntity,
                toRemove => removed = toRemove);

            removedEntries = removed;
            return result;
        }

        private List<FakeClass> Merge(IList<FakeClass> original, IList<FakeClass> newCollection, IList<IEntityKeyPropertyInfo> keyPropertyInfo)
        {
            IList<FakeClass> removedEntries = null;
            return Merge(original, newCollection, keyPropertyInfo, ref removedEntries);
        }
    }


    internal class FakeClass
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}