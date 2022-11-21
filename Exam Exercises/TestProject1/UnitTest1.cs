namespace TestProject1
{
    using System;
    using BookingApp;
    using System.Linq;
    using NUnit.Framework;
    using System.Reflection;
    using System.Collections.Generic;

    public class Tests_000_002
    {
        private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

        [Test]
        public void UploadRoomTypesWorkProperlyWithAllRoomTypes()
        {
            var controller = CreateObjectInstance(GetType("Controller"));

            var hotelArguments = new object[] { "Alpine Slopes", 4 };
            InvokeMethod(controller, "AddHotel", hotelArguments);

            var roomArgumentsOne = new object[] { "Alpine Slopes", "Apartment" };
            var validActualResultOne = InvokeMethod(controller, "UploadRoomTypes", roomArgumentsOne);
            var roomArgumentsTwo = new object[] { "Alpine Slopes", "Studio" };
            var validActualResultTwo = InvokeMethod(controller, "UploadRoomTypes", roomArgumentsTwo);
            var roomArgumentsFour = new object[] { "Alpine Slopes", "DoubleBed" };
            var validActualResultFour = InvokeMethod(controller, "UploadRoomTypes", roomArgumentsFour);

            var validExpectedResultOne = "Successfully added Apartment room type in Alpine Slopes hotel!";
            var validExpectedResultTwo = "Successfully added Studio room type in Alpine Slopes hotel!";
            var validExpectedResultFour = "Successfully added DoubleBed room type in Alpine Slopes hotel!";

            Assert.AreEqual(validExpectedResultOne, validActualResultOne);
            Assert.AreEqual(validExpectedResultTwo, validActualResultTwo);
            Assert.AreEqual(validExpectedResultFour, validActualResultFour);
        }

        private static object InvokeMethod(object obj, string methodName, object[] parameters)
        {
            try
            {
                var result = obj.GetType()
                    .GetMethod(methodName)
                    .Invoke(obj, parameters);

                return result;
            }
            catch (TargetInvocationException e)
            {
                return e.InnerException.Message;
            }
        }

        private static object CreateObjectInstance(Type type, params object[] parameters)
        {
            try
            {
                var desiredConstructor = type.GetConstructors()
                    .FirstOrDefault(x => x.GetParameters().Any());

                if (desiredConstructor == null)
                {
                    return Activator.CreateInstance(type, parameters);
                }

                var instances = new List<object>();

                foreach (var parameterInfo in desiredConstructor.GetParameters())
                {
                    var currentInstance = Activator.CreateInstance(GetType(parameterInfo.Name.Substring(1)));

                    instances.Add(currentInstance);
                }

                return Activator.CreateInstance(type, instances.ToArray());
            }
            catch (TargetInvocationException e)
            {
                return e.InnerException.Message;
            }
        }

        private static Type GetType(string name)
        {
            var type = ProjectAssembly
                .GetTypes()
                .FirstOrDefault(t => t.Name.Contains(name));

            return type;
        }
    }
}