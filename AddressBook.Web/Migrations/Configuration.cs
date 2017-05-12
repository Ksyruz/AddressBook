namespace AddressBook.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AddressBook.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AddressBook.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            context.Contacts.AddOrUpdate(
              p => p.EmailAddress,
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
            );
        }
    }
}
