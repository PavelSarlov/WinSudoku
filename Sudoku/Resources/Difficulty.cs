using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// Represents the number of empty cells.
    /// </summary>
    public enum Difficulty
    {
        /// <summary>
        /// Easy mode hides 26 cells.
        /// </summary>
        EASY = 26,
        /// <summary>
        /// Medium mode hides 41 cells.
        /// </summary>
        MEDIUM = 41,
        /// <summary>
        /// Hard mode hides 56 cells.
        /// </summary>
        HARD = 56
    }
}
