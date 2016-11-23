using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Data.Entity.Core.Metadata.Edm;

namespace AndrewD.EntityPlus
{
    public class NavigationPropertyInfo
    {
        public PropertyInfo PropertyInfo { get; protected set; }
        public bool ReferencedEntityIsDependent { get; protected set; }
        public OperationAction ReferencedEntityDeleteBehavior { get; protected set; }

        protected NavigationPropertyInfo()
        {

        }

        public NavigationPropertyInfo(PropertyInfo propertyInfo, OperationAction referencedEntityDeleteBehavior,
            bool referencedEntityIsDependent)
        {
            PropertyInfo = propertyInfo;
            ReferencedEntityDeleteBehavior = referencedEntityDeleteBehavior;
            ReferencedEntityIsDependent = referencedEntityIsDependent;
        }
        
    }
}
