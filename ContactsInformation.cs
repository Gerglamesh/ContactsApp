using System;
using System.Collections.Generic;
using System.Windows;

namespace ContactsApp
{
    public static class ContactsInformation
    {
        public static Dictionary<string, Person> ContactsDictionary { get; set; } = new Dictionary<string, Person>();

        /// <summary>
        /// Returns specific Person based on their name. Returns null of Person not found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Person GetContact(string name)
        {
            if (ContactsDictionary.ContainsKey(name))
            {
                return ContactsDictionary[name];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns a persons general and contact information based on name. Prints error message if not found and returns "Not found".
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetContactInfo(string name)
        {
            try
            {
                Person contact = GetContact(name);
                string result;
                result = $"Name: {contact.FirstName} {contact.LastName}\n";
                result += $"Birth date: {contact.BirthDate:yyyy/MM/dd}\n";
                result += $"Phone: {contact.PhoneNumber}\n\n";
                result += "Adress:\n";
                result += $"{contact.Address.Street} {contact.Address.HouseNumber}\n";
                result += $"{contact.Address.ZipCode} {contact.Address.City}\n";
                result += contact.Address.Country;

                return result;
            }
            catch (Exception)
            {
                MessageBox.Show("Contact does not exist in dictionary. Try another name.");
            }

            return "Not found";
        }

        public static void AddContact(Person person)
        {
            //Check that contact does not allready exist
            if (ContactsDictionary.ContainsKey($"{person.FirstName} {person.LastName}"))
            {
                MessageBoxResult result = MessageBox.Show($"Contact called {person.FirstName} {person.LastName} allready exists!\nDo you wish to overwrite it?", 
                    "Contact Exists!", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    RemoveContact(person.ToString(), false);
                }            
            }

            ContactsDictionary.Add($"{person.FirstName} {person.LastName}", person);
            MessageBox.Show($"Contact {person.FirstName} {person.LastName} added successfully.");
        }

        public static void RemoveContact(string name, bool showSuccessMessage = true)
        {
            if (!ContactsDictionary.ContainsKey(name))
            {
                MessageBox.Show(name + " does not exist.");                
            }
            else
            {
                if (ContactsDictionary.Remove(name))
                {
                    if (showSuccessMessage)
                    {
                        MessageBox.Show(name + " has been removed successfully.");
                    }                    
                }
                else
                {
                    MessageBox.Show("Unable to remove " + name);
                }
            }
        }
    }
}
