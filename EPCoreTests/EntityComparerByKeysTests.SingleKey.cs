using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecordLabel.TheContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndrewD.EntityPlus
{
    [TestClass]
    public class EntityComparerByKeysTests_SingleKey
    {
        private EntityComparerByKeys comparer;

        [TestInitialize]
        public void Init()
        {
            comparer = new EntityComparerByKeys();
        }

        [TestMethod]
        public void EntitiesMustBeNotEqual_When_KeysNotEqual_DespiteOtherValues()
        {
            var keyProperties = GetKeyPropertiesMock();

            var entity1 = new FakeClass { Id = 1, DoubleValue = 7.11, Value = "Seven Eleven" };
            var entity2 = new FakeClass { Id = 2, DoubleValue = 7.11, Value = "Seven Eleven" };

            var result = comparer.CompareEntities(entity1, entity2, keyProperties);

            Assert.AreEqual(false, result, "Two entities with same values but different key property values are supposed to be not equal");
        }

        [TestMethod]
        public void EntitiesMustBeNotEqual_When_KeysNotEqual_And_OneKeyIsDefault_DespiteOtherValues()
        {
            var keyProperties = GetKeyPropertiesMock();

            var entity1 = new FakeClass { Id = 1, DoubleValue = 7.11, Value = "Seven Eleven" };
            var entity2 = new FakeClass { Id = default(int), DoubleValue = 7.11, Value = "Seven Eleven" };

            var result = comparer.CompareEntities(entity1, entity2, keyProperties);

            Assert.AreEqual(false, result, "Two entities with same values but different key property values are supposed to be not equal");
        }

        [TestMethod]
        public void EntitiesMustBeEqual_When_KeysAreEqual_But_OtherValuesAreNotEqual()
        {
            var keyProperties = GetKeyPropertiesMock();

            var entity1 = new FakeClass { Id = 1, DoubleValue = 1.0, Value = "Hey Ho, Let's go!" };
            var entity2 = new FakeClass { Id = 1, DoubleValue = 7.11, Value = "Seven Eleven" };

            var result = comparer.CompareEntities(entity1, entity2, keyProperties);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void EntitiesMustBeEqual_When_KeysAreEqual_And_BothAreDefault()
        {
            var keyProperties = GetKeyPropertiesMock();

            var entity1 = new FakeClass { Id = default(int), DoubleValue = 1.0, Value = "What's up, dog?" };
            var entity2 = new FakeClass { Id = default(int), DoubleValue = 7.11, Value = "Seven Eleven" };

            var result = comparer.CompareEntities(entity1, entity2, keyProperties);

            Assert.AreEqual(true, result);
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
    }
}