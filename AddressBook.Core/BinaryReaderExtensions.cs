using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace AddressBook
{
    /// <summary>
    /// Extension Methods for the BinaryReader class
    /// </summary>
    public static class BinaryReaderExtensions
    {

        /// <summary>
        /// takes BinaryReader to a fixed-length field using ASCII and converts to generic Type T
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="reader">The Reader to read from</param>
        /// <param name="length">The Length of the field</param>
        /// <remarks>
        /// Strips off any trailing NULL (ASCII 0x00) padding
        /// </remarks>
        public static T Read<T>(this BinaryReader reader, int length) 
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            if (length <= 0) return default(T);
                //throw new ArgumentException("length must be > 0");

            byte[] ba = new byte[length]; // are you saying this should be null before and let.ReadBytes allocate it(if it does that?)
            // we are within the casting limits so we can use the optimized method of reading
            ba = reader.ReadBytes(length);

            return (typeof(T) == typeof(string)) ? (T)((object)Encoding.ASCII.GetString(ba).Trim((char)0x00)) : ba.FromByteArray<T>();
        }
        /// <summary>
        /// Write a string to a fixed-length field using ASCII
        /// </summary>
        /// <param name="reader">The Reader to read from</param>
        /// <param name="length">The Length of the field</param>
        /// <remarks>
        /// Strips off any trailing NULL (ASCII 0x00) padding
        /// </remarks>
        public static string ReadString(this BinaryReader reader, long length)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            
            if (length <= 0)
                throw new ArgumentException("length must be > 0");

            byte[] ba = new byte[length]; // are you saying this should be null before and let.ReadBytes allocate it(if it does that?)

            if (length <= int.MaxValue)
            {
                // we are within the casting limits so we can use the optimized method of reading
                ba = reader.ReadBytes((int)length);
            }
            else
            {
                // we are outside the limits (rare) so we can use the normal way of reading (slower)
                ArrayList b = new ArrayList();
                byte readByte = 0x00;
                while (reader.BaseStream.Position < length)
                {
                    readByte = reader.ReadByte();
                    b.Add(readByte);
                }

                ba = (byte[])b.ToArray(typeof(byte));
            }
            return Encoding.ASCII
                .GetString(ba)
                .Trim((char)0x00);
        }
        /// <summary>
        /// Read a BinaryReader to a fixed-length field string using ASCII
        /// </summary>
        /// <param name="reader">The Reader to read from</param>
        /// <remarks>
        /// Strips off any trailing NULL (ASCII 0x00) padding
        /// </remarks>
        public static string ReadAsAsciiString(this BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            byte[] ba = new byte[reader.BaseStream.Length]; // are you saying this should be null before and let.ReadBytes allocate it(if it does that?)

            if (reader.BaseStream.Length <= int.MaxValue)
            {
                // we are within the casting limits so we can use the optimized method of reading
                ba = reader.ReadBytes((int)reader.BaseStream.Length);
            }
            else
            {
                // we are outside the limits (rare) so we can use the normal way of reading (slower)
                ArrayList b = new ArrayList();
                byte readByte = 0x00;
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    readByte = reader.ReadByte();
                    b.Add(readByte);
                }

                ba = (byte[])b.ToArray(typeof(byte));
            }

            //byte[] stringBytes = reader.ReadBytes(length);
            return Encoding.ASCII
                .GetString(ba)
                .Trim((char)0x00);
        }
    }
}
namespace System
{
    public static class ByteEX
    {
        public static T FromByteArray<T>(this byte[] rawValue)
        {
            GCHandle handle = GCHandle.Alloc(rawValue, GCHandleType.Pinned);
            T structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return structure;
        }

        public static byte[] ToByteArray(object value, int maxLength)
        {
            int rawsize = Marshal.SizeOf(value);
            byte[] rawdata = new byte[rawsize];
            GCHandle handle =
                GCHandle.Alloc(rawdata,
                GCHandleType.Pinned);
            Marshal.StructureToPtr(value,
                handle.AddrOfPinnedObject(),
                false);
            handle.Free();
            if (maxLength < rawdata.Length)
            {
                byte[] temp = new byte[maxLength];
                Array.Copy(rawdata, temp, maxLength);
                return temp;
            }
            else
            {
                return rawdata;
            }
        }
    }
}