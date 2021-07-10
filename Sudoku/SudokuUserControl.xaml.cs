using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for SudokuUserControl.xaml
    /// </summary>
    public partial class SudokuUserControl : UserControl
    {
        #region Data
        private Timer timer = new Timer();                           // stopwatch
        private Board sudoku;                                        // data board
        private TextBox[] textBoxes;                                 // boxes of the grid
        private Stack<Cell[]> undoStack = new Stack<Cell[]>();       // undo operations stack
        private Stack<Cell[]> redoStack = new Stack<Cell[]>();       // redo operations stack
        private DirectoryInfo saveDir;                               // default save directory

        // keeping track of saved sudoku in current session
        private ObservableCollection<Tuple<Board, Timer>> currentSessionSudokuList = new ObservableCollection<Tuple<Board, Timer>>();
        #endregion

        /// <summary>
        /// Main method.
        /// </summary>
        public SudokuUserControl()
        {
            InitializeComponent();
            InitializeSaveDirectory();
            GenerateGrid();
            listCurrentSaves.ItemsSource = currentSessionSudokuList;
        }

        #region Utility Methods
        /// <summary>
        /// Bind new timer to textblock.
        /// </summary>
        private void BindTimer(Timer newTimer)
        {
            timer?.Stop();
            timer = newTimer == null ? new Timer() : newTimer;

            txtTime.DataContext = timer;

            timer.Start();
        }

        /// <summary>
        /// Setting up the save directory.
        /// </summary>
        private void InitializeSaveDirectory()
        {
            if (!Directory.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Saves\\"))
                saveDir = Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Saves\\");
            else
                saveDir = new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Saves\\");
        }

        /// <summary>
        /// Rebind textboxes to their corresponding grid cell.
        /// Reset the timer and clear the undo/redo stacks.
        /// </summary>
        private void ResetInterface(Timer t)
        {
            Keyboard.ClearFocus();

            for (byte i = 0; i < sudoku.Grid.Length; i++)
            {
                textBoxes[i].DataContext = sudoku.Grid[i];

                textBoxes[i].IsReadOnly = true;
                textBoxes[i].Background = Brushes.LightGray;

                if (sudoku.Grid[i].Modifiable)
                {
                    textBoxes[i].IsReadOnly = false;
                    textBoxes[i].Focusable = true;
                    textBoxes[i].Background = Brushes.Transparent;
                }
            }

            txtCurrentDifficulty.DataContext = sudoku;
            switch (sudoku.Difficulty)
            {
                case Difficulty.EASY:
                    txtCurrentDifficulty.Foreground = Brushes.DarkSeaGreen;
                    break;
                case Difficulty.MEDIUM:
                    txtCurrentDifficulty.Foreground = Brushes.DarkGoldenrod;
                    break;
                case Difficulty.HARD:
                    txtCurrentDifficulty.Foreground = Brushes.DarkRed;
                    break;
            }

            sudoku.CellChanged = new EventHandler(OnCellChanged);
            sudoku.PuzzleSolved = new EventHandler(OnSolved);
            BindTimer(t);
            undoStack.Clear();
            redoStack.Clear();
            mnuUndo.IsEnabled = false;
            mnuRedo.IsEnabled = false;
            mnuSaveAs.IsEnabled = true;
            mnuSolve.IsEnabled = true;
        }

        /// <summary>
        /// Creating a TextBox for each grid cell.
        /// </summary>
        private void GenerateGrid()
        {
            int length = Board.GRID_SIZE * Board.GRID_SIZE;
            textBoxes = new TextBox[length];

            for (int i = 0; i < length; i++)
            {
                // Create textbox for each cell and bind it
                textBoxes[i] = new TextBox
                {
                    FontSize = 30,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    TextAlignment = TextAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    IsReadOnly = true,
                    Background = Brushes.Transparent,
                    Margin = new Thickness(5),
                    BorderBrush = Brushes.Transparent,
                    Focusable = false,
                };

                Binding b = new Binding
                {
                    Path = new PropertyPath($"Data"),
                    Converter = new ByteConverter(),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };

                textBoxes[i].SetBinding(TextBox.TextProperty, b);
                textBoxes[i].PreviewTextInput += TextBox_PreviewTextInput;
                textBoxes[i].PreviewKeyDown += TextBox_BackKeyDown;
                textBoxes[i].GotFocus += TextBox_GotFocus;
                textBoxes[i].LostFocus += TextBox_LostFocus;

                Grid.SetRow(textBoxes[i], i / Board.GRID_SIZE);
                Grid.SetColumn(textBoxes[i], i % Board.GRID_SIZE);
                grdSudoku.Children.Add(textBoxes[i]);


                // Add border for each cell
                Border border = new Border()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                };

                Grid.SetRow(border, i / Board.GRID_SIZE);
                Grid.SetColumn(border, i % Board.GRID_SIZE);
                grdSudoku.Children.Add(border);
            }
        }
        #endregion

        #region TextBox EventHandlers
        /// <summary>
        /// Handles textbox being focused
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e"></param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!tb.IsReadOnly)
            {
                tb.BorderThickness = new Thickness(2);
            }
        }

        /// <summary>
        /// Handles textbox losing focus
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e"></param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!tb.IsReadOnly)
            {
                tb.BorderThickness = new Thickness(0);
            }
        }

        /// <summary>
        /// Handling textbox backspace input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_BackKeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (e.Key == Key.Back && tb.Text.Length > 0 && tb.Text.Remove(tb.Text.Length - 1) == string.Empty)
            {
                undoStack.Push(sudoku.GetGridCopy());
                mnuUndo.IsEnabled = true;

                redoStack.Clear();
                mnuRedo.IsEnabled = false;
            }
        }

        /// <summary>
        /// Handling textbox input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (!byte.TryParse(tb.Text + e.Text, out byte num) || num < 1 || num > 9)
            {
                e.Handled = true;
                return;
            }

            undoStack.Push(sudoku.GetGridCopy());
            mnuUndo.IsEnabled = true;

            redoStack.Clear();
            mnuRedo.IsEnabled = false;
        }
        #endregion

        #region Menu EventHandlers
        /// <summary>
        /// Menu option File->New generates new sudoku
        /// with the chosen difficulty.
        /// </summary>
        /// <param name="sender">File->New</param>
        /// <param name="e"></param>
        private async void MnuNew_Click(object sender, RoutedEventArgs e)
        {
            DifficultyDialogBox difficultyDialogBox = new DifficultyDialogBox();
            bool? result = difficultyDialogBox.ShowDialog();

            if (result == true)
            {
                sudoku = new Board(difficultyDialogBox.Result.Value);
                await Task.Run(sudoku.SetUpGrid).ContinueWith((t) => this.Dispatcher.Invoke(() => ResetInterface(null)));
            }
        }

        /// <summary>
        /// Menu option "Edit->Solve" to solve the current puzzle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuSolve_Click(object sender, RoutedEventArgs e)
        {
            if (sudoku.Solved)
            {
                MessageBox.Show("The puzzle is already solved.");
                return;
            }

            sudoku.Solved = true;
            sudoku.Solve();
            timer.Stop();
            timer.Reset();
            timer.CurrentTime = "Revealed";
            undoStack.Clear();
            redoStack.Clear();
            mnuUndo.IsEnabled = false;
            mnuRedo.IsEnabled = false;

            textBoxes.ToList().ForEach(x =>
            {
                x.Background = Brushes.LightGray;
                x.IsReadOnly = true;
            });

            MessageBox.Show("Puzzle was revealed. Timer cancelled.");
        }

        /// <summary>
        /// Menu option to undo the last action on the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuUndo_Click(object sender, RoutedEventArgs e)
        {
            if (undoStack.Count == 0)
                return;

            redoStack.Push(sudoku.GetGridCopy());
            mnuRedo.IsEnabled = true;

            for (int i = 0; i < textBoxes.Length; i++)
                sudoku.Grid[i].Data = undoStack.Peek()[i].Data;
            undoStack.Pop();

            if (undoStack.Count == 0)
                mnuUndo.IsEnabled = false;
        }

        /// <summary>
        /// Menu option to redo the last action on the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuRedo_Click(object sender, RoutedEventArgs e)
        {
            if (redoStack.Count == 0)
                return;

            undoStack.Push(sudoku.GetGridCopy());
            mnuUndo.IsEnabled = true;

            for (int i = 0; i < textBoxes.Length; i++)
                sudoku.Grid[i].Data = redoStack.Peek()[i].Data;
            redoStack.Pop();

            if (redoStack.Count == 0)
                mnuRedo.IsEnabled = false;
        }

        /// <summary>
        /// Menu option "File->Load" to load selected progress from the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuLoad_Click(object sender, RoutedEventArgs e)
        {
            if (listCurrentSaves.SelectedItem != null && currentSessionSudokuList[listCurrentSaves.SelectedIndex].Item1.ID != sudoku.ID)
            {
                var item = currentSessionSudokuList[listCurrentSaves.SelectedIndex];
                sudoku = item.Item1;
                ResetInterface(item.Item2);
                sudoku.CheckIfSolved();
            }
        }

        /// <summary>
        /// Menu option "File->Load From File" to load an exisitng sudoku.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Data Files|*.dat";
            ofd.Title = "Open Sudoku Board";
            ofd.InitialDirectory = saveDir.FullName;

            bool? result = ofd.ShowDialog();

            if (result == true)
            {
                string openPath = ofd.FileName;

                if (File.Exists(openPath))
                {
                    using (FileStream board = new FileStream(openPath, FileMode.Open, FileAccess.Read))
                    {
                        try
                        {
                            var serializer = new XmlSerializer(typeof(Board));
                            sudoku = new Board(((Board)serializer.Deserialize(board)));
                            ResetInterface(null);
                            sudoku.CheckIfSolved();
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show($"Error while loading file: {exc.Message}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Menu option "File->Save" to save current progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuSave_Click(object sender, RoutedEventArgs e)
        {
            if (currentSessionSudokuList.Where(x => x.Item1.ID == sudoku.ID).Count() == 0)
            {
                currentSessionSudokuList.Add(new Tuple<Board, Timer>(sudoku, timer));
            }
        }

        /// <summary>
        /// Menu option "File->Save As" to save the current sudoku as a file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Data Files|*.dat";
            sfd.Title = "Save Sudoku Board";
            sfd.InitialDirectory = saveDir.FullName;

            bool? result = sfd.ShowDialog();

            if (result == true)
            {
                string savePath = sfd.FileName;

                using (FileStream save = File.Create(savePath))
                {
                    try
                    {
                        var serializer = new XmlSerializer(typeof(Board));
                        serializer.Serialize(save, sudoku);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show($"Error while saving file: {exc.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// Menu option to check current progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuCheckProgress_Click(object sender, RoutedEventArgs e)
        {
            if (currentSessionSudokuList.Count() == 0)
            {
                MessageBox.Show($"There is no progress to report.");
                return;
            }

            int solvedPuzzles = currentSessionSudokuList.Where(x => (((Board)x.Item1).Solved && x.Item2.CurrentTime != "Revealed")).Count();

            MessageBox.Show($"{solvedPuzzles} sudoku solved out of {currentSessionSudokuList.Count} puzzles total. Progress: {(double)solvedPuzzles / currentSessionSudokuList.Count:0.0%}");
        }

        private void MnuQuit_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
        #endregion

        #region Grid Change EventHandlers
        /// <summary>
        /// Handling solved puzzle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSolved(object sender, EventArgs e)
        {
            timer.Stop();
            undoStack.Clear();
            redoStack.Clear();

            mnuSolve.IsEnabled = false;

            if (sudoku.Solved)
            {
                textBoxes.ToList().ForEach(x =>
                {
                    x.Background = Brushes.LightGray;
                    x.IsReadOnly = true;
                });

                return;
            }

            sudoku.Solved = true;

            textBoxes.ToList().ForEach(x =>
            {
                x.Background = Brushes.LightGreen;
                x.IsReadOnly = true;
            });

            MessageBox.Show($"Congrats! You solved the puzzle with a time of {timer.CurrentTime}.");
        }

        /// <summary>
        /// Handling cell value change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCellChanged(object sender, EventArgs e)
        {
            (bool IsValid, int Index) result = ((bool, int))sender;

            if (!result.IsValid)
            {
                textBoxes[result.Index].Background = Brushes.Red;
                textBoxes[result.Index].ToolTip = "You should have unique numbers in each row, column and minigrid";
            }
            else
            {
                textBoxes[result.Index].Background = Brushes.Transparent;
                textBoxes[result.Index].ToolTip = null;
            }
        }
        #endregion
    }
}
