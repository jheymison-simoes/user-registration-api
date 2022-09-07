using System.ComponentModel;
using System.Reflection;
using System.Resources;
using UserRegister.Business.Exceptions;

namespace UserRegister.Business.Utils;

public static class StringHelpers
{
    public static string GetResourceFormat(this ResourceSet resourceSet, string getString, params object[] args)
    {
        return args.Length == 0 
            ? resourceSet.GetString(getString)! 
            : string.Format(resourceSet.GetString(getString)!, args);
    }
    
    public static string ResourceFormat(this string message, params object[] args)
    {
        return string.Format(message, args);
    }
    
    public static string GetEnumDescription(this Enum enumValue)
    {
        try
        {
            var attribute = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

}