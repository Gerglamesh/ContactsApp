using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp
{
    [Serializable]
    public class Address
    {
        public string street { get; set; }
        public string houseNumber { get; set; }
        public int zipCode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
}
