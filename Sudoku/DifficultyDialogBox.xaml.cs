using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for DifficultyDialogBox.xaml
    /// </summary>
    public partial class DifficultyDialogBox : Window
    {
        /// <summary>
        /// Chosen difficulty.
        /// </summary>
        public Difficulty? Result { get; private set; }

        /// <summary>
        /// Dialog window constructor.
        /// </summary>
        public DifficultyDialogBox()
        {
            InitializeComponent();

            rbtnEasy.Tag = Difficulty.EASY;
            rbtnMedium.Tag = Difficulty.MEDIUM;
            rbtnHard.Tag = Difficulty.HARD;
        }

        /// <summary>
        /// Enables "Continue" button and updates choice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionChosen(object sender, RoutedEventArgs e)
        {
            btnContinue.IsEnabled = true;
            Result = ((Difficulty)((RadioButton)sender).Tag);
        }

        /// <summary>
        /// Returns to the main window with positive result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Returns to the main window with negative result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Result = null;
            this.Close();
        }
    }
}
