using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition
{
    public static class Tools
    {
        public static string GetWorkflowName(Type type)
        {
            return type.IsGenericType ? FriendlyTypeName(type.GetGenericArguments()[0]) : throw new InvalidOperationException();
        }

        public static string FriendlyTypeName(Type t)
        {
            if (t.IsGenericTypeDefinition)
                throw new InvalidOperationException();

            if (!t.IsGenericType)
            {
                return t.Name;
            }
            var b = new StringBuilder();
            b.Append(t.Name).Append("[");
            foreach (var v in t.GetGenericArguments())
            {
                var name = FriendlyTypeName(v);
                b.Append(name).Append(", ");
            }
            b.Length -= 2;
            b.Append("]");
            return b.ToString();
        }
    }
}
