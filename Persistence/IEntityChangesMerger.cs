using System;

namespace AndrewD.EntityPlus.Persistence
{
    /// <summary>
    /// Implementations are responsible for performing all necessary operations in order to merge changes from
    /// model to an entity in the context
    /// </summary>
    /// <typeparam name="TModel">Type of entity that this will be used for</typeparam>
    public interface IEntityChangesMerger<TModel> where TModel : class
    {
        /// <summary>
        /// Implementations must perform complete merge of all changes from the supplied model to a corresponding
        /// entity in the context or attach the model to the context if it's new
        /// </summary>
        /// <param name="model">Model that contains the changes</param>
        void MergeEntityChanges(TModel model);
    }
}
