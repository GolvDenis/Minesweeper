using ClassLibrary1.Interfaces;
using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Services
{
    public sealed class GameBoardGenerator : IGameBoardGenerator
    {
        private readonly Random _random;

        public GameBoardGenerator(Random? random = null)
        {
            _random = random ?? Random.Shared;
        }

        public GameBoard Generate(GameSettings settings, int safeRow, int safeColumn)
        {
            ArgumentNullException.ThrowIfNull(settings);

            var board = new GameBoard(settings.Rows, settings.Columns);

            var excluded = new HashSet<(int Row, int Column)>();

            for (int r = safeRow - 1; r <= safeRow + 1; r++)
            {
                for (int c = safeColumn - 1; c <= safeColumn + 1; c++)
                {
                    if (board.IsInside(r, c))
                    {
                        excluded.Add((r, c));
                    }
                }
            }

            var positions = GetAllPositions(board.Rows, board.Columns)
                .Where(position => !excluded.Contains(position))
                .ToList();

            if (settings.Mines > positions.Count)
            {
                throw new ArgumentException("Too many mines for the selected board size.");
            }

            Shuffle(positions);

            for (int i = 0; i < settings.Mines; i++)
            {
                var (row, column) = positions[i];
                board.GetCell(row, column).PlaceMine();
            }

            CalculateAdjacentMineCounts(board);

            return board;
        }

        private static void CalculateAdjacentMineCounts(GameBoard board)
        {
            foreach (var cell in board.GetAllCells())
            {
                if (cell.HasMine)
                {
                    cell.SetAdjacentMines(0);
                    continue;
                }

                var count = board.GetNeighbors(cell.Row, cell.Column).Count(neighbor => neighbor.HasMine);
                cell.SetAdjacentMines(count);
            }
        }

        private static List<(int Row, int Column)> GetAllPositions(int rows, int columns)
        {
            var result = new List<(int Row, int Column)>(rows * columns);

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    result.Add((row, column));
                }
            }

            return result;
        }

        private void Shuffle<T>(IList<T> items)
        {
            for (int i = items.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (items[i], items[j]) = (items[j], items[i]);
            }
        }
    }
}
