namespace AddressBook
{
    /// <summary>
    /// An Address Book read from a File
    /// </summary>
    public class AddressBookData
    {
        private static ContactList _contacts = null;

        /// <summary>
        /// Gets the Contacts List
        /// </summary>
        public ContactList Contacts
        {
            get
            {
                // Init Contacts Table?
                if (_contacts == null)
                {
                    _contacts = new ContactList();
                }

                return _contacts;
            }
        }

        /// <summary>
        /// Clear all items from this Data Object
        /// </summary>
        public void Clear()
        {
            Contacts.Clear();
        }
    }
}
