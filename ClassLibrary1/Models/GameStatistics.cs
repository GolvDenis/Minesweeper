using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class GameStatistics
    {
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }

        public int BestTimeSeconds { get; set; }
        public int CurrentWinStreak { get; set; }
        public int BestWinStreak { get; set; }

        public DateTimeOffset? LastPlayedAt { get; set; }

        public void RegisterWin(int timeSeconds)
        {
            GamesPlayed++;
            GamesWon++;
            CurrentWinStreak++;

            if (BestWinStreak < CurrentWinStreak)
            {
                BestWinStreak = CurrentWinStreak;
            }

            if (BestTimeSeconds == 0 || timeSeconds < BestTimeSeconds)
            {
                BestTimeSeconds = timeSeconds;
            }

            LastPlayedAt = DateTimeOffset.UtcNow;
        }

        public void RegisterLoss()
        {
            GamesPlayed++;
            GamesLost++;
            CurrentWinStreak = 0;
            LastPlayedAt = DateTimeOffset.UtcNow;
        }
    }
}
