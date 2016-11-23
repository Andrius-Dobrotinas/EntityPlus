using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AndrewD.EntityPlus;
using RecordLabel.TheContext;

namespace EPCoreTests
{
    [TestClass]
    public class DbContextReflectorTests
    {
        ReleaseContext dbContext;
        IDbContextReflector reflector;

        [TestInitialize]
        public void Init()
        {
            dbContext = new ReleaseContext();
            reflector = new DbContextReflector(dbContext, "RecordLabel.TheContext", "TheContext");
        }

        [TestCleanup]
        public void Destroy()
        {
            dbContext.Dispose();
        }

        [TestMethod]
        public void GetAllNavigationProperties_MustFindAllReferenceNavigationalProperties()
        {
            var navProperties = reflector.GetAllNavigationProperties<Track>();

            Assert.AreNotEqual(null, navProperties.FirstOrDefault(x => x.PropertyName == "Release"), "Navigation property for Release not found");
            
            Assert.AreEqual(1, navProperties.Length, "Wrong number of navigational properties found");
        }

        [TestMethod]
        public void GetCollectionNavigationProperties_MustFindCollectionNavigationalPropertiesInBaseEntity()
        {
            var navProperties = reflector.GetCollectionNavigationProperties<Release>();

            Assert.AreNotEqual(null, navProperties.FirstOrDefault(x => x.PropertyName == "Metadata"), "Collection Navigation property for Metadata not found");
            Assert.AreNotEqual(null, navProperties.FirstOrDefault(x => x.PropertyName == "References"), "Collection Navigation property for References not found");

            Assert.AreEqual(1, navProperties.Length, "Wrong number of collection navigational properties found");
        }
    }
}
