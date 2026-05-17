using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Interfaces
{
    public interface IGameLogicService
    {
        GameSession StartNewGame(GameSettings settings);
        void RevealCell(GameSession session, int row, int column);
        void ToggleFlag(GameSession session, int row, int column);
        bool IsWin(GameSession session);
    }
}
