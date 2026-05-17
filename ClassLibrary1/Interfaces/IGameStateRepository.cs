using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Interfaces
{
    public interface IGameStateRepository
    {
        Task SaveAsync(GameSession session, CancellationToken cancellationToken = default);
        Task<GameSession?> LoadAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(CancellationToken cancellationToken = default);
    }
}
