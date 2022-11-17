using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;

namespace Stealer
{
    public class Spy
    {

        public string StealFieldInfo(string className, params string[] fieldsName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Class under investigation: {className}");

            Type type = Type.GetType(className);

            var fieldsInfo = type.GetFields(BindingFlags.Instance | BindingFlags.Public
                                              | BindingFlags.Static | BindingFlags.NonPublic);

            var instanceOfClass = Activator.CreateInstance(type);

            foreach (var field in fieldsInfo.Where(f => fieldsName.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(instanceOfClass)}");
            }

            return sb.ToString().TrimEnd();
        }

        public string AnalyzeAccessModifiers(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type type = Type.GetType(className);
            var fieldsInfo = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

            foreach (var field in fieldsInfo)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            var methodsInfo = type.GetMethods(BindingFlags.Instance | BindingFlags.Public
                                             | BindingFlags.Static | BindingFlags.NonPublic);

            foreach (var method in methodsInfo)
            {
                if (method.Name.StartsWith("get") && !method.IsPublic)
                {
                    sb.AppendLine($"{method.Name} have to be public!");
                }
                else if (method.Name.StartsWith("set") && !method.IsPrivate)
                {
                    sb.AppendLine($"{method.Name} have to be private!");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string RevealPrivateMethods(string className)
        {
            StringBuilder sb = new StringBuilder();
            Type type = Type.GetType(className);
            var methodsInfo = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            sb.AppendLine($"All Private Methods of Class: {className}");
            sb.AppendLine($"Base Class: {type.BaseType.Name}");

            foreach (var method in methodsInfo)
            {
                sb.AppendLine(method.Name);
            }

            return sb.ToString().TrimEnd();
        }

        public string CollectGettersAndSetters(string className)
        {
            StringBuilder sb = new StringBuilder();
            Type type = Type.GetType(className);
            var methodsInfo = type.GetMethods(BindingFlags.Instance | BindingFlags.Public
                                             | BindingFlags.Static | BindingFlags.NonPublic);

            foreach (var method in methodsInfo)
            {
                if (method.Name.StartsWith("get"))
                {
                    sb.AppendLine($"{method.Name} will return {method.ReturnType}");
                }
            }

            foreach (var method in methodsInfo)
            {
                if (method.Name.StartsWith("set"))
                {
                    string parameterType = method.GetParameters()[0].ParameterType.FullName;
                    sb.AppendLine($"{method.Name} will set field of {parameterType}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
