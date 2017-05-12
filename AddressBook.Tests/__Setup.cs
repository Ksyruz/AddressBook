using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Tests
{
    [SetUpFixture]
    public class __Setup
    {
        public static string TestDataPath { get; private set; }

        public static Contact[] ReferenceContacts { get; private set; }


        [OneTimeSetUp]
        public void Setup()
        {
            TestDataPath = Path.Combine(
                Path.GetDirectoryName(typeof(__Setup).Assembly.Location),
                "data.bin");

            // Create Sample Data
            //changed from new[] to Contact[]
            ReferenceContacts = new Contact[]
            {
                new Contact()
                {
                    EmailAddress = "jsmith@hotmail.com",
                    Forename = "John",
                    Gender = Gender.Male,
                    Group = ContactGroup.Personal,
                    IsFavourite = true,
                    IsRecent = false,
                    PhoneIntlDialCode = 44,
                    PhoneNumber = "01234567890",
                    Surname = "Smith",
                    TwitterId = "jsmith987",
                },
                new Contact()
                {
                    EmailAddress = "bob@bobdylan.com",
                    Forename = "Bob",
                    Gender = Gender.Male,
                    Group = ContactGroup.Work,
                    IsFavourite = false,
                    IsRecent = false,
                    PhoneIntlDialCode = 44,
                    PhoneNumber = "09876543210",
                    Surname = "Dylan",
                    TwitterId = "the_real_dylan",
                },
                new Contact()
                {
                    EmailAddress = "m@mi6.gov.uk",
                    Forename = "Judy",
                    Gender = Gender.Female,
                    Group = ContactGroup.Work,
                    IsFavourite = true,
                    IsRecent = false,
                    PhoneIntlDialCode = 44,
                    PhoneNumber = "999",
                    Surname = "Dench",
                    TwitterId = "007sBoss",
                }
            };
        }
    }




}

