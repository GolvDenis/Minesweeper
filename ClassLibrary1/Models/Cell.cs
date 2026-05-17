using ClassLibrary1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class Cell
    {
        public int Row { get; }
        public int Column { get; }

        public bool HasMine { get; set; }
        public int AdjacentMines { get; set; }
        public CellState State { get; set; }

        public Cell(int row, int column)
        {
            if (row < 0) throw new ArgumentOutOfRangeException(nameof(row));
            if (column < 0) throw new ArgumentOutOfRangeException(nameof(column));

            Row = row;
            Column = column;
            State = CellState.Hidden;
        }

        public void PlaceMine()
        {
            HasMine = true;
        }

        public void SetAdjacentMines(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            AdjacentMines = count;
        }

        public void Reveal()
        {
            if (State != CellState.Flagged)
            {
                State = CellState.Revealed;
            }
        }

        public void ToggleFlag()
        {
            if (State == CellState.Revealed)
            {
                return;
            }

            State = State == CellState.Flagged
                ? CellState.Hidden
                : CellState.Flagged;
        }

        public bool IsRevealed => State == CellState.Revealed;
        public bool IsFlagged => State == CellState.Flagged;
    }
}
