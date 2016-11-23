using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AndrewD.EntityPlus.Persistence
{
    /// <summary>
    /// Main class for merging changes from model to entity in the context
    /// </summary>
    /// <typeparam name="TModel">Type of entity that this will be used for</typeparam>
    public class DefaultEntityChangesMerger<TModel> : IEntityChangesMerger<TModel>
        where TModel : class
    {
        private DbContext DbContext;
        private IDbContextReflector reflector;
        private IEntityUpdater scalarUpdater;
        private IEntityComparerByKeys entityComparer;
        private ICollectionMerger collectionMerger;
        private IRecursiveEntityUpdater navUpdater;

        /// <summary>
        /// Instantiates the class using default components
        /// </summary>
        /// <param name="dbContext">Context in which to perform the merge operation</param>
        /// <param name="modelsNamespace">Namespace in which all context entities are defined (all models must be within 
        /// the same namespace. If model type is in a different namespace, an exception will be thrown when instantiating
        /// the type. This is, hopefully, a temporary workaround, until I find a good solution for this)</param>
        /// <param name="modelAssemblyName">Name of assembly which contains all context enties. Again, just like with the
        /// namespace parameter, this is a temporary workaround</param>
        public DefaultEntityChangesMerger(DbContext dbContext, string modelsNamespace, string modelAssemblyName)
        {
            DbContext = dbContext;
            reflector = new DbContextReflector(DbContext, modelsNamespace, modelAssemblyName);
            scalarUpdater = new ScalarPropertyUpdater(DbContext, reflector);
            entityComparer = new EntityComparerByNonForeignKeys();
            collectionMerger = new CollectionMerger(entityComparer);
            navUpdater = new NavigationPropertyUpdater(DbContext, scalarUpdater, reflector);
        }

        /// <summary>
        /// Merges model collection properties. May be used in cases where DbContextReflector is unable to identify all collection properties
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity">Entity in the context</param>
        /// <param name="model">Model that contains changes</param>
        /// <param name="modelProperty"></param>
        /// <param name="actionOnDelete"></param>
        /// <param name="referencedEntityIsDependent"></param>
        protected void MergeCollectionProperty<TProperty>(TModel entity, TModel model, Expression<Func<TModel, IList<TProperty>>> modelProperty,
            System.Data.Entity.Core.Metadata.Edm.OperationAction actionOnDelete, bool referencedEntityIsDependent)
            where TProperty : class
        {
            MemberExpression mex = (MemberExpression)modelProperty.Body;
            PropertyInfo property = (PropertyInfo)mex.Member;
            NavigationPropertyInfo propertyInfo = new NavigationPropertyInfo(property, actionOnDelete, referencedEntityIsDependent);

            ICollectionPropertyUpdater<TModel> collectionPropertyUpdater = new CollectionPropertyUpdater<TModel>(DbContext, entity, collectionMerger);
            collectionPropertyUpdater.UpdateCollection<TProperty>(reflector, propertyInfo, model, entity == model, navUpdater);
        }

        /// <summary>
        /// Merge all changes from the supplied model to a corresponding entity in the context or attaches the
        /// model to the context if it's new
        /// </summary>
        /// <param name="model">Model that contains the changes</param>
        public void MergeEntityChanges(TModel model)
        {
            var entity = scalarUpdater.UpdateEntity(model);
            IRecursiveEntityUpdater updater = new EntityUpdater(DbContext, reflector, scalarUpdater, collectionMerger);
            updater.UpdateEntity<TModel>(model, navUpdater);

            OnMergeChanges(entity, model);
        }

        /// <summary>
        /// Gets called at the end of MergeEntityChanges and may be used to perform extra merge operations
        /// </summary>
        /// <param name="updatedEntity"></param>
        /// <param name="model"></param>
        protected virtual void OnMergeChanges(TModel updatedEntity, TModel model)
        {

        }
    }
}
