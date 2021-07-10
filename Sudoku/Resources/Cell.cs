using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Collections;
using System.Xml.Serialization;

namespace Sudoku
{
    /// <summary>
    /// Cell class that holds a nullable byte value.
    /// </summary>
    public class Cell : INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// Handles property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Data
        /// <summary>
        /// Value of the object.
        /// </summary>
        private byte? data;
        #endregion

        #region Constructors
        /// <summary>
        /// Parameter constructor.
        /// </summary>
        /// <param name="data">Value</param>
        /// <param name="modifiable">Modifiable indicator</param>
        /// <param name="index">Index in grid</param>
        public Cell(byte? data, bool modifiable, int index)
        {
            this.Data = data;
            this.Modifiable = modifiable;
            this.Index = index;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Cell() : this(null, false, -1) { }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">Copied object</param>
        public Cell(Cell other) : this(other.data, other.Modifiable, other.Index) { }
        #endregion

        #region Properties
        /// <summary>
        /// Data property.
        /// </summary>
        public byte? Data
        {
            get => data;
            set
            {
                if (data != value)
                {
                    data = value;
                    if (Modifiable)
                        NotifyPropertyChanged(nameof(Data));
                }
            }
        }

        /// <summary>
        /// Index representing the position in the grid.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Indicates if the cell value can be changed.
        /// </summary>
        public bool Modifiable { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Notifies about property changes.
        /// </summary>
        /// <param name="propName">Property name</param>
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
