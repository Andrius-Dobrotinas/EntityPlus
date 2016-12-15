using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecordLabel.TheContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndrewD.EntityPlus
{
    [TestClass]
    public class EntityComparerByNonForeignKeysTests
    {
        private EntityComparerByKeys comparer;

        [TestInitialize]
        public void Init()
        {
            comparer = new EntityComparerByNonForeignKeys();
        }

        [TestMethod]
        public void EntitiesMustBeEqual_When_AllKeysAreEqual_DespiteOtherValues()
        {
            var keyProperties = GetKeyPropertiesMock();

            var entity1 = new FakeClass { Id = 1, DoubleValue = 7.11, Value = "Hey Ho, Let's go!" };
            var entity2 = new FakeClass { Id = 1, DoubleValue = 7.11, Value = "Seven Eleven" };

            var result = comparer.CompareEntities(entity1, entity2, keyProperties);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void EntitiesMustBeEqual_When_NonForeignKeysAreEqual_DespiteOtherValues()
        {
            var keyProperties = GetKeyPropertiesMock();

            var entity1 = new FakeClass { Id = 1, DoubleValue = 5.01, Value = "Hey Ho, Let's go!" };
            var entity2 = new FakeClass { Id = 1, DoubleValue = 7.11, Value = "Seven Eleven" };

            var result = comparer.CompareEntities(entity1, entity2, keyProperties);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void EntitiesNotMustBeEqual_When_NonForeignKeysAreNotEqual_DespiteOtherValues()
        {
            var keyProperties = GetKeyPropertiesMock();

            var entity1 = new FakeClass { Id = 2, DoubleValue = 7.11, Value = "Seven Eleven" };
            var entity2 = new FakeClass { Id = 1, DoubleValue = 7.11, Value = "Seven Eleven" };

            var result = comparer.CompareEntities(entity1, entity2, keyProperties);

            Assert.AreEqual(false, result);
        }
        
        private List<IEntityKeyPropertyInfo> GetKeyPropertiesMock()
        {
            var keyMoq = new Moq.Mock<IEntityKeyPropertyInfo>();

            var idProperty = typeof(FakeClass).GetProperty(nameof(FakeClass.Id));
            keyMoq.Setup(x => x.PropertyInfo).Returns(idProperty);
            keyMoq.Setup(x => x.IsForeignKey).Returns(false);

            var key2Moq = new Moq.Mock<IEntityKeyPropertyInfo>();
            var property2 = typeof(FakeClass).GetProperty(nameof(FakeClass.DoubleValue));
            key2Moq.Setup(x => x.PropertyInfo).Returns(property2);
            key2Moq.Setup(x => x.IsForeignKey).Returns(true);

            var keyProperties = new List<IEntityKeyPropertyInfo>
            {
                keyMoq.Object, key2Moq.Object
            };

            return keyProperties;
        }
    }
}