using ClassLibrary1.Models;
using ClassLibrary1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibrary1.Data
{
    public class JsonSettingsRepository : ISettingsRepository
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true
        };

        private readonly string _filePath;

        public JsonSettingsRepository(string? filePath = null)
        {
            _filePath = string.IsNullOrWhiteSpace(filePath)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Minesweeper", "settings.json")
                : filePath;
        }

        public async Task SaveAsync(GameSettings settings, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(settings);

            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

            var dto = SettingsDto.FromModel(settings);

            await using var stream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(stream, dto, Options, cancellationToken);
        }

        public async Task<GameSettings?> LoadAsync(CancellationToken cancellationToken = default)
        {
            if (!File.Exists(_filePath))
            {
                return null;
            }

            await using var stream = File.OpenRead(_filePath);
            var dto = await JsonSerializer.DeserializeAsync<SettingsDto>(stream, Options, cancellationToken);

            return dto?.ToModel();
        }
    }
}
