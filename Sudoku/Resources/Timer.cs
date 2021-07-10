using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Threading;
using System.Diagnostics;

namespace Sudoku
{
    /// <summary>
    /// Simple time tracking using dispatcher and stopwatch
    /// </summary>
    public class Timer : INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// Handles property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Data
        private DispatcherTimer disTimer;
        private Stopwatch stopWatch;
        private string currentTime;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Timer()
        {
            this.disTimer = new DispatcherTimer();
            this.stopWatch = new Stopwatch();
            this.CurrentTime = "00:00:00";

            this.disTimer.Tick += Tick;
            this.disTimer.Interval = new TimeSpan(0, 0, 0, 1);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Holds string representation of current time.
        /// </summary>
        public string CurrentTime
        {
            get => currentTime;
            set
            {
                currentTime = value != null ? value : "00:00:00";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTime)));
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles seconds ticking of the dispatcher.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        public void Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                CurrentTime = string.Format($"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}");
            }
        }

        /// <summary>
        /// Resets the timer.
        /// </summary>
        public void Reset()
        {
            stopWatch.Reset();
            CurrentTime = "00:00:00";
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            stopWatch.Start();
            disTimer.Start();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            stopWatch.Stop();
            disTimer.Stop();
        }
        #endregion
    }
}
