using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace ContactsApp
{
    [Serializable]
    public class Contact
    {
        public string firstName { get; set; } = "Empty";
        public string lastName { get; set; } = "Empty";
        public DateTime birthDate { get; set; } = new DateTime(1001, 01, 01);
        public string phoneNumber { get; set; } = "Empty";
        public AddressSerializable address { get; set; }

        public Contact()
        {
            address = new AddressSerializable();
        }

        public override string ToString()
        {
            return $"{firstName} {lastName}";
        }
    }

    [Serializable]
    public class AddressSerializable
    {
        public string street { get; set; } = "";
        public string houseNumber { get; set; } = "";
        public int zipCode { get; set; } = 0;
        public string city { get; set; } = "";
        public string country { get; set; } = "";

        public AddressSerializable()
        {

        }
    }

    public static class ContactsInformation
    {
        private static Dictionary<string, Contact> contactsDictionary = new Dictionary<string, Contact>();
        private static BinaryFormatter formatter = new BinaryFormatter();

        private const string DATA_FILENAME = "ContactsInformation.dat";

        public static Dictionary<string, Contact> GetContacts()
        {
            return contactsDictionary;
        }

        public static Contact GetContact(string name)
        {
            if (contactsDictionary.ContainsKey(name))
            {
                return contactsDictionary[name];
            }
            else
            {
                return null;
            }            
        }

        public static string GetContactInfo(string name)
        {
            Contact contact = GetContact(name);
            string result;
            result = $"Name: {contact.firstName} {contact.lastName}\n";
            result += $"Birthd date: {contact.birthDate.ToString("yyyy/MM/dd")}\n";
            result += $"Phone: {contact.phoneNumber}\n\n";
            result += "Adress:\n";
            result += $"{contact.address.street} {contact.address.houseNumber}\n";
            result += $"{contact.address.zipCode} {contact.address.city}\n";
            result += contact.address.country;

            return result;
        }

        public static bool AddContact(Contact contact)
        {
            //Check that contact does not allready exist
            if (contactsDictionary.ContainsKey($"{contact.firstName} {contact.lastName}"))
            {
                MessageBoxResult result = MessageBox.Show($"Contact called {contact.firstName} {contact.lastName} allready exists!\nDo you wish to overwrite it?", 
                    "Contact Exists!", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    RemoveContact(contact.ToString());
                }
                else
                {
                    return false;
                }                
            }

            //Add contact to dictionary
            contactsDictionary.Add($"{contact.firstName} {contact.lastName}", contact);
            MessageBox.Show($"Contact {contact.firstName} {contact.lastName} added successfully.");
            return true;
        }

        public static bool RemoveContact(string name)
        {
            //If we do not have a contact with this name
            if (!contactsDictionary.ContainsKey(name))
            {
                MessageBox.Show(name + " does not exist.");                
            }
            //Else if we have a contact with this name
            else
            {
                if (contactsDictionary.Remove(name))
                {
                    MessageBox.Show(name + " had been removed successfully.");
                    return true;
                }
                else
                {
                    MessageBox.Show("Unable to remove " + name);
                }
            }
            return false;
        }

        public static void Save()
        {
            //Gain code access to the file that we are going to write to
            try
            {
                //Create a FileStream that will write data to file.
                FileStream writerFileStream = new FileStream(DATA_FILENAME, FileMode.Create, FileAccess.Write);

                //Save our dictionary of contacts to file
                formatter.Serialize(writerFileStream, contactsDictionary);

                //Close the writerFileStream when we are done.
                writerFileStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unable to save contacts information! \n\n{e}");
            }
        }

        public static bool Load()
        {
            //Check if we have previously saved information
            if (File.Exists(DATA_FILENAME))
            {
                try
                {
                    //Create a FileStream will gain read access to the data file.
                    FileStream readerFileStream = new FileStream(DATA_FILENAME, FileMode.Open, FileAccess.Read);

                    //Reconstruct information of our contacts from file.
                    contactsDictionary = (Dictionary<String, Contact>)formatter.Deserialize(readerFileStream);

                    //Close the readerFileStream when we are done
                    readerFileStream.Close();

                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show($"There was a problem with retrieving contacts information! \n\n{e}");
                }
            }
            return false;
        }
    }
}
