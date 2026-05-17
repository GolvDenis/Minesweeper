using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class GameBoard
    {
        private readonly Cell[,] _cells;

        public int Rows { get; }
        public int Columns { get; }

        public GameBoard(int rows, int columns)
        {
            if (rows < 2) throw new ArgumentOutOfRangeException(nameof(rows));
            if (columns < 2) throw new ArgumentOutOfRangeException(nameof(columns));

            Rows = rows;
            Columns = columns;

            _cells = new Cell[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    _cells[row, column] = new Cell(row, column);
                }
            }
        }

        public Cell GetCell(int row, int column)
        {
            if (!IsInside(row, column))
            {
                throw new ArgumentOutOfRangeException($"Cell ({row}, {column}) is out of bounds.");
            }

            return _cells[row, column];
        }

        public bool IsInside(int row, int column)
        {
            return row >= 0 && row < Rows && column >= 0 && column < Columns;
        }

        public IEnumerable<Cell> GetNeighbors(int row, int column)
        {
            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = column - 1; c <= column + 1; c++)
                {
                    if (r == row && c == column)
                    {
                        continue;
                    }

                    if (IsInside(r, c))
                    {
                        yield return _cells[r, c];
                    }
                }
            }
        }

        public IEnumerable<Cell> GetAllCells()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    yield return _cells[row, column];
                }
            }
        }
    }
