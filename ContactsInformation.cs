﻿using System;
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
    public static class ContactsInformation
    {
        private static Dictionary<string, Person> contactsDictionary = new Dictionary<string, Person>();
        private static BinaryFormatter formatter = new BinaryFormatter();

        private const string DATA_FILENAME = "ContactsInformation.dat";

        public static Dictionary<string, Person> GetContacts() => contactsDictionary;

        /// <summary>
        /// Returns specific Person based on their name. Returns null of Person not found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Person GetContact(string name)
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
                result += $"Birthd date: {contact.BirthDate.ToString("yyyy/MM/dd")}\n";
                result += $"Phone: {contact.PhoneNumber}\n\n";
                result += "Adress:\n";
                result += $"{contact.Address.street} {contact.Address.houseNumber}\n";
                result += $"{contact.Address.zipCode} {contact.Address.city}\n";
                result += contact.Address.country;

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
            if (contactsDictionary.ContainsKey($"{person.FirstName} {person.LastName}"))
            {
                MessageBoxResult result = MessageBox.Show($"Contact called {person.FirstName} {person.LastName} allready exists!\nDo you wish to overwrite it?", 
                    "Contact Exists!", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    RemoveContact(person.ToString(), false);
                }            
            }

            contactsDictionary.Add($"{person.FirstName} {person.LastName}", person);
            MessageBox.Show($"Contact {person.FirstName} {person.LastName} added successfully.");
        }

        public static void RemoveContact(string name, bool showSuccessMessage = true)
        {
            if (!contactsDictionary.ContainsKey(name))
            {
                MessageBox.Show(name + " does not exist.");                
            }
            else
            {
                if (contactsDictionary.Remove(name))
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

        /// <summary>
        /// Save this sessions contactsDictionary to file.
        /// </summary>
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

        /// <summary>
        /// Load this sessions contactsDictionary from file.
        /// </summary>
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
                    contactsDictionary = (Dictionary<String, Person>)formatter.Deserialize(readerFileStream);

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