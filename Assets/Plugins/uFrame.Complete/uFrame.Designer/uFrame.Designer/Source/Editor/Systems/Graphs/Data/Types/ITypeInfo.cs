using System;
using System.Collections.Generic;
using uFrame.Editor.Core;
using uFrame.Editor.Database.Data;

namespace uFrame.Editor.Graphs.Data.Types
{
    public interface ITypeInfo : IItem, IValueItem
    {
        bool IsArray { get; }
        bool IsList { get; }
        bool IsEnum { get; }
        ITypeInfo InnerType { get; }
        string TypeName { get; }
        string FullName { get; }
        string Namespace { get; }
        IEnumerable<IMemberInfo> GetMembers();
        bool IsAssignableTo(ITypeInfo info);
        ITypeInfo BaseTypeInfo { get; }

        bool HasAttribute(Type attribute);
    }

    public static class TypeInfoExtensions
    {
        public static IEnumerable<IMemberInfo> GetAllMembers(this ITypeInfo typeInfo)
        {
            if (typeInfo.FullName == typeof(void).FullName) yield break;
            foreach (var item in typeInfo.GetMembers())
            {
                yield return item;
            }
            var baseType = typeInfo.BaseTypeInfo;

            if (baseType != null && baseType != typeInfo)
            {
                foreach (var item in baseType.GetAllMembers())
                {
                    yield return item;
                }
            }

        }
    }
}