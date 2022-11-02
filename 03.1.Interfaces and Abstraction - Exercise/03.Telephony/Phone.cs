using System;
using System.Linq;

namespace _03.Telephony
{
     public class Phone
    {
        public virtual void CallNumber(string phoneNumber)
        {
            if (phoneNumber.Any(ch => !char.IsDigit(ch)))
            {
                throw new ArgumentException("Invalid number!");
            }

            if (phoneNumber.Length == 7)
            {
                Console.WriteLine($"Dialing... {phoneNumber}");
            }
        }
    }
}
