using System;
using System.Collections.Generic;
using LLBLGenExtensions.EntityExtensions.Searching;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace LLBLGenExtensions.EntityExtensions.Searching
{
    public static class FieldCollectionExtensions
    {

        #region Get Single Value

        public static string Get(this EntityBase entity, IEntityField field)
        {
            object value = Get<object>(entity, field);

            if(value == null)
                return "";

            if (value.GetType() == typeof(bool))
                return value.ToString().ToLower();

            return value.ToString();
        }

        public static T Get<T>(this EntityBase entity, IEntityField field)
        {
            return Get<T>(entity, field, new List<IEntityRelation>());
        }

        public static T Get<T>(this EntityBase entity, IEntityField field, IEntityRelation relation)
        {
            return GetList<T>(new List<EntityBase>() { entity }, field, new List<IEntityRelation>() { relation })[0];
        }

        public static T Get<T>(this EntityBase entity, IEntityField field, List<IEntityRelation> relations)
        {
            return GetList<T>(new List<EntityBase>() { entity }, field, relations)[0];
        }

        #endregion


        #region Get Collection of Values

        public static List<T> GetList<T>(this IEntityCollection entities, EntityField field)
        {
            return GetList<T>(entities, field, new List<IEntityRelation>());
        }

        public static List<T> GetList<T>(this IEntityCollection entities, EntityField field, IEntityRelation relation)
        {
            return GetList<T>(entities, field, new List<IEntityRelation> { relation });
        }

        public static List<T> GetList<T>(this IEntityCollection entities, EntityField field, List<IEntityRelation> relations)
        {
            return GetList<T>(entities.ToListOfEntityBases(), field, relations);
        }

        public static List<T> GetList<T>(this EntityBase entity, IEntityField field, IEntityRelation relation)
        {
            return GetList<T>(new List<EntityBase>() { entity }, field, new List<IEntityRelation>(){ relation });
        }

        public static List<T> GetList<T>(this EntityBase entity, IEntityField field, List<IEntityRelation> relations)
        {
            return GetList<T>(new List<EntityBase>() { entity }, field, relations);
        }

        public static List<T> GetList<T>(this List<EntityBase> entities, IEntityField field, IEntityRelation relation)
        {
            return GetList<T>(entities, field, new List<IEntityRelation>() { relation });
        }

        public static List<T> GetList<T>(this List<EntityBase> entities, IEntityField field, List<IEntityRelation> relations)
        {
            List<EntityBase> relatedEntities = GetRelatedEntities(entities, new List<IEntityRelation>(relations));

            if (relatedEntities.Count == 0)
                return null;

            var objects = new List<T>();
            foreach (EntityBase relatedEntity in relatedEntities)
                objects.Add((T)relatedEntity.GetCurrentFieldValue(field.FieldIndex));

            return objects;
        }

        #endregion


        #region Helper Methods

        public static List<IEntityRelation> To(this IEntityRelation relation1, IEntityRelation relation2)
        {
            return new List<IEntityRelation> { relation1, relation2 };
        }

        public static List<IEntityRelation> To(this List<IEntityRelation> relations, IEntityRelation newRelation)
        {
            relations.Add(newRelation);
            return relations;
        }

        private static List<EntityBase> GetRelatedEntities(List<EntityBase> entities, IList<IEntityRelation> relations)
        {
            if (relations == null || relations.Count == 0)
                return entities;

            var relation = relations[0];
            relations.RemoveAt(0);

            var relatedEntities = new List<EntityBase>();
            foreach (EntityBase entity in entities)
            {
                foreach (KeyValuePair<string, object> pair in entity.GetRelatedData())
                {
                    if (pair.Key == relation.MappedFieldName)
                    {
                        if (pair.Value is EntityBase)
                        {
                            relatedEntities.Add(pair.Value as EntityBase);
                        }
                        else
                        {
                            var currentRelatedEntities = pair.Value as IEntityCollection;
                            foreach (var currentRelatedEntity in currentRelatedEntities)
                                relatedEntities.Add((EntityBase)currentRelatedEntity);
                        }
                    }
                }
            }
            return GetRelatedEntities(relatedEntities, relations);
        }

        #endregion

    }
}
