using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Sudoku
{
    /// <summary>
    /// Board representing the sudoku grid.
    /// </summary>
    public class Board
    {
        #region Events
        /// <summary>
        /// Handler for the grid changes.
        /// </summary>
        [XmlIgnore]
        public EventHandler CellChanged;

        /// <summary>
        /// Handler for a solved puzzle.
        /// </summary>
        [XmlIgnore]
        public EventHandler PuzzleSolved;
        #endregion

        #region Data
        private static int ctr = 0;

        /// <summary>
        /// Sudoku ID.
        /// </summary>
        public readonly string ID;

        /// <summary>
        /// Default grid size of a sudoku.
        /// </summary>
        public static readonly byte GRID_SIZE = 9;

        /// <summary>
        /// Default minigrid size of a sudoku.
        /// </summary>
        public static readonly byte MINIGRID_SIZE = 3;

        /// <summary>
        /// Representation of the current grid.
        /// </summary>
        private Cell[] grid;

        /// <summary>
        /// Solution to the puzzle.
        /// </summary>
        private byte[] solution;
        #endregion

        #region Constructors
        /// <summary>
        /// Parameter constructor.
        /// </summary>
        /// <param name="difficulty">Chosen difficulty.</param>
        /// <param name="grid">Given grid.</param>
        /// <param name="solved">Indicates if solved.</param>
        public Board(Difficulty difficulty, Cell[] grid, bool solved)
        {
            Grid = grid;
            Solved = solved;
            Difficulty = difficulty;
            ID = $"S{ctr++:D6}";
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Board() : this(Difficulty.EASY, (new Cell[GRID_SIZE * GRID_SIZE]).Select(x => new Cell()).ToArray(), false) { }

        /// <summary>
        /// Constructor only with difficulty initializer.
        /// </summary>
        /// <param name="difficulty">Chosen difficulty</param>
        public Board(Difficulty difficulty) : this(difficulty, (new Cell[GRID_SIZE * GRID_SIZE]).Select(x => new Cell()).ToArray(), false) { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="other">Object we copy from.</param>
        public Board(Board other) : this(other.Difficulty, other.grid, other.Solved) { }
        #endregion

        #region Properties
        /// <summary>
        /// Grid property.
        /// </summary>
        public Cell[] Grid
        {
            get => grid;
            set
            {
                if (value != null)
                {
                    grid = new Cell[value.Length];

                    for (int i = 0; i < value.Length; i++)
                    {
                        grid[i] = new Cell(value[i]);
                        grid[i].Index = i;
                        grid[i].PropertyChanged += OnCellChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Chosen difficulty.
        /// </summary>
        public Difficulty Difficulty { get; set; }

        /// <summary>
        /// Deep copy solution property.
        /// </summary>
        public byte[] Solution
        {
            get
            {
                return solution.Select(x => x).ToArray();
            }
            set
            {
                if (value != null)
                {
                    solution = new byte[value.Length];
                    Array.Copy(value, solution, value.Length);
                }
            }
        }

        /// <summary>
        /// Indicates whether the puzzle was solved
        /// </summary>
        public bool Solved { get; set; }    // indicates if the puzzle was solved
        #endregion

        #region Methods
        /// <summary>
        /// Copies the current grid.
        /// </summary>
        /// <returns>Copy of the current grid.</returns>
        public Cell[] GetGridCopy()
        {
            return this.grid.Select(x => new Cell(x) { Index = x.Index }).ToArray();
        }

        /// <summary>
        /// Checks of the puzzle was solved.
        /// In case it was, invokes the PuzzleSolved event.
        /// Otherwise checks the modifiable cells for collisions.
        /// </summary>
        public void CheckIfSolved()
        {
            if (Solved)
            {
                PuzzleSolved?.Invoke(this, null);
                return;
            }

            for (int i = 0; i < grid.Length; i++)
            {
                if (grid[i].Modifiable)
                    grid[i].NotifyPropertyChanged(null);
            }
        }

        /// <summary>
        /// Generates the solution and arranges the grid
        /// according to the chosen difficulty.
        /// </summary>
        public async Task SetUpGrid()
        {
            await Task.Run(() =>
            {
                solution = SudokuGenerator.GenerateSudoku(GRID_SIZE);
                for (int i = 0; i < GRID_SIZE * GRID_SIZE; i++)
                    grid[i].Data = solution[i];

                Random rand = new Random(Guid.NewGuid().GetHashCode());

                byte toHide = (byte)Difficulty;
                while (toHide > 0)
                {
                    int x = rand.Next(GRID_SIZE * GRID_SIZE);

                    if (grid[x].Data != null)
                    {
                        grid[x].Data = null;
                        grid[x].Modifiable = true;
                        grid[x].PropertyChanged += OnCellChanged;
                        toHide--;
                    }
                }
            });
        }

        /// <summary>
        /// Reveals the solution.
        /// </summary>
        public void Solve()
        {
            for (int i = 0; i < GRID_SIZE * GRID_SIZE; i++)
            {
                grid[i].Data = this.solution[i];
            }

            PuzzleSolved?.Invoke(this, null);
        }

        /// <summary>
        /// Handles a cell value change.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        public void OnCellChanged(object sender, EventArgs e)
        {
            Cell c = (Cell)sender;

            if (Solved)
                return;

            (bool IsValid, bool IsComplete) result = BoardValidator.ValidateBoard(grid, c.Index / GRID_SIZE, c.Index % GRID_SIZE);

            if (result.IsValid && result.IsComplete)
                PuzzleSolved?.Invoke(this, null);
            else
                CellChanged?.Invoke((result.IsValid, c.Index), null);
        }

        /// <summary>
        /// Short string representation of the board.
        /// </summary>
        /// <returns>Short string identificator</returns>
        public override string ToString() => $"{ID} {Difficulty}";
        #endregion
    }
}
