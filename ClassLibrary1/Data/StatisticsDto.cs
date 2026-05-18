using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data
{
    public class StatisticsDto
    {
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int BestTimeSeconds { get; set; }
        public int BestTimeEasySeconds { get; set; }
        public int BestTimeMediumSeconds { get; set; }
        public int BestTimeHardSeconds { get; set; }
        public int CurrentWinStreak { get; set; }
        public int BestWinStreak { get; set; }
        public DateTimeOffset? LastPlayedAt { get; set; }

        public static StatisticsDto FromModel(GameStatistics statistics)
        {
            ArgumentNullException.ThrowIfNull(statistics);

            return new StatisticsDto
            {
                GamesPlayed = statistics.GamesPlayed,
                GamesWon = statistics.GamesWon,
                GamesLost = statistics.GamesLost,
                BestTimeSeconds = statistics.BestTimeSeconds,
                BestTimeEasySeconds = statistics.BestTimeEasySeconds,
                BestTimeMediumSeconds = statistics.BestTimeMediumSeconds,
                BestTimeHardSeconds = statistics.BestTimeHardSeconds,
                CurrentWinStreak = statistics.CurrentWinStreak,
                BestWinStreak = statistics.BestWinStreak,
                LastPlayedAt = statistics.LastPlayedAt
            };
        }

        public GameStatistics ToModel()
        {
            return new GameStatistics
            {
                GamesPlayed = GamesPlayed,
                GamesWon = GamesWon,
                GamesLost = GamesLost,
                BestTimeSeconds = BestTimeSeconds,
                BestTimeEasySeconds = BestTimeEasySeconds,
                BestTimeMediumSeconds = BestTimeMediumSeconds,
                BestTimeHardSeconds = BestTimeHardSeconds,
                CurrentWinStreak = CurrentWinStreak,
                BestWinStreak = BestWinStreak,
                LastPlayedAt = LastPlayedAt
            };
        }
    }
}
