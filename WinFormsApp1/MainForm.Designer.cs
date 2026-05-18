namespace WinFormsApp1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.ComboBox cboDifficulty;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox grpStatistics;
        private System.Windows.Forms.Label lblGamesPlayed;
        private System.Windows.Forms.Label lblGamesPlayedValue;
        private System.Windows.Forms.Label lblGamesWon;
        private System.Windows.Forms.Label lblGamesWonValue;
        private System.Windows.Forms.Label lblGamesLost;
        private System.Windows.Forms.Label lblGamesLostValue;
        private System.Windows.Forms.Label lblBestTime;
        private System.Windows.Forms.Label lblBestTimeValue;
        // Per-difficulty best labels
        private System.Windows.Forms.Label lblBestEasy;
        private System.Windows.Forms.Label lblBestEasyValue;
        private System.Windows.Forms.Label lblBestMedium;
        private System.Windows.Forms.Label lblBestMediumValue;
        private System.Windows.Forms.Label lblBestHard;
        private System.Windows.Forms.Label lblBestHardValue;
        private System.Windows.Forms.Label lblWinStreak;
        private System.Windows.Forms.Label lblWinStreakValue;
        private System.Windows.Forms.Label lblBestWinStreak;
        private System.Windows.Forms.Label lblBestWinStreakValue;
        private System.Windows.Forms.Label lblLastPlayed;
        private System.Windows.Forms.Label lblLastPlayedValue;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 360);
            this.Text = "Minesweeper";

            // Difficulty combo
            this.cboDifficulty = new System.Windows.Forms.ComboBox();
            this.cboDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDifficulty.Items.AddRange(new object[] { "Easy", "Medium", "Hard" });
            this.cboDifficulty.SelectedIndex = 0;
            this.cboDifficulty.Location = new System.Drawing.Point(12, 12);
            this.cboDifficulty.Size = new System.Drawing.Size(120, 23);
            this.Controls.Add(this.cboDifficulty);

            // Start button
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStart.Text = "Start";
            this.btnStart.Location = new System.Drawing.Point(150, 10);
            this.btnStart.Size = new System.Drawing.Size(75, 26);
            this.Controls.Add(this.btnStart);

            // Statistics group
            this.grpStatistics = new System.Windows.Forms.GroupBox();
            this.grpStatistics.Text = "Statistics";
            this.grpStatistics.Location = new System.Drawing.Point(12, 50);
            this.grpStatistics.Size = new System.Drawing.Size(390, 300);

            int labelLeft = 12;
            int valueLeft = 200;
            int top = 22;
            int rowHeight = 22;

            this.lblGamesPlayed = new System.Windows.Forms.Label();
            this.lblGamesPlayed.Text = "Games played:";
            this.lblGamesPlayed.Location = new System.Drawing.Point(labelLeft, top);
            this.lblGamesPlayed.Size = new System.Drawing.Size(180, 20);
            this.grpStatistics.Controls.Add(this.lblGamesPlayed);

            this.lblGamesPlayedValue = new System.Windows.Forms.Label();
            this.lblGamesPlayedValue.Text = "0";
            this.lblGamesPlayedValue.Location = new System.Drawing.Point(valueLeft, top);
            this.lblGamesPlayedValue.Size = new System.Drawing.Size(160, 20);
            this.grpStatistics.Controls.Add(this.lblGamesPlayedValue);

            top += rowHeight;
            this.lblGamesWon = new System.Windows.Forms.Label();
            this.lblGamesWon.Text = "Games won:";
            this.lblGamesWon.Location = new System.Drawing.Point(labelLeft, top);
            this.lblGamesWon.Size = new System.Drawing.Size(180, 20);
            this.grpStatistics.Controls.Add(this.lblGamesWon);

            this.lblGamesWonValue = new System.Windows.Forms.Label();
            this.lblGamesWonValue.Text = "0";
            this.lblGamesWonValue.Location = new System.Drawing.Point(valueLeft, top);
            this.lblGamesWonValue.Size = new System.Drawing.Size(160, 20);
            this.grpStatistics.Controls.Add(this.lblGamesWonValue);

            top += rowHeight;
            this.lblGamesLost = new System.Windows.Forms.Label();
            this.lblGamesLost.Text = "Games lost:";
            this.lblGamesLost.Location = new System.Drawing.Point(labelLeft, top);
            this.lblGamesLost.Size = new System.Drawing.Size(180, 20);
            this.grpStatistics.Controls.Add(this.lblGamesLost);

            this.lblGamesLostValue = new System.Windows.Forms.Label();
            this.lblGamesLostValue.Text = "0";
            this.lblGamesLostValue.Location = new System.Drawing.Point(valueLeft, top);
            this.lblGamesLostValue.Size = new System.Drawing.Size(160, 20);
            this.grpStatistics.Controls.Add(this.lblGamesLostValue);

            top += rowHeight;
            this.lblBestTime = new System.Windows.Forms.Label();
            this.lblBestTime.Text = "Best time (overall):";
            this.lblBestTime.Location = new System.Drawing.Point(labelLeft, top);
            this.lblBestTime.Size = new System.Drawing.Size(180, 20);
            this.grpStatistics.Controls.Add(this.lblBestTime);

            this.lblBestTimeValue = new System.Windows.Forms.Label();
            this.lblBestTimeValue.Text = "-";
            this.lblBestTimeValue.Location = new System.Drawing.Point(valueLeft, top);
            this.lblBestTimeValue.Size = new System.Drawing.Size(160, 20);
            this.grpStatistics.Controls.Add(this.lblBestTimeValue);

            // Per-difficulty bests
            top += rowHeight;
            this.lblBestEasy = new System.Windows.Forms.Label();
            this.lblBestEasy.Text = "Best (Easy):";
            this.lblBestEasy.Location = new System.Drawing.Point(labelLeft, top);
            this.lblBestEasy.Size = new System.Drawing.Size(180, 20);
            this.grpStatistics.Controls.Add(this.lblBestEasy);

            this.lblBestEasyValue = new System.Windows.Forms.Label();
            this.lblBestEasyValue.Text = "-";
            this.lblBestEasyValue.Location = new System.Drawing.Point(valueLeft, top);
            this.lblBestEasyValue.Size = new System.Drawing.Size(160, 20);
            this.grpStatistics.Controls.Add(this.lblBestEasyValue);

            top += rowHeight;
            this.lblBestMedium = new System.Windows.Forms.Label();
            this.lblBestMedium.Text = "Best (Medium):";
            this.lblBestMedium.Location = new System.Drawing.Point(labelLeft, top);
            this.lblBestMedium.Size = new System.Drawing.Size(180, 20);
            this.grpStatistics.Controls.Add(this.lblBestMedium);

            this.lblBestMediumValue = new System.Windows.Forms.Label();
            this.lblBestMediumValue.Text = "-";
            this.lblBestMediumValue.Location = new System.Drawing.Point(valueLeft, top);
            this.lblBestMediumValue.Size = new System.Drawing.Size(160, 20);
            this.grpStatistics.Controls.Add(this.lblBestMediumValue);

            top += rowHeight;
            this.lblBestHard = new System.Windows.Forms.Label();
            this.lblBestHard.Text = "Best (Hard):";
            this.lblBestHard.Location = new System.Drawing.Point(labelLeft, top);
            this.lblBestHard.Size = new System.Drawing.Size(180, 20);
            this.grpStatistics.Controls.Add(this.lblBestHard);

            this.lblBestHardValue = new System.Windows.Forms.Label();
            this.lblBestHardValue.Text = "-";
            this.lblBestHardValue.Location = new System.Drawing.Point(valueLeft, top);
            this.lblBestHardValue.Size = new System.Drawing.Size(160, 20);
            this.grpStatistics.Controls.Add(this.lblBestHardValue);

            top += rowHeight;
            this.lblWinStreak = new System.Windows.Forms.Label();
            this.lblWinStreak.Text = "Current win streak:";
            this.lblWinStreak.Location = new System.Drawing.Point(labelLeft, top);
            this.lblWinStreak.Size = new System.Drawing.Size(180, 20);
            this.grpStatistics.Controls.Add(this.lblWinStreak);

            this.lblWinStreakValue = new System.Windows.Forms.Label();
            this.lblWinStreakValue.Text = "0";
            this.lblWinStreakValue.Location = new System.Drawing.Point(valueLeft, top);
            this.lblWinStreakValue.Size = new System.Drawing.Size(160, 20);
            this.grpStatistics.Controls.Add(this.lblWinStreakValue);

            top += rowHeight;
            this.lblBestWinStreak = new System.Windows.Forms.Label();
            this.lblBestWinStreak.Text = "Best win streak:";
            this.lblBestWinStreak.Location = new System.Drawing.Point(labelLeft, top);
            this.lblBestWinStreak.Size = new System.Drawing.Size(180, 20);
            this.grpStatistics.Controls.Add(this.lblBestWinStreak);

            this.lblBestWinStreakValue = new System.Windows.Forms.Label();
            this.lblBestWinStreakValue.Text = "0";
            this.lblBestWinStreakValue.Location = new System.Drawing.Point(valueLeft, top);
            this.lblBestWinStreakValue.Size = new System.Drawing.Size(160, 20);
            this.grpStatistics.Controls.Add(this.lblBestWinStreakValue);

            top += rowHeight;
            this.lblLastPlayed = new System.Windows.Forms.Label();
            this.lblLastPlayed.Text = "Last played:";
            this.lblLastPlayed.Location = new System.Drawing.Point(labelLeft, top);
            this.lblLastPlayed.Size = new System.Drawing.Size(180, 20);
            this.grpStatistics.Controls.Add(this.lblLastPlayed);

            this.lblLastPlayedValue = new System.Windows.Forms.Label();
            this.lblLastPlayedValue.Text = "-";
            this.lblLastPlayedValue.Location = new System.Drawing.Point(valueLeft, top);
            this.lblLastPlayedValue.Size = new System.Drawing.Size(160, 20);
            this.grpStatistics.Controls.Add(this.lblLastPlayedValue);

            this.Controls.Add(this.grpStatistics);
        }

        #endregion
    }
}