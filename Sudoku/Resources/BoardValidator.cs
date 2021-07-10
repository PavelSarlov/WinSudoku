using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace Sudoku
{
    /// <summary>
    /// Helper static class for board validation.
    /// </summary>
    public static class BoardValidator
    {
        #region Solution Checker
        /// <summary>
        /// Checks if a row is valid.
        /// </summary>
        /// <param name="b">Solution grid.</param>
        /// <param name="row">Row to check.</param>
        /// <returns>True if no collisions are found.</returns>
        private static bool ValidateRow(byte[] b, int row)
        {
            bool[] duplicates = new bool[Board.GRID_SIZE];
            Parallel.For(0, Board.GRID_SIZE, i => { duplicates[i] = false; });
            for (int col = 0; col < Board.GRID_SIZE; col++)
            {
                if (b[row * Board.GRID_SIZE + col] == 0)
                    return true;
                if (duplicates[b[row * Board.GRID_SIZE + col] - 1])
                    return false;
                if (b[row * Board.GRID_SIZE + col] != 0)
                    duplicates[b[row * Board.GRID_SIZE + col] - 1] = true;
            }
            return true;
        }

        /// <summary>
        /// Checks if a column is valid.
        /// </summary>
        /// <param name="b">Solution grid.</param>
        /// <param name="col">Row to check.</param>
        /// <returns>True if no collisions are found.</returns>
        private static bool ValidateCol(byte[] b, int col)
        {
            bool[] duplicates = new bool[Board.GRID_SIZE];
            Parallel.For(0, Board.GRID_SIZE, i => { duplicates[i] = false; });
            for (int row = 0; row < Board.GRID_SIZE; row++)
            {
                if (b[row * Board.GRID_SIZE + col] == 0)
                    return true;
                if (duplicates[b[row * Board.GRID_SIZE + col] - 1])
                    return false;
                if (b[row * Board.GRID_SIZE + col] != 0)
                    duplicates[b[row * Board.GRID_SIZE + col] - 1] = true;
            }
            return true;
        }

        /// <summary>
        /// Checks if a minigrid is valid.
        /// </summary>
        /// <param name="b">Solution grid.</param>
        /// <param name="x">Cell row.</param>
        /// <param name="y">Cell column.</param>
        /// <returns>True if no collisions are found.</returns>
        private static bool ValidateMinigrid(byte[] b, int x, int y)
        {
            bool[] duplicates = new bool[Board.GRID_SIZE];
            Parallel.For(0, Board.GRID_SIZE, i => { duplicates[i] = false; });
            int startRow = x - x % Board.MINIGRID_SIZE;
            int startCol = y - y % Board.MINIGRID_SIZE;
            for (int row = startRow; row < startRow + 3; row++)
            {
                for (int col = startCol; col < startCol + 3; col++)
                {
                    if (b[row * Board.GRID_SIZE + col] == 0)
                        return true;
                    if (duplicates[b[row * Board.GRID_SIZE + col] - 1])
                        return false;
                    if (b[row * Board.GRID_SIZE + col] != 0)
                        duplicates[b[row * Board.GRID_SIZE + col] - 1] = true;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if there are any row, column
        /// or minigrid collisions with the value in the
        /// given cell coordinates.
        /// </summary>
        /// <param name="b">Grid to check</param>
        /// <param name="x">Row</param>
        /// <param name="y">Column</param>
        /// <returns></returns>
        public static bool ValidateBoard(byte[] b, int x, int y)
        {
            return (ValidateRow(b, x) && ValidateCol(b, y) && ValidateMinigrid(b, x, y));
        }
        #endregion

        #region Grid Checker
        /// <summary>
        /// Checks if row is valid.
        /// </summary>
        /// <param name="b">Sudoku grid.</param>
        /// <param name="x">Cell row.</param>
        /// <param name="y">Cell column.</param>
        /// <returns>True if no collisions are found.</returns>
        private static bool ValidateRow(Cell[] b, int x, int y)
        {
            if (b[x * Board.GRID_SIZE + y].Data == null)
                return true;

            int row = x * Board.GRID_SIZE;

            for (int col = 0; col < Board.GRID_SIZE; col++)
            {
                if (col != y && b[row + col].Data == b[row + y].Data)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Check if column is valid.
        /// </summary>
        /// <param name="b">Sudoku grid.</param>
        /// <param name="x">Cell row.</param>
        /// <param name="y">Cell column.</param>
        /// <returns>True if no collisions are found.</returns>
        private static bool ValidateCol(Cell[] b, int x, int y)
        {
            if (b[x * Board.GRID_SIZE + y].Data == null)
                return true;

            int col = y;

            for (int row = 0; row < Board.GRID_SIZE; row++)
            {
                if (row != x && b[row * Board.GRID_SIZE + col].Data == b[x * Board.GRID_SIZE + col].Data)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a minigrid is valid.
        /// </summary>
        /// <param name="b">Sudoku grid.</param>
        /// <param name="x">Cell row.</param>
        /// <param name="y">Cell column.</param>
        /// <returns>True if no collisions are found.</returns>
        private static bool ValidateMinigrid(Cell[] b, int x, int y)
        {
            if (b[x * Board.GRID_SIZE + y].Data == null)
                return true;

            int startRow = x - x % Board.MINIGRID_SIZE;
            int startCol = y - y % Board.MINIGRID_SIZE;

            for (int row = startRow; row < startRow + 3; row++)
            {
                for (int col = startCol; col < startCol + 3; col++)
                {
                    if (row != x && col != y && b[row * Board.GRID_SIZE + col].Data == b[x * Board.GRID_SIZE + y].Data)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if there are any row, column
        /// or minigrid collisions with the value in the
        /// given cell coordinates and also if the grid has any empty cells.
        /// </summary>
        /// <param name="b">Grid to check.</param>
        /// <param name="x">Cell row.</param>
        /// <param name="y">Cell column.</param>
        /// <returns>The results of each condition plus completeness of the grid, all in a tuple.</returns>
        public static (bool, bool) ValidateBoard(Cell[] b, int x, int y)
        {
            return ((ValidateRow(b, x, y) && ValidateCol(b, x, y) && ValidateMinigrid(b, x, y)), (b.Where(i => i.Data == null).Count() == 0));
        }
        #endregion
    }
}
