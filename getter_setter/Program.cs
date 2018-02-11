using System;

namespace getter_setter
{
    class Program
    {
        // some public property of that class. Anyone can access/modify it
        public string someProperty1 = "test";

        // some custom getter and setter implementations for a property
        private string _someProperty2;
        public string someProperty2
        {
            get
            {
                return this._someProperty2;
            }

            // getter and setter of the same property can have different accessors
            private set
            {
                this._someProperty2 = value;
            }
        }

        // an auto-implemented property. **This is equivalent to someProperty2**,
        // but when the need arises to have a custom getter/setter for that property
        // we can expand it with implementation
        public string someProperty3 { get; private set; }
    }
}
