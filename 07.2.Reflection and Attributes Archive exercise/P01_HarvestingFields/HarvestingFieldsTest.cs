namespace P01_HarvestingFields
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            HarvestingFields harvestingFields = new HarvestingFields();
            
            string command;

            while ((command = Console.ReadLine()) != "HARVEST")
            {
                FieldInfo[] fields = harvestingFields.GetType().GetFields((BindingFlags)60);

                switch (command)
                {
                    case "private":
                        fields = fields.Where(p => p.IsPrivate).ToArray();
                        break;
                    case "protected":
                        fields = fields.Where(p => p.IsFamily).ToArray();
                        break;
                    case "public":
                        fields = fields.Where(p => p.IsPublic).ToArray();
                        break;
                    case "all":
                        break;
                    default:
                        throw new InvalidOperationException("Invalid modifier!");
                }

                foreach (var field in fields)
                {
                    string accModifier = field.Attributes.ToString().ToLower();

                    if (accModifier == "family")
                    {
                        accModifier = "protected";
                    }

                    Console.WriteLine($"{accModifier} {field.FieldType.Name} {field.Name}");
                }
            }
        }
    }
}
