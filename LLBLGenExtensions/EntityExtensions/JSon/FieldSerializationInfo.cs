using System;
using LLBLGenExtensions.EntityExtensions.Searching;
using LLBLGenExtensions.ExtensionMethods;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace LLBLGenExtensions.EntityExtensions.JSon
{
    public class FieldSerializationInfo : ISerializationInfo
    {
        public IEntityField Field;

        public FieldSerializationInfo(IEntityField field)
        {
            this.Field = field;
        }

        public override string Name
        {
            get { return this.Field.Name; }
        }

        public override string GetJSon(EntityBase entity)
        {
            var value = entity.Get(this.Field);
            if (this.Field.DataType == typeof(String))
                return String.Format("\"{0}\"", value.EscapeForJsonization());
            if (this.Field.DataType == typeof(int?) && String.IsNullOrEmpty(value))
                return "null";
            return value;
        }
    }
}
