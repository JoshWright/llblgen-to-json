using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace LLBLGenExtensions.EntityExtensions.Searching
{
    public static class RelatedCollectionExtensions
    {

        #region Getting Related Entities

        public static EntityBase GetRelatedEntity(this EntityBase entity, IEntityRelation relation)
        {
            var relatedEntities = GetRelatedEntities(entity, relation);
            return relatedEntities[0];
        }

        public static List<EntityBase> GetRelatedEntities(this EntityBase entity, IEntityRelation relation)
        {
            var relatedEntities = new List<EntityBase>();
            var relatedData = entity.GetRelatedData();
            foreach (KeyValuePair<string, object> pair in relatedData)
            {
                if (pair.Key == relation.MappedFieldName)
                {
                    if(pair.Value == null)
                        throw new Exception("Cannot serialize '"+pair.Key + "' to JSON because it was not prefetched");
                    if (pair.Value is EntityBase)
                    {
                        relatedEntities.Add(pair.Value as EntityBase);
                    }
                    else
                    {
                        var recievedEntities = pair.Value as IEntityCollection;
                        foreach (EntityBase currentRelatedEntity in recievedEntities)
                            relatedEntities.Add(currentRelatedEntity);
                    }
                }
            }
            return relatedEntities;
        }

        public static List<EntityBase> ToListOfEntityBases(this IEntityCollection entities)
        {
            var entityList = new List<EntityBase>();
            foreach (EntityBase entity in entities)
                entityList.Add(entity);
            return entityList;
        }

        #endregion

    }
}
