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

        public GameBoard Generate(GameSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings);

            var board = new GameBoard(settings.Rows, settings.Columns);

            PlaceMines(board, settings.Mines);
            CalculateAdjacentMineCounts(board);

            return board;
        }

        private void PlaceMines(GameBoard board, int mineCount)
        {
            var positions = GetAllPositions(board.Rows, board.Columns);
            Shuffle(positions);

            for (int i = 0; i < mineCount; i++)
            {
                var (row, column) = positions[i];
                board.GetCell(row, column).PlaceMine();
            }
        }

        private void CalculateAdjacentMineCounts(GameBoard board)
        {
            foreach (var cell in board.GetAllCells())
            {
                if (cell.HasMine)
                {
                    cell.SetAdjacentMines(0);
                    continue;
                }

                int mineCount = board.GetNeighbors(cell.Row, cell.Column)
                    .Count(neighbor => neighbor.HasMine);

                cell.SetAdjacentMines(mineCount);
            }
        }

        private List<(int Row, int Column)> GetAllPositions(int rows, int columns)
        {
            var positions = new List<(int Row, int Column)>(rows * columns);

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    positions.Add((row, column));
                }
            }

            return positions;
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
