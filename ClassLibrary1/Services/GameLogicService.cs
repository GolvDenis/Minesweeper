using ClassLibrary1.Enums;
using ClassLibrary1.Interfaces;
using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Services
{
    public class GameLogicService : IGameLogicService
    {
        private readonly IGameBoardGenerator _boardGenerator;

        public GameLogicService(IGameBoardGenerator boardGenerator)
        {
            _boardGenerator = boardGenerator ?? throw new ArgumentNullException(nameof(boardGenerator));
        }

        public GameSession StartNewGame(GameSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings);
            return new GameSession(settings);
        }

        public void RevealCell(GameSession session, int row, int column)
        {
            ValidateSession(session);

            if (session.Status is GameStatus.Won or GameStatus.Lost)
            {
                return;
            }

            if (!session.Board.IsInside(row, column))
            {
                return;
            }

            if (!session.IsBoardGenerated)
            {
                session.Board = _boardGenerator.Generate(session.Settings, row, column);
                session.IsBoardGenerated = true;
                session.Status = GameStatus.Running;
                session.StartedAt = DateTimeOffset.UtcNow;
            }

            var startCell = session.Board.GetCell(row, column);

            if (startCell.IsFlagged || startCell.IsRevealed)
            {
                return;
            }

            startCell.Reveal();

            if (startCell.HasMine)
            {
                LoseGame(session);
                return;
            }

            if (startCell.AdjacentMines == 0)
            {
                RevealEmptyArea(session, startCell.Row, startCell.Column);
            }

            if (IsWin(session))
            {
                WinGame(session);
            }
        }

        public void ToggleFlag(GameSession session, int row, int column)
        {
            ValidateSession(session);

            if (session.Status is GameStatus.Won or GameStatus.Lost)
            {
                return;
            }

            if (!session.IsBoardGenerated)
            {
                return;
            }

            if (!session.Board.IsInside(row, column))
            {
                return;
            }

            var cell = session.Board.GetCell(row, column);

            if (cell.IsRevealed)
            {
                return;
            }

            bool wasFlagged = cell.IsFlagged;
            cell.ToggleFlag();

            if (wasFlagged && !cell.IsFlagged)
            {
                session.FlaggedCellsCount--;
            }
            else if (!wasFlagged && cell.IsFlagged)
            {
                session.FlaggedCellsCount++;
            }
        }

        public bool IsWin(GameSession session)
        {
            ValidateSession(session);

            if (!session.IsBoardGenerated)
            {
                return false;
            }

            foreach (var cell in session.Board.GetAllCells())
            {
                if (!cell.HasMine && !cell.IsRevealed)
                {
                    return false;
                }
            }

            return true;
        }

        private void RevealEmptyArea(GameSession session, int row, int column)
        {
            var queue = new Queue<(int Row, int Column)>();
            queue.Enqueue((row, column));

            while (queue.Count > 0)
            {
                var (currentRow, currentColumn) = queue.Dequeue();

                foreach (var neighbor in session.Board.GetNeighbors(currentRow, currentColumn))
                {
                    if (neighbor.IsRevealed || neighbor.IsFlagged)
                    {
                        continue;
                    }

                    neighbor.Reveal();

                    if (!neighbor.HasMine && neighbor.AdjacentMines == 0)
                    {
                        queue.Enqueue((neighbor.Row, neighbor.Column));
                    }
                }
            }
        }

        private void WinGame(GameSession session)
        {
            session.Status = GameStatus.Won;
            session.EndedAt = DateTimeOffset.UtcNow;
        }

        private void LoseGame(GameSession session)
        {
            session.Status = GameStatus.Lost;
            session.EndedAt = DateTimeOffset.UtcNow;

            foreach (var cell in session.Board.GetAllCells())
            {
                if (cell.HasMine)
                {
                    cell.Reveal();
                }
            }
        }

        private static void ValidateSession(GameSession session)
        {
            ArgumentNullException.ThrowIfNull(session);
            ArgumentNullException.ThrowIfNull(session.Board);
            ArgumentNullException.ThrowIfNull(session.Settings);
        }

    }
}
