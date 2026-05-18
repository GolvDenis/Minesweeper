using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class CellButton : Button
    {
        public int Row { get; }
        public int Column { get; }

        public CellButton(int row, int column)
        {
            Row = row;
            Column = column;

            Margin = Padding.Empty;
            Padding = Padding.Empty;
            Dock = DockStyle.Fill;
            Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MinimumSize = new Size(28, 28);
        }

        public void ApplyCell(Cell cell, bool gameFinished = false)
        {
            if (cell.IsRevealed)
            {
                Enabled = false;
                Text = cell.HasMine
                    ? "💣"
                    : cell.AdjacentMines == 0 ? string.Empty : cell.AdjacentMines.ToString();

                return;
            }

            Enabled = !gameFinished;
            Text = cell.IsFlagged ? "🚩" : string.Empty;
        }
    }
}
