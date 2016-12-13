using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AndrewD.EntityPlus
{
    public interface IEntityKeyPropertyInfo
    {
        int? Order { get; }
        bool IsForeignKey { get; }
        //NavigationProperty RelatedNavigationProperty { get; }
        PropertyInfo PropertyInfo { get; }
        Type PropertyType { get; }
        object DefaultValue { get; }
    }
}
