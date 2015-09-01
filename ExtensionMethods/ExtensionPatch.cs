
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(
        AttributeTargets.Assembly
        | AttributeTargets.Class
        | AttributeTargets.Method,
        Inherited = false,
        AllowMultiple = false)
    ]
    public class ExtensionAttribute : Attribute
    {
    }
}
