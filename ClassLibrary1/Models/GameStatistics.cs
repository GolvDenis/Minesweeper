using ClassLibrary1.Enums;
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
        public int BestTimeEasySeconds { get; set; }
        public int BestTimeMediumSeconds { get; set; }
        public int BestTimeHardSeconds { get; set; }

        public int CurrentWinStreak { get; set; }
        public int BestWinStreak { get; set; }

        public DateTimeOffset? LastPlayedAt { get; set; }

        public void RegisterWin(int timeSeconds, DifficultyLevel difficulty)
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

            switch (difficulty)
            {
                case DifficultyLevel.Easy:
                    if (BestTimeEasySeconds == 0 || timeSeconds < BestTimeEasySeconds)
                        BestTimeEasySeconds = timeSeconds;
                    break;
                case DifficultyLevel.Medium:
                    if (BestTimeMediumSeconds == 0 || timeSeconds < BestTimeMediumSeconds)
                        BestTimeMediumSeconds = timeSeconds;
                    break;
                case DifficultyLevel.Hard:
                    if (BestTimeHardSeconds == 0 || timeSeconds < BestTimeHardSeconds)
                        BestTimeHardSeconds = timeSeconds;
                    break;
            }

            LastPlayedAt = DateTimeOffset.UtcNow;
        }

        public void RegisterWin(int timeSeconds)
        {
            RegisterWin(timeSeconds, DifficultyLevel.Easy);
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
