using System;
using System.Collections.Generic;
using System.IO;
using LibDat.Types;

namespace LibDat.Data
{
    /// <summary>
    /// Represents a unicode string found in the data section of a .dat file
    /// </summary>
    public class StringData : ValueData<string>
    {
        /// <summary>
        /// The replacement string. If this is set then it will replace the original string when it's saved.
        /// </summary>
        public string NewValue { get; set; }

        public StringData(BaseDataType type, BinaryReader inStream, Dictionary<string, object> options)
            : base(type, inStream, options)
        {
            NewValue = null;
            Length = 2 * Value.Length + 4;

            DatContainer.DataEntries[Offset] = this;
        }

        /// <summary>
        /// Saves the unicode string to the specified stream. 
        /// If 'NewData' has been filled out then it will be written instead of the original data.
        /// </summary>
        /// <param name="outStream"></param>
        public override int Save(BinaryWriter outStream)
        {
            var newOffset = (int)outStream.BaseStream.Position;
            var dataToWrite = NewValue ?? Value;
            outStream.Write<string>(dataToWrite);
            return newOffset;
        }

        public override string GetValueString()
        {
            return String.IsNullOrEmpty(NewValue) ? Value : NewValue;
        }
    }
}