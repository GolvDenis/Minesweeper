using ClassLibrary1.Interfaces;
using ClassLibrary1.Models;
using ClassLibrary1.Enums;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class GameForm : Form
    {
        private readonly IGameLogicService? _gameLogicService;
        private readonly ISettingsRepository? _settingsRepository;
        private readonly IStatisticsRepository? _statisticsRepository;
        private readonly GameSettings? _settings;

        private GameSession? _session;
        private TableLayoutPanel? _boardPanel;
        private System.Windows.Forms.Timer? _timer;
        private Label? _lblMinesLeft;
        private Label? _lblTimer;

        public GameForm(IGameLogicService gameLogicService, ISettingsRepository settingsRepository, IStatisticsRepository statisticsRepository, GameSettings settings)
        {
            InitializeComponent();

            _gameLogicService = gameLogicService ?? throw new ArgumentNullException(nameof(gameLogicService));
            _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
            _statisticsRepository = statisticsRepository ?? throw new ArgumentNullException(nameof(statisticsRepository));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            Text = $"Minesweeper - {_settings.Difficulty}";

            Load += GameForm_Load;
            FormClosed += GameForm_FormClosed;
        }

        private void GameForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            _timer?.Stop();
            _timer?.Dispose();
        }

        private void GameForm_Load(object? sender, EventArgs e)
        {
            if (_settings is null || _gameLogicService is null)
            {
                Close();
                return;
            }

            var topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                Padding = new Padding(8),
                BackColor = SystemColors.Control
            };

            _lblMinesLeft = new Label
            {
                AutoSize = true,
                Location = new Point(8, 10),
                Text = $"Mines: {_settings.Mines}"
            };

            _lblTimer = new Label
            {
                AutoSize = true,
                Location = new Point(150, 10),
                Text = "Time: 0s"
            };

            topPanel.Controls.Add(_lblMinesLeft);
            topPanel.Controls.Add(_lblTimer);

            _boardPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = _settings.Columns,
                RowCount = _settings.Rows,
                Padding = new Padding(4),
                Margin = Padding.Empty
            };

            for (int c = 0; c < _settings.Columns; c++)
            {
                _boardPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / _settings.Columns));
            }

            for (int r = 0; r < _settings.Rows; r++)
            {
                _boardPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / _settings.Rows));
            }

            var content = new Panel { Dock = DockStyle.Fill, Padding = Padding.Empty };
            content.Controls.Add(_boardPanel);
            content.Controls.Add(topPanel);

            Controls.Add(content);

            StartNewSession();

            _timer = new System.Windows.Forms.Timer { Interval = 1000 };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_session is null) return;
            if (_session.StartedAt.HasValue && !_session.EndedAt.HasValue)
            {
                var elapsed = DateTimeOffset.UtcNow - _session.StartedAt.Value;
                _lblTimer!.Text = $"Time: {(int)elapsed.TotalSeconds}s";
            }
            else if (_session.StartedAt.HasValue && _session.EndedAt.HasValue)
            {
                var elapsed = _session.EndedAt.Value - _session.StartedAt.Value;
                _lblTimer!.Text = $"Time: {(int)elapsed.TotalSeconds}s";
            }
            else
            {
                _lblTimer!.Text = "Time: 0s";
            }
        }

        private void StartNewSession()
        {
            if (_settings is null || _gameLogicService is null) return;

            _session = _gameLogicService.StartNewGame(_settings);

            PopulateBoard();
            UpdateMinesLeft();
            _lblTimer!.Text = "Time: 0s";
        }

        private void PopulateBoard()
        {
            if (_boardPanel is null || _session is null) return;

            _boardPanel.SuspendLayout();
            _boardPanel.Controls.Clear();

            for (int r = 0; r < _session.Settings.Rows; r++)
            {
                for (int c = 0; c < _session.Settings.Columns; c++)
                {
                    var btn = new CellButton(r, c) { Margin = Padding.Empty };
                    btn.Tag = (r, c);
                    btn.MouseDown += CellButton_MouseDown;

                    var cell = _session.Board.GetCell(r, c);
                    btn.ApplyCell(cell, gameFinished: false);

                    _boardPanel.Controls.Add(btn, c, r);
                }
            }

            _boardPanel.ResumeLayout();
        }

        private void CellButton_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is not CellButton btn || _session is null || _gameLogicService is null) return;

            var (r, c) = ((int, int))btn.Tag!;

            if (e.Button == MouseButtons.Left)
            {
                _gameLogicService.RevealCell(_session, r, c);
            }
            else if (e.Button == MouseButtons.Right)
            {
                _gameLogicService.ToggleFlag(_session, r, c);
            }

            RefreshBoard();

            if (_session.Status == ClassLibrary1.Enums.GameStatus.Won)
            {
                HandleGameFinished(win: true);
            }
            else if (_session.Status == ClassLibrary1.Enums.GameStatus.Lost)
            {
                HandleGameFinished(win: false);
            }
            else
            {
                UpdateMinesLeft();
            }
        }

        private void RefreshBoard()
        {
            if (_boardPanel is null || _session is null) return;

            foreach (Control ctrl in _boardPanel.Controls)
            {
                if (ctrl is CellButton btn)
                {
                    var (r, c) = ((int, int))btn.Tag!;
                    var cell = _session.Board.GetCell(r, c);
                    btn.ApplyCell(cell, gameFinished: _session.Status is ClassLibrary1.Enums.GameStatus.Won or ClassLibrary1.Enums.GameStatus.Lost);
                }
            }
        }

        private void UpdateMinesLeft()
        {
            if (_lblMinesLeft is null || _session is null || _settings is null) return;

            int left = _settings.Mines - _session.FlaggedCellsCount;
            _lblMinesLeft.Text = $"Mines: {left}";
        }

        private async void HandleGameFinished(bool win)
        {
            if (_session is null) return;

            _timer?.Stop();
            RefreshBoard();
            UpdateMinesLeft();

            try
            {
                var stats = await _statisticsRepository!.LoadAsync() ?? new GameStatistics();

                if (win)
                {
                    int timeSeconds = (int)(_session.Duration?.TotalSeconds ?? 0);
                    stats.RegisterWin(timeSeconds, _session.Settings.Difficulty);
                }
                else
                {
                    stats.RegisterLoss();
                }

                await _statisticsRepository.SaveAsync(stats);
            }
            catch
            {
            }

            var message = win ? "You won!" : "You lost.";
            MessageBox.Show(this, message, "Game finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }
    }
}
