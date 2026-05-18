using ClassLibrary1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class GameSession
    {
        public GameBoard Board { get; set; }
        public GameSettings Settings { get; }

        public GameStatus Status { get; set; }
        public DateTimeOffset? StartedAt { get; set; }
        public DateTimeOffset? EndedAt { get; set; }
        public int FlaggedCellsCount { get; set; }
        public bool IsBoardGenerated { get; set; }

        public GameSession(GameSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            Board = new GameBoard(settings.Rows, settings.Columns);
            Status = GameStatus.NotStarted;
            IsBoardGenerated = false;
        }

        public TimeSpan? Duration => StartedAt.HasValue && EndedAt.HasValue
            ? EndedAt.Value - StartedAt.Value
            : null;
    }
}
