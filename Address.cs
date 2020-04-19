using System;

namespace ContactsApp
{
    [Serializable]
    public class Address
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
