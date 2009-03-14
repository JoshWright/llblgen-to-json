using System.Collections.Generic;
using LLBLGenExtensions.EntityExtensions.Searching;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace LLBLGenExtensions.EntityExtensions.JSon
{
    public class EntitySerializationInfo : ISerializationInfo
    {
        public IEntityRelation Relation;
        public List<ISerializationInfo> SubInfo;

        public EntitySerializationInfo(IEntityRelation relation, List<ISerializationInfo> infos)
        {
            this.Relation = relation;
            this.SubInfo = infos;
        }

        public override string Name
        {
            get { return this.Relation.MappedFieldName; }
        }

        public override string GetJSon(EntityBase entity)
        {
            if (this.Relation.TypeOfRelation == RelationType.ManyToOne || this.Relation.TypeOfRelation == RelationType.OneToOne)
            {
                EntityBase subEntity = entity.GetRelatedEntity(this.Relation);
                return subEntity.ToJSon(this.SubInfo);
            }
            else
            {
                List<EntityBase> relatedEntities = entity.GetRelatedEntities(this.Relation);
                return relatedEntities.ToJSon(this.SubInfo);
            }
        }
    }
}
