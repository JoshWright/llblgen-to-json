using SD.LLBLGen.Pro.ORMSupportClasses;

namespace LLBLGenExtensions.EntityExtensions.JSon
{
    public abstract class ISerializationInfo
    {
        public abstract string Name { get; }
        public abstract string GetJSon(EntityBase entity);

        public static implicit operator ISerializationInfo(EntityField field)
        {
            return new FieldSerializationInfo(field);
        }
    }
}
