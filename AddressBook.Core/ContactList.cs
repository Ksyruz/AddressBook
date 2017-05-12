using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook
{
    /// <summary>
    /// A Collection of Contacts
    /// </summary>
    public class ContactList : IEnumerable<Contact>
    {
        /// <summary>
        /// Internal items
        /// </summary>
        private List<Contact> _items = new List<Contact>();

        /// <summary>
        /// Gets the number of items in the list
        /// </summary>
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        /// <summary>
        /// Clear all items from this list
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        /// <summary>
        /// Gets the Contact with the provided ID
        /// </summary>
        /// <param name="contactId">The ID of the Contact to return</param>
        /// <returns>Contact</returns>
        public Contact this[int contactId]
        {
            get
            {
                Contact contact = _items.FirstOrDefault(c => c.ContactId == contactId);
                
                if (contact == null)
                    throw new IndexOutOfRangeException("Invalid ContactId");

                return contact;
            }
        }

        /// <summary>
        /// Insert a new contact into the list
        /// </summary>
        /// <param name="contact">The Contact to add</param>
        /// <remarks>
        /// The ContactId is updated to reflect the new ID
        /// </remarks>
        public void Add(Contact contact)
        {
            // Get the next available ID
            int newId = 0;
            if (_items.Count > 0)
                newId = _items.Max(c => c.ContactId) + 1;
            
            // Set the new ID
            contact.ContactId = newId;

            _items.Add(contact);
        }

        /// <summary>
        /// Remove a contact from the list
        /// </summary>
        /// <param name="contactId">The ID of the contact to remove</param>
        public void Remove(int contactId)
        {
            Contact contact = this[contactId];
            _items.Remove(contact);
            contact.ContactId = -1;
        }

        #region IEnumerable Implementation

        /// <summary>
        /// Returns a strongly-typed enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Contact> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Returns a weakly-typed enumerator
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

    }
}
