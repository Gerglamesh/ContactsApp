using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace ContactsApp
{
    public static class ContactsIO
    {
        private static BinaryFormatter formatter = new BinaryFormatter();
        private const string DATA_FILENAME = "ContactsInformation.dat";

        /// <summary>
        /// Save this sessions contactsDictionary to file.
        /// </summary>
        public static void Save(Dictionary<string, Person> contactsDictionary)
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
        public static Dictionary<string, Person> Load()
        {
            Dictionary<string, Person> contacts = new Dictionary<string, Person>();

            //Check if we have previously saved information
            if (File.Exists(DATA_FILENAME))
            {
                try
                {
                    //Create a FileStream will gain read access to the data file.
                    FileStream readerFileStream = new FileStream(DATA_FILENAME, FileMode.Open, FileAccess.Read);

                    //Reconstruct information of our contacts from file.
                    contacts = (Dictionary<String, Person>)formatter.Deserialize(readerFileStream);

                    //Close the readerFileStream when we are done
                    readerFileStream.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"There was a problem with retrieving contacts information! \n\n{e}");
                }
            }
            else
            {
                MessageBox.Show($"No contacts file with correct name exists!");
            }

            return contacts;
        }
    }
}
