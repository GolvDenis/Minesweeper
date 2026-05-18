using ClassLibrary1.Data;
using ClassLibrary1.Interfaces;
using ClassLibrary1.Services;

namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            IGameBoardGenerator boardGenerator = new GameBoardGenerator();
            IGameLogicService gameLogicService = new GameLogicService(boardGenerator);
            ISettingsRepository settingsRepository = new JsonSettingsRepository();
            IStatisticsRepository statisticsRepository = new JsonStatisticsRepository();

            Application.Run(new MainForm(gameLogicService, settingsRepository, statisticsRepository));
        }
    }
}