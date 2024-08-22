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
    }
}
