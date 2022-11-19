namespace P02_BlackBoxInteger
{
    using System;
    using System.Reflection;
    using System.Text;

    public class BlackBoxIntegerTests
    {
        public static void Main()
        {            
            var blackBoxType = typeof(BlackBoxInteger);
            object blackBoxInstance = Activator.CreateInstance(blackBoxType, true);            
            string command;

            while ((command = Console.ReadLine()) != "END")
            {
                string[] cmdArgs = command.Split('_');

                string methodName = cmdArgs[0];
                int value = int.Parse(cmdArgs[1]);

                Console.WriteLine(TestMethod(blackBoxInstance, methodName, value));
            }
        }

        public static string TestMethod(object classInstance, string methodName, int value)
        {
            var resultValues = new StringBuilder();
            var fields = classInstance.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var method = classInstance.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);

            method.Invoke(classInstance, new object[] { value });

            foreach (var field in fields)
            {
                resultValues.AppendLine(field.GetValue(classInstance).ToString());
            }

            return resultValues.ToString().Trim();
        }
    }
}
