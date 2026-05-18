using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Interfaces
{
    public interface ISettingsRepository
    {
        Task SaveAsync(GameSettings settings, CancellationToken cancellationToken = default);
        Task<GameSettings?> LoadAsync(CancellationToken cancellationToken = default);
    }
}
