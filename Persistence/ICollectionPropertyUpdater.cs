using System;
using System.Collections.Generic;
using System.Linq;

namespace AndrewD.EntityPlus.Persistence
{
    // TODO: Consider grouping EntityPropertyInfo and EntityKeyPropertyInfo into something like EntityTypeInfo or EntityInfo
    public interface ICollectionPropertyUpdater<TModel>
    {
        // Updates .... any object type that has the same property (name- and type-wise)
        void UpdateCollection<TCollectionEntry>(IDbContextReflector reflector, NavigationPropertyInfo property,
            object sourceModel, bool modelIsNew, IRecursiveEntityUpdater entityUpdater)
            where TCollectionEntry : class;
    }
}
