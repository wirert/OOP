using System;


namespace Attributes
{
    [AttributeUsage(AttributeTargets.Method 
        | AttributeTargets.Parameter 
        | AttributeTargets.Class, AllowMultiple = true)]

    public class AuthorAttribute : Attribute
    {
        public AuthorAttribute()
        { }

        public AuthorAttribute(string name)
        {
            Name= name;
        }

        public string Name { get; set; }
    }
}
