using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp
{
    [Serializable]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; } = new DateTime(1001, 01, 01);
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }

        public Person()
        {
            this.BirthDate = new DateTime(1001, 01, 01);
            Address = new Address();
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
