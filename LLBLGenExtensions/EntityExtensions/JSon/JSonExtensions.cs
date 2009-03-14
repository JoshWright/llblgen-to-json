using System;
using System.Collections.Generic;
using LLBLGenExtensions.EntityExtensions.Searching;
using LLBLGenExtensions.ExtensionMethods;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace LLBLGenExtensions.EntityExtensions.JSon
{

    public static class EntitySerializationInfoExtensions
    {

        #region Related Entity

        public static EntitySerializationInfo With(this IEntityRelation relation, params ISerializationInfo [] infos)
        {
            return new EntitySerializationInfo(relation, new List<ISerializationInfo>(infos));
        }

        #endregion


        #region ToJSon

        public static string ToJSon(this IEntityCollection entityCollection, params ISerializationInfo[] serializationInfos)
        {
            return ToJSon(entityCollection.ToListOfEntityBases(), new List<ISerializationInfo>(serializationInfos));
        }

        public static string ToJSon(this List<EntityBase> entities, List<ISerializationInfo> infos)
        {
            var relatedEntitiesJson = new List<string>();
            foreach (var entity in entities)
                relatedEntitiesJson.Add(entity.ToJSon(infos));
            return relatedEntitiesJson.ToJSon();
        }

        public static string ToJSon(this EntityBase entity, params ISerializationInfo[] infos)
        {
            return ToJSon(entity, new List<ISerializationInfo>(infos));
        }

        public static string ToJSon(this EntityBase entity, List<ISerializationInfo> infos)
        {
            if(infos.Count == 0)
                return "{}";

            var json = "{";
            foreach (var info in infos)
                json += String.Format("\"{0}\":{1},", info.Name.EscapeForJsonization(), info.GetJSon(entity));
            return json.Substring(0, json.Length-1) + "}";
        }

        #endregion

    }

}
