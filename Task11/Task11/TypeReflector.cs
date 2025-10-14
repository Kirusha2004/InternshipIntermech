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
        List<string> info =
        [
            $"Тип: {_type.Name}"
        ];
        object[] typeAttrs = _type.GetCustomAttributes(false);
        if (typeAttrs.Length > 0)
        {
            info.Add("Атрибуты типа:");
            foreach (object attr in typeAttrs)
            {
                info.Add($"- {attr.GetType().Name}");
            }
        }

        if (_memberTypes.HasFlag(MemberTypesToShow.Methods))
        {
            info.Add("Методы:");
            foreach (MethodInfo method in _type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                info.Add($"- {method.Name}");
                object[] methodAttrs = method.GetCustomAttributes(false);
                foreach (object attr in methodAttrs)
                {
                    info.Add($"  Атрибут: {attr.GetType().Name}");
                }
            }
        }

        if (_memberTypes.HasFlag(MemberTypesToShow.Properties))
        {
            info.Add("Свойства:");
            foreach (PropertyInfo prop in _type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                info.Add($"- {prop.Name} ({prop.PropertyType.Name})");
                object[] propAttrs = prop.GetCustomAttributes(false);
                foreach (object attr in propAttrs)
                {
                    info.Add($"  Атрибут: {attr.GetType().Name}");
                }
            }
        }

        return string.Join(Environment.NewLine, info);
    }
}
