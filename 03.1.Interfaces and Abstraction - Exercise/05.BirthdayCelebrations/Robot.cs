using System;

namespace BirthdayCelebrations
{
    public class Robot : IIdentifiable
    {
        private string model;
        private string id;

        public Robot(string id)
        {
            this.id = id;
        }

        public Robot(string model, string id) : this(id)
        {
            this.model = model;
        }

        public string Model => model;
        public string Id => id;
    }
}
