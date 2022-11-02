using System;
using System.IO;

namespace _03.Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] phoneNumbers = Console.ReadLine().Split(' ');

            foreach (var phoneNumber in phoneNumbers)
            {
                Smartphone phone = new Smartphone();
                try
                {                    
                    phone.CallNumber(phoneNumber);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }                
            }

            string[] webAddresses = Console.ReadLine().Split(' ');

            foreach (var webAddress in webAddresses)
            {
                Smartphone phone = new Smartphone();
                try
                {
                    phone.Browse(webAddress);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
        }
    }
}
