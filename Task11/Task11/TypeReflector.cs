using System.Collections;
using System.Reflection;

namespace Task11;

public class TypeReflector
{
    private readonly Type _type;
    private readonly MemberTypesToShow _memberTypes;

    public TypeReflector(Type type, MemberTypesToShow memberTypes)
    {
        _type = type ?? throw new ArgumentNullException(nameof(type));
        _memberTypes = memberTypes;
    }

    public string GetReflectionInfo()
    {
        IList info = new List<string>
        {
            $"Тип: {_type.Name}"
        };
        object[] typeAttrs = _type.GetCustomAttributes(false);
        if (typeAttrs.Length > 0)
        {
            _ = info.Add("Атрибуты типа:");
            foreach (object attr in typeAttrs)
            {
                _ = info.Add($"- {attr.GetType().Name}");
            }
        }

        if (_memberTypes.HasFlag(MemberTypesToShow.Methods))
        {
            _ = info.Add("Методы:");
            foreach (MethodInfo method in _type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                _ = info.Add($"- {method.Name}");
                object[] methodAttrs = method.GetCustomAttributes(false);
                foreach (object attr in methodAttrs)
                {
                    _ = info.Add($"  Атрибут: {attr.GetType().Name}");
                }
            }
        }

        if (_memberTypes.HasFlag(MemberTypesToShow.Properties))
        {
            _ = info.Add("Свойства:");
            foreach (PropertyInfo prop in _type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                _ = info.Add($"- {prop.Name} ({prop.PropertyType.Name})");
                object[] propAttrs = prop.GetCustomAttributes(false);
                foreach (object attr in propAttrs)
                {
                    _ = info.Add($"  Атрибут: {attr.GetType().Name}");
                }
            }
        }

        return string.Join(Environment.NewLine, info);
    }
}
