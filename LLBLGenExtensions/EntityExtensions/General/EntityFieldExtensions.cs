using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace LLBLGenExtensions.EntityExtensions.General
{
    public static class EntityFieldExtensions
    {
        public static IPredicate Contains(this EntityField field, string keyword)
        {
            return field % ("%" + keyword + "%");
        }
    }
}
