namespace ValidationAttributes.Utilities
{
    using Attributes;

    public static class Validator
    {
        public static bool IsValid(object classInstance)
        {            
            var props = classInstance.GetType().GetProperties();

            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(typeof(MyValidationAttribute), true);
                var propValue = prop.GetValue(classInstance);

                foreach (var attr in attrs)
                {
                    var attrMethod = attr.GetType().GetMethod("IsValid");

                    bool isValid = (bool)attrMethod.Invoke(attr, new object[] { propValue });

                    if (!isValid)
                    {
                        return false;
                    }
                }
            }     

            return true;
        }
    }
}
