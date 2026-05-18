using ClassLibrary1.Interfaces;
using ClassLibrary1.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        private readonly IGameLogicService _gameLogicService;
        private readonly ISettingsRepository _settings_repository;
        private readonly IStatisticsRepository _statisticsRepository;

        private GameStatistics _statistics = new();

        public MainForm(IGameLogicService gameLogicService, ISettingsRepository settingsRepository, IStatisticsRepository statisticsRepository)
        {
            _gameLogicService = gameLogicService ?? throw new ArgumentNullException(nameof(gameLogicService));
            _settings_repository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
            _statisticsRepository = statisticsRepository ?? throw new ArgumentNullException(nameof(statisticsRepository));

            InitializeComponent();

            Load += MainForm_Load;
            btnStart.Click += BtnStart_Click;
        }

        private async void MainForm_Load(object? sender, EventArgs e)
        {
            await LoadSettingsAsync();
            await LoadStatisticsAsync();
        }

        private async Task LoadSettingsAsync()
        {
            try
            {
                var settings = await _settings_repository.LoadAsync();
                if (settings != null)
                {
                    cboDifficulty.SelectedIndex = (int)settings.Difficulty;
                }
                else
                {
                    cboDifficulty.SelectedIndex = 0;
                }
            }
            catch
            {
                cboDifficulty.SelectedIndex = 0;
            }
        }

        private async Task LoadStatisticsAsync()
        {
            try
            {
                var stats = await _statisticsRepository.LoadAsync();
                _statistics = stats ?? new GameStatistics();
            }
            catch
            {
                _statistics = new GameStatistics();
            }

            UpdateStatisticsDisplay();
        }

        private void UpdateStatisticsDisplay()
        {
            lblGamesPlayedValue.Text = _statistics.GamesPlayed.ToString();
            lblGamesWonValue.Text = _statistics.GamesWon.ToString();
            lblGamesLostValue.Text = _statistics.GamesLost.ToString();
            lblBestTimeValue.Text = _statistics.BestTimeSeconds == 0 ? "-" : _statistics.BestTimeSeconds + "s";
            lblWinStreakValue.Text = _statistics.CurrentWinStreak.ToString();
            lblBestWinStreakValue.Text = _statistics.BestWinStreak.ToString();
            lblLastPlayedValue.Text = _statistics.LastPlayedAt?.ToLocalTime().ToString("g") ?? "-";

            lblBestEasyValue.Text = _statistics.BestTimeEasySeconds == 0 ? "Easy: -" : $"Easy: {_statistics.BestTimeEasySeconds}s";
            lblBestMediumValue.Text = _statistics.BestTimeMediumSeconds == 0 ? "Medium: -" : $"Medium: {_statistics.BestTimeMediumSeconds}s";
            lblBestHardValue.Text = _statistics.BestTimeHardSeconds == 0 ? "Hard: -" : $"Hard: {_statistics.BestTimeHardSeconds}s";
        }

        private async void BtnStart_Click(object? sender, EventArgs e)
        {
            GameSettings settings = cboDifficulty.SelectedIndex switch
            {
                1 => GameSettings.Medium(),
                2 => GameSettings.Hard(),
                _ => GameSettings.Easy(),
            };

            try
            {
                await _settings_repository.SaveAsync(settings);
            }
            catch
            {
            }

            using var gameForm = new GameForm(_gameLogicService, _settings_repository, _statisticsRepository, settings);
            gameForm.StartPosition = FormStartPosition.CenterParent;
            gameForm.ShowDialog(this);

            await LoadStatisticsAsync();
        }
    }
}
