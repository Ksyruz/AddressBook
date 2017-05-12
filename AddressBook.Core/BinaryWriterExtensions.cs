using System;
using System.IO;
using System.Text;

namespace AddressBook
{
    /// <summary>
    /// Extension Methods for the BinaryWriter Class
    /// </summary>
    public static class BinaryWriterExtensions
    {
        /// <summary>
        /// Write a string to a fixed-length field using ASCII
        /// </summary>
        /// <param name="writer">The Writer to output to</param>
        /// <param name="str">The String to write</param>
        /// <param name="length">The length of the field</param>
        /// <remarks>
        /// Pads the passed string up to length with NULL (ASCII 0x00) characters
        /// </remarks>
        public static void Write(this BinaryWriter writer, string str, int length)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            if (str == null)
                throw new ArgumentNullException("str");

            if (length <= 0)
                throw new ArgumentException("length must be > 0");

            if (str.Length > length)
                throw new ArgumentException("str cannot be longer than length");


            byte[] stringBytes = Encoding.ASCII.GetBytes(str);
            writer.Write(stringBytes);

            // Write padding
            for (int i = stringBytes.Length; i < length; i++)
                writer.Write((byte)0x00);

        }
    }
}
