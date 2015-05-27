using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersGameSdk
{
    public enum RowType
    {
        BigNumbers,
        SmallNumbers,
    }

    /// <summary>
    /// This is work in progress - please ignore.
    /// </summary>
    
    public class NumberTileGrid
    {
        private List<RowType> _rowTypes = new List<RowType>();
        public int Rows { get; set; }
        public int RowTypes { get; set; }

        /// <summary>
        /// Returns the kind of row (BigNumbers/SmallNumbers) for a given row index
        /// </summary>
        /// <param name="idx">The zero-based index of the row</param>
        /// <returns></returns>
        public RowType this[int idx]
        {
            get { return _rowTypes[idx]; }
        }

    }
}
