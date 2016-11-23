using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace AndrewD.EntityPlus
{
    public sealed class EntityNavigationPropertyInfo : NavigationPropertyInfo
    {
        private const string PropertyInfoMetadataName = "ClrPropertyInfo";

        public NavigationProperty Property { get; }
        public string PropertyName => Property.Name;
        
        public Type ReferencedEntityType => PropertyInfo.PropertyType;
        
        
        public EntityRelationshipType Relationship { get; }

        public EntityNavigationPropertyInfo(NavigationProperty property, EntityRelationshipResolver relationshipResolver)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));
            if (relationshipResolver == null)
                throw new ArgumentNullException(nameof(relationshipResolver));

            Property = property;
            PropertyInfo = (System.Reflection.PropertyInfo)property.MetadataProperties[PropertyInfoMetadataName].Value;
            ReferencedEntityDeleteBehavior = property.FromEndMember.DeleteBehavior;
            ReferencedEntityIsDependent = relationshipResolver.DetermineIfEndMemberIsDependent(property);
            Relationship = relationshipResolver.GetRelationshipType(property);
        }
    }
}
