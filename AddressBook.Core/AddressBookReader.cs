using AddressBook;
using System;
using System.Collections;
using System.IO;
using System.Text;

namespace AddressBook
{
    /// <summary>
    /// Reads Address Book Data from a specified file
    /// </summary>
    public class AddressBookReader
    {
        /// <summary>
        /// Read an Address Book File
        /// </summary>
        /// <param name="filePath">The Path to the .bin file</param>
        public AddressBookData ReadFile(string filePath)
        {
            var data = new AddressBookData();
            using (FileStream fs = File.OpenRead(filePath))
            {
                if (!fs.CanRead) throw new ArgumentException("File cannot be read");
                if (fs.Length <= 0) throw new ArgumentException("Empty file");

                var loop = (fs.Length - 8) / 146;

                fs.Position = 0;

               using (BinaryReader reader = new BinaryReader(fs, Encoding.ASCII))
                {
                    var exportDate = reader.Read<int>(4);
                    var items = reader.Read<int>(4);
                    if (items != loop) throw new ArgumentOutOfRangeException("File Header number of items does not correlate with actual number of items in body");
                    if ((items * 146) + 8 != fs.Length) throw new EndOfStreamException("number of items do not correlate with file size of 146 bytes per item record");
                    for (var i = 0; i < loop; i++)
                    {
                        var contact = new Contact();
                        contact.ContactId = i;
                        contact.Surname = reader.Read<string>(16);
                        contact.Forename = reader.Read<string>(16);
                        var flag = new BitArray(new byte[] { reader.Read<byte>(1) });
                        contact.IsFavourite = flag[0];
                        contact.Gender = flag[1] ? Gender.Female : Gender.Male;
                        contact.Group = flag[2] ? ContactGroup.Work : ContactGroup.Personal;
                        contact.IsRecent = flag[3];
                        contact.PhoneIntlDialCode = reader.Read<int>(1);
                        contact.PhoneNumber = reader.Read<string>(16);
                        contact.EmailAddress = reader.Read<string>(64);
                        contact.TwitterId = reader.Read<string>(32);
                        data.Contacts.Add(contact);
                    }
                }

                // TODO: Perform your import here!
                // HINT: See BinaryReader
                // HINT: Use data.Contacts.Add(...) to insert a Contact
                // HINT: Add some error checking
            }
            return data;
        }

        //internal 
    }
}
