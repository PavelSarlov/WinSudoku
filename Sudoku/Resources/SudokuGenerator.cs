using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// Generates a random sudoku puzzle.
    /// </summary>
    public class SudokuGenerator
    {
        /// <summary>
        /// Uses a simple algorithm with backtracking to generate a sudoku solution.
        /// </summary>
        /// <param name="size">Default grid size.</param>
        /// <returns>Found solution.</returns>
        public static byte[] GenerateSudoku(byte size)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            byte fullSize = (byte)(size * size);
            byte[] solution = new byte[fullSize];
            HashSet<byte>[] available = new HashSet<byte>[fullSize];

            // fill available digits for all cells
            for (int i = 0; i < fullSize; i++)
            {
                available[i] = new HashSet<byte>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            }

            // generate solution using backtracking
            byte index = 0;
            while (index < fullSize)
            {
                while (available[index].Count > 0)
                {
                    solution[index] = available[index].ElementAt(rand.Next(available[index].Count));
                    available[index].Remove(solution[index]);
                    if (!BoardValidator.ValidateBoard(solution, index / size, index % size))
                    {
                        continue;
                    }
                    index++;
                    break;
                }

                if (index < fullSize && available[index].Count == 0)
                {
                    available[index] = new HashSet<byte>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    solution[index] = 0;
                    index--;
                }
            }

            return solution;
        }
    }
}
