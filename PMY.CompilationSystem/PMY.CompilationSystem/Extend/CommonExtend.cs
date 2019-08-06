using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMY.CompilationSystem.Extend
{
    public static class CommonExtend
    {
        /// <summary>
        /// 判断对象是否是Enumber类型的
        /// </summary>
        /// <param name="enumerableType"></param>
        /// <returns></returns>
        public static bool IsEnumerableType(this Type enumerableType)
        {
            return (FindGenericType(typeof(IEnumerable<>), enumerableType) != null);
        }
        public static Type FindGenericType(this Type definition, Type type)
        {
            while ((type != null) && (type != typeof(object)))
            {
                if (type.IsGenericType && (type.GetGenericTypeDefinition() == definition))
                {
                    return type;
                }
                if (definition.IsInterface)
                {
                    foreach (Type type2 in type.GetInterfaces())
                    {
                        Type type3 = FindGenericType(definition, type2);
                        if (type3 != null)
                        {
                            return type3;
                        }
                    }
                }
                type = type.BaseType;
            }
            return null;
        }
    }
}