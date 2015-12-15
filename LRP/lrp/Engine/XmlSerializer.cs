namespace Lrp.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Reflection;

    static class XmlSerializer
    {
        public static void Initialize(Object instance, XmlNode node)
        {
            var type = instance.GetType();

            ValidateName(node, type);

            var nameToProperty = GetProperties(type);
            var nameToValue = GetAttributes(node);

            Initialize(instance, nameToProperty, nameToValue);
        }

        static void ValidateName(XmlNode node, Type type)
        {
            var xmlName = GetAttribute<XmlNameAttribute>(type);
            var name = xmlName != null ? xmlName.Name : type.Name;
            if (name != node.Name)
            {
                var message = string.Format("Incorrect node name = {0} for class = {1}", node.Name, name);
                throw new Exception(message);
            }
        }

        static Dictionary<string, PropertyInfo> GetProperties(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            var result = new Dictionary<string, PropertyInfo>();
            foreach (var element in properties)
            {
                var xmlSerializableAttributeattribute = GetAttribute<XmlSerializableAttribute>(element);
                if (xmlSerializableAttributeattribute == null)
                    continue;

                var xmlNameAttribute = GetAttribute<XmlNameAttribute>(element);
                string name;
                if (xmlNameAttribute != null)
                    name = xmlNameAttribute.Name.ToLower();
                else
                    name = element.Name.ToLower();

                if (result.ContainsKey(name))
                {
                    var message = string.Format("Duplicate property name = {0} for type = {1}", element.Name, type.Name);
                    throw new Exception(message);
                }

                result[name] = element;
            }

            return result;
        }

        static Dictionary<string, string> GetAttributes(XmlNode node)
        {
            var result = new Dictionary<string, string>();

            var attributes = node.Attributes;
            var count = attributes.Count;
            for (var index = 0; index < count; ++index)
            {
                var attribute = attributes[index];
                var name = attribute.Name.ToLower();
                if (result.ContainsKey(name))
                {
                    var message = string.Format("Duplicate property name = {0} for node = {1}", attribute.Name, node.Name);
                    throw new Exception(message);
                }
                result[name] = attribute.Value;
            }
            return result;
        }

        static void Initialize(object instance, Dictionary<string, PropertyInfo> nameToProperty, Dictionary<string, string> nameToValue)
        {
            foreach (var element in nameToProperty)
            {
                string value;
                var property = element.Value;
                if (!nameToValue.TryGetValue(element.Key, out value))
                {
                    if (IsRequired(property))
                    {
                        var message = string.Format("Xml attribute does not exist for name = {0}", property.Name);
                        throw new Exception(message);
                    }
                }
                else
                {
                    Initialize(instance, property, value);
                }
            }

            foreach (var element in nameToValue)
            {
                if (!nameToProperty.ContainsKey(element.Key))
                {
                    var message = string.Format("Unsupported xml attribute name = {0}", element.Key);
                    throw new Exception(message);
                }
            }
        }

        static bool IsRequired(PropertyInfo property)
        {
            var objs = property.GetCustomAttributes(typeof(XmlSerializableAttribute), true);
            if (objs == null || objs.Length != 1)
            {
                var message = string.Format("Invalid attributes number for type = {0}", property.PropertyType.Name);
                throw new ArgumentException(message);
            }
            var attr = (XmlSerializableAttribute)objs[0];
            var result = attr.Required;
            return result;
        }

        static void Initialize(object instance, PropertyInfo property, string value)
        {
            if (property.PropertyType == typeof(string))
                InitializeString(instance, property, value);
            else if (property.PropertyType.IsEnum)
                InitializeEnumeration(instance, property, value);
            else if (property.PropertyType == typeof(bool))
                InitializeBool(instance, property, value);
        }

        static void InitializeString(object instance, PropertyInfo property, string value)
        {
            property.SetValue(instance, value, null);
        }

        static void InitializeBool(object instance, PropertyInfo property, string value)
        {
            bool val = bool.Parse(value);
            property.SetValue(instance, val, null);
        }

        static void InitializeEnumeration(object instance, PropertyInfo property, string value)
        {
            var type = property.PropertyType;

            var fields = type.GetFields();
            for (var index = 1; index < fields.Length; ++index)
            {
                var field = fields[index];
                if (value == field.Name.ToLower())
                {
                    var obj = field.GetValue(null);
                    property.SetValue(instance, obj, null);
                    return;
                }

                var displayName = GetAttribute<XmlNameAttribute>(field);
                if (displayName != null)
                {
                    if (value == displayName.Name.ToLower())
                    {
                        var obj = field.GetValue(null);
                        property.SetValue(instance, obj, null);
                        return;
                    }
                }
            }

            var message = string.Format("Could not deserialize enumeration value = {0}", value);
            throw new Exception(message);
        }

        static T GetAttribute<T>(FieldInfo info)
            where T : class
        {
            var attributes = info.GetCustomAttributes(typeof(T), true);
            return GetAttribute<T>(attributes);
        }

        static T GetAttribute<T>(PropertyInfo property)
            where T : class
        {
            var attributes = property.GetCustomAttributes(typeof(T), true);
            return GetAttribute<T>(attributes);
        }

        static T GetAttribute<T>(Type type)
            where T : class
        {
            var attributes = type.GetCustomAttributes(typeof(T), true);
            return GetAttribute<T>(attributes);
        }

        static T GetAttribute<T>(Object[] attributes)
            where T : class
        {
            if (attributes.Length == 0)
                return null;

            if (attributes.Length > 1)
                throw new Exception("Invalid attributes number");

            var obj = attributes[0];
            var result = (T)obj;
            return result;
        }

    }
}
