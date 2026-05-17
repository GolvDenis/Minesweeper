using ClassLibrary1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class GameSettings
    {
        public int Rows { get; }
        public int Columns { get; }
        public int Mines { get; }
        public DifficultyLevel Difficulty { get; }

        public GameSettings(int rows, int columns, int mines, DifficultyLevel difficulty)
        {
            if (rows < 2) throw new ArgumentOutOfRangeException(nameof(rows));
            if (columns < 2) throw new ArgumentOutOfRangeException(nameof(columns));
            if (mines < 1) throw new ArgumentOutOfRangeException(nameof(mines));
            if (mines >= rows * columns) throw new ArgumentException("Mines must be less than total cell count.");

            Rows = rows;
            Columns = columns;
            Mines = mines;
            Difficulty = difficulty;
        }

        public static GameSettings Easy() => new(9, 9, 10, DifficultyLevel.Easy);
        public static GameSettings Medium() => new(16, 16, 40, DifficultyLevel.Medium);
        public static GameSettings Hard() => new(16, 30, 99, DifficultyLevel.Hard);
    }
}
