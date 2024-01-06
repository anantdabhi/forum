﻿// ***********************************************************************
// <copyright file="DefaultTypeMap.cs" company="ServiceStack, Inc.">
//     Copyright (c) ServiceStack, Inc. All Rights Reserved.
// </copyright>
// <summary>Fork for YetAnotherForum.NET, Licensed under the Apache License, Version 2.0</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceStack.OrmLite.Dapper;

/// <summary>
/// Represents default type mapping strategy used by Dapper
/// </summary>
public sealed class DefaultTypeMap : SqlMapper.ITypeMap
{
    /// <summary>
    /// The fields
    /// </summary>
    private readonly List<FieldInfo> _fields;
    /// <summary>
    /// The type
    /// </summary>
    private readonly Type _type;

    /// <summary>
    /// Creates default type map
    /// </summary>
    /// <param name="type">Entity type</param>
    /// <exception cref="System.ArgumentNullException">type</exception>
    public DefaultTypeMap(Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        _fields = GetSettableFields(type);
        Properties = GetSettableProps(type);
        _type = type;
    }

    /// <summary>
    /// Gets the property setter.
    /// </summary>
    /// <param name="propertyInfo">The property information.</param>
    /// <param name="type">The type.</param>
    /// <returns>MethodInfo.</returns>
    static internal MethodInfo GetPropertySetter(PropertyInfo propertyInfo, Type type)
    {
        if (propertyInfo.DeclaringType == type)
        {
            return propertyInfo.GetSetMethod(true);
        }

        return propertyInfo.DeclaringType.GetProperty(
            propertyInfo.Name,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
            Type.DefaultBinder,
            propertyInfo.PropertyType,
            propertyInfo.GetIndexParameters().Select(p => p.ParameterType).ToArray(),
            null).GetSetMethod(true);
    }

    /// <summary>
    /// Gets the settable props.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>List&lt;PropertyInfo&gt;.</returns>
    static internal List<PropertyInfo> GetSettableProps(Type t)
    {
        return t
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(p => GetPropertySetter(p, t) != null)
            .ToList();
    }

    /// <summary>
    /// Gets the settable fields.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>List&lt;FieldInfo&gt;.</returns>
    static internal List<FieldInfo> GetSettableFields(Type t)
    {
        return [.. t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)];
    }

    /// <summary>
    /// Finds best constructor
    /// </summary>
    /// <param name="names">DataReader column names</param>
    /// <param name="types">DataReader column types</param>
    /// <returns>Matching constructor or default one</returns>
    public ConstructorInfo FindConstructor(string[] names, Type[] types)
    {
        var constructors = _type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (ConstructorInfo ctor in constructors.OrderBy(c => c.IsPublic ? 0 : c.IsPrivate ? 2 : 1).ThenBy(c => c.GetParameters().Length))
        {
            ParameterInfo[] ctorParameters = ctor.GetParameters();
            if (ctorParameters.Length == 0)
            {
                return ctor;
            }

            if (ctorParameters.Length != types.Length)
            {
                continue;
            }

            int i = 0;
            for (; i < ctorParameters.Length; i++)
            {
                if (!string.Equals(ctorParameters[i].Name, names[i], StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (types[i] == typeof(byte[]) && ctorParameters[i].ParameterType.FullName == SqlMapper.LinqBinary)
                {
                    continue;
                }

                var unboxedType = Nullable.GetUnderlyingType(ctorParameters[i].ParameterType) ?? ctorParameters[i].ParameterType;
                if (unboxedType != types[i] && !SqlMapper.HasTypeHandler(unboxedType)
                                            && !(unboxedType.IsEnum && Enum.GetUnderlyingType(unboxedType) == types[i])
                                            && !(unboxedType == typeof(char) && types[i] == typeof(string))
                                            && !(unboxedType.IsEnum && types[i] == typeof(string)))
                {
                    break;
                }
            }

            if (i == ctorParameters.Length)
            {
                return ctor;
            }
        }

        return null;
    }

    /// <summary>
    /// Returns the constructor, if any, that has the ExplicitConstructorAttribute on it.
    /// </summary>
    /// <returns>ConstructorInfo.</returns>
    public ConstructorInfo FindExplicitConstructor()
    {
        var constructors = _type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var withAttr = constructors.Where(c => c.GetCustomAttributes(typeof(ExplicitConstructorAttribute), true).Length > 0).ToList();

        if (withAttr.Count == 1)
        {
            return withAttr[0];
        }

        return null;
    }

    /// <summary>
    /// Gets mapping for constructor parameter
    /// </summary>
    /// <param name="constructor">Constructor to resolve</param>
    /// <param name="columnName">DataReader column name</param>
    /// <returns>Mapping implementation</returns>
    public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
    {
        var parameters = constructor.GetParameters();

        return new SimpleMemberMap(columnName, parameters.FirstOrDefault(p => string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase)));
    }

    /// <summary>
    /// Gets member mapping for column
    /// </summary>
    /// <param name="columnName">DataReader column name</param>
    /// <returns>Mapping implementation</returns>
    public SqlMapper.IMemberMap GetMember(string columnName)
    {
        var property = Properties.Find(p => string.Equals(p.Name, columnName, StringComparison.Ordinal))
                       ?? Properties.Find(p => string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));

        if (property == null && MatchNamesWithUnderscores)
        {
            property = Properties.Find(p => string.Equals(p.Name, columnName.Replace("_", ""), StringComparison.Ordinal))
                       ?? Properties.Find(p => string.Equals(p.Name, columnName.Replace("_", ""), StringComparison.OrdinalIgnoreCase));
        }

        if (property != null)
        {
            return new SimpleMemberMap(columnName, property);
        }

        // roslyn automatically implemented properties, in particular for get-only properties: <{Name}>k__BackingField;
        var backingFieldName = "<" + columnName + ">k__BackingField";

        // preference order is:
        // exact match over underscre match, exact case over wrong case, backing fields over regular fields, match-inc-underscores over match-exc-underscores
        var field = _fields.Find(p => string.Equals(p.Name, columnName, StringComparison.Ordinal))
                    ?? _fields.Find(p => string.Equals(p.Name, backingFieldName, StringComparison.Ordinal))
                    ?? _fields.Find(p => string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase))
                    ?? _fields.Find(p => string.Equals(p.Name, backingFieldName, StringComparison.OrdinalIgnoreCase));

        if (field == null && MatchNamesWithUnderscores)
        {
            var effectiveColumnName = columnName.Replace("_", "");
            backingFieldName = "<" + effectiveColumnName + ">k__BackingField";

            field = _fields.Find(p => string.Equals(p.Name, effectiveColumnName, StringComparison.Ordinal))
                    ?? _fields.Find(p => string.Equals(p.Name, backingFieldName, StringComparison.Ordinal))
                    ?? _fields.Find(p => string.Equals(p.Name, effectiveColumnName, StringComparison.OrdinalIgnoreCase))
                    ?? _fields.Find(p => string.Equals(p.Name, backingFieldName, StringComparison.OrdinalIgnoreCase));
        }

        if (field != null)
        {
            return new SimpleMemberMap(columnName, field);
        }

        return null;
    }
    /// <summary>
    /// Should column names like User_Id be allowed to match properties/fields like UserId ?
    /// </summary>
    /// <value><c>true</c> if [match names with underscores]; otherwise, <c>false</c>.</value>
    public static bool MatchNamesWithUnderscores { get; set; }

    /// <summary>
    /// The settable properties for this typemap
    /// </summary>
    /// <value>The properties.</value>
    public List<PropertyInfo> Properties { get; }
}