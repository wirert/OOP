using System;
using System.Linq;
using System.Reflection;
using System.Text;

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
    }
}
