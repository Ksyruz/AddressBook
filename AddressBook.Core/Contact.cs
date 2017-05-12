using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBook
{
    /// <summary>
    /// A Contact entry in an Address Book
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// [PK]
        /// Gets or Sets the ID of the contact
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        [Display(Name = "ContactId")]
        public int ContactId { get; set; }

        /// <summary>
        /// Gets or Sets the Surname
        /// </summary>
        [StringLength(16), Column(TypeName = "varchar"), Index(IsClustered = false, IsUnique = false)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or Sets the Forename
        /// </summary>
        [StringLength(16), Column(TypeName = "varchar")]
        [Display(Name = "Forename")]
        public string Forename { get; set; }

        /// <summary>
        /// Gets or Sets the Internaltional Dialing Code
        /// </summary>
        /// <example>44 = UK</example>
        [Column(TypeName = "int")]
        [Display(Name = "PhoneIntlDialCode")]
        public int PhoneIntlDialCode { get; set; }

        /// <summary>
        /// Gets or Sets the Phone Number
        /// </summary>
        [StringLength(16), Column(TypeName = "varchar")]
        [Display(Name = "Forename")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or Sets the Email Address
        /// </summary>
        [StringLength(64), Column(TypeName = "nvarchar"), Index(IsClustered = false, IsUnique = false)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "EmailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Sets or Sets the Twitter Username
        /// </summary>
        [StringLength(32), Column(TypeName = "varchar")]
        [Display(Name = "TwitterId")]
        public string TwitterId { get; set; }

        /// <summary>
        /// Gets or Sets whether the Contact is a 'Favourite'
        /// </summary>
        public bool IsFavourite { get; set; } = false;

        /// <summary>
        /// Gets or Sets the Gender of the Contact
        /// </summary>
        public Gender Gender { get; set; } = Gender.Male;

        /// <summary>
        /// Gets or Sets the Group for the Contact
        /// </summary>
        public ContactGroup Group { get; set; } = ContactGroup.Personal;

        /// <summary>
        /// Gets or Sets whether the contact is 'recent'
        /// </summary>
        public bool IsRecent { get; set; } = false;

    }
}
