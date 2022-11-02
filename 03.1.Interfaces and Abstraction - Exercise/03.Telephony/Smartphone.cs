using System;
using System.Linq;

namespace _03.Telephony
{
    public class Smartphone : Phone
    {

        public void Browse(string webAddress)
        {
            if (webAddress.Any(ch => char.IsDigit(ch)))
            {
                throw new ArgumentException("Invalid URL!");
            }

            Console.WriteLine($"Browsing: {webAddress}!");
        }
        

        public override void CallNumber(string phoneNumber)
        {
            base.CallNumber(phoneNumber);

            if (phoneNumber.Length == 10)
            {
                Console.WriteLine($"Calling... {phoneNumber}");
            }            
        }
    }
}
