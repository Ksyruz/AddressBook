using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Tests
{
    [TestFixture(Category = "Address Book Reader", Description = "Tests the AddressBookReader class")]
    public class AddressBookReaderTests
    {
        private AddressBookData _data; // data read from the file

        [OneTimeSetUp]
        public void SetUp()
        {
            var reader = new AddressBookReader();
            _data = reader.ReadFile(__Setup.TestDataPath);
        }

        [Test]
        public void CountOfEntries()
        {
            Assert.AreEqual(__Setup.ReferenceContacts.Length, _data.Contacts.Count, "Incorrect number of Contacts read");
        }

        [Test]
        public void AllContactsMatch()
        {
            var expectedContacts = __Setup.ReferenceContacts;
            var actualContacts = _data.Contacts.ToArray();

            for (int i = 0; i < expectedContacts.Length; i++)
            {
                var expected = expectedContacts[i];
                var actual = actualContacts[i];
                AssertContactsMatch(expected, actual);
            }
        }

        public void AssertContactsMatch(Contact expected, Contact actual)
        {
            Assert.AreEqual(expected.EmailAddress, actual.EmailAddress, "Email Address mismatch");
            Assert.AreEqual(expected.Forename, actual.Forename, "Forename mismatch");
            Assert.AreEqual(expected.Gender, actual.Gender, "Gender Mismatch");
            Assert.AreEqual(expected.Group, actual.Group, "Group Mismatch");
            Assert.AreEqual(expected.IsFavourite, actual.IsFavourite, "Is Favourite Mismatch");
            Assert.AreEqual(expected.IsRecent, actual.IsRecent, "Is Recent Mismatch");
            Assert.AreEqual(expected.PhoneIntlDialCode, actual.PhoneIntlDialCode, "Phone Intl Dial Code Mismatch");
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber, "Phone Number Mismatch");
            Assert.AreEqual(expected.Surname, actual.Surname, "Surname Mismatch");
            Assert.AreEqual(expected.TwitterId, actual.TwitterId, "Twitter ID Mismatch");
        }

    }
}
