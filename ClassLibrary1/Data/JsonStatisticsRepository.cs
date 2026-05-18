using ClassLibrary1.Interfaces;
using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibrary1.Data
{
    public class JsonStatisticsRepository : IStatisticsRepository
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true
        };

        private readonly string _filePath;

        public JsonStatisticsRepository(string? filePath = null)
        {
            _filePath = string.IsNullOrWhiteSpace(filePath)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Minesweeper", "statistics.json")
                : filePath;
        }

        public async Task SaveAsync(GameStatistics statistics, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(statistics);

            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

            var dto = StatisticsDto.FromModel(statistics);

            await using var stream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(stream, dto, Options, cancellationToken);
        }

        public async Task<GameStatistics?> LoadAsync(CancellationToken cancellationToken = default)
        {
            if (!File.Exists(_filePath))
            {
                return null;
            }

            await using var stream = File.OpenRead(_filePath);
            var dto = await JsonSerializer.DeserializeAsync<StatisticsDto>(stream, Options, cancellationToken);

            return dto?.ToModel();
        }
    }
}
