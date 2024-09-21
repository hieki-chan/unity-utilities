using System.Linq;
using System.Reflection;

namespace Hieki.Utils
{
    public static class ReflectionExtensions
    {
        public static void SetField<V>(this object target, V value, string fieldName = null)
        {
            FieldInfo info = target.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.FieldType == typeof(V) && (string.IsNullOrEmpty(fieldName) ? true : f.Name == fieldName))
                .FirstOrDefault();

            info?.SetValue(target, value);
        }

        public static V GetField<V>(this object target, string fieldName = null)
        {
            FieldInfo info = target.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.FieldType == typeof(V) && (string.IsNullOrEmpty(fieldName) ? true : f.Name == fieldName))
                .FirstOrDefault();

            if (info == null)
                return default;

            return (V)(info.GetValue(target));
        }

        public static bool SetField(this object target, object value, string fieldName, Type baseType = null)
        {
            Type type = target.GetType();
            FieldInfo[] fields = null;

            if(baseType != null)
            {
                while (type != null)
                {
                    if (type == baseType)
                    {
                        fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);

                        break;
                    }
                    type = type.BaseType;
                }
            }
            else
                fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);

            //Debug.Log(type);

            if (fields == null)
                return false;

            foreach (var field in fields)
            {
                if (fieldName == field.Name)
                {
                    field.SetValue(target, value);
                    return true;
                }
            }

            return false;
        }

        public static bool SetField<T>(this object target, object value, string fieldName)
        {
            return SetField(target, value, fieldName, typeof(T));
        }
    }
}
