using ClassLibrary1.Enums;
using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data
{
    public class SettingsDto
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Mines { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        public static SettingsDto FromModel(GameSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings);

            return new SettingsDto
            {
                Rows = settings.Rows,
                Columns = settings.Columns,
                Mines = settings.Mines,
                Difficulty = settings.Difficulty
            };
        }

        public GameSettings ToModel()
        {
            return new GameSettings(Rows, Columns, Mines, Difficulty);
        }
    }
}
