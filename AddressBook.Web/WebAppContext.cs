using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddressBook.Web
{
    /// <summary>
    /// Provides access to shared app resources
    /// </summary>
    public static class WebAppContext
    {
        /// <summary>
        /// Gets the loaded AddressBook Data
        /// </summary>
        public static AddressBookData AddressBook { get; private set; }

        /// <summary>
        /// Initializes shared app data
        /// </summary>
        /// <param name="dataFilePath"></param>
        public static void Initialize(String dataFilePath)
        {
            var reader = new AddressBookReader();
            AddressBook = reader.ReadFile(dataFilePath);
        }
    }
}