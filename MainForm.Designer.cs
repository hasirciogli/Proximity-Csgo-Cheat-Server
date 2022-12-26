namespace RogsoftwareServer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.serverStopButton = new System.Windows.Forms.Button();
            this.serverStartButton = new System.Windows.Forms.Button();
            this.column_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_username = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_steam_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.usersListView = new System.Windows.Forms.ListView();
            this.commandSendButton = new System.Windows.Forms.Button();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.commandBox = new System.Windows.Forms.TextBox();
            this.connectedClientsCheckerTimer = new System.Windows.Forms.Timer(this.components);
            this.clearLogBoxButton = new System.Windows.Forms.Button();
            this.column_userUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_steam_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // serverStopButton
            // 
            this.serverStopButton.Location = new System.Drawing.Point(1110, 456);
            this.serverStopButton.Name = "serverStopButton";
            this.serverStopButton.Size = new System.Drawing.Size(75, 29);
            this.serverStopButton.TabIndex = 6;
            this.serverStopButton.Text = "STOP";
            this.serverStopButton.UseVisualStyleBackColor = true;
            this.serverStopButton.Click += new System.EventHandler(this.serverStopButton_Click);
            // 
            // serverStartButton
            // 
            this.serverStartButton.Location = new System.Drawing.Point(1029, 456);
            this.serverStartButton.Name = "serverStartButton";
            this.serverStartButton.Size = new System.Drawing.Size(75, 29);
            this.serverStartButton.TabIndex = 7;
            this.serverStartButton.Text = "START";
            this.serverStartButton.UseVisualStyleBackColor = true;
            this.serverStartButton.Click += new System.EventHandler(this.serverStartButton_Click);
            // 
            // column_id
            // 
            this.column_id.Text = "ID";
            this.column_id.Width = 30;
            // 
            // column_username
            // 
            this.column_username.Text = "Username";
            this.column_username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_username.Width = 100;
            // 
            // column_steam_id
            // 
            this.column_steam_id.Text = "Steam ID";
            this.column_steam_id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_steam_id.Width = 150;
            // 
            // usersListView
            // 
            this.usersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_id,
            this.column_userUID,
            this.column_username,
            this.column_steam_id,
            this.column_steam_name});
            this.usersListView.HideSelection = false;
            this.usersListView.Location = new System.Drawing.Point(652, 12);
            this.usersListView.Name = "usersListView";
            this.usersListView.Size = new System.Drawing.Size(533, 437);
            this.usersListView.TabIndex = 5;
            this.usersListView.UseCompatibleStateImageBehavior = false;
            this.usersListView.View = System.Windows.Forms.View.Details;
            // 
            // commandSendButton
            // 
            this.commandSendButton.Location = new System.Drawing.Point(551, 456);
            this.commandSendButton.Name = "commandSendButton";
            this.commandSendButton.Size = new System.Drawing.Size(94, 29);
            this.commandSendButton.TabIndex = 8;
            this.commandSendButton.Text = "Send";
            this.commandSendButton.UseVisualStyleBackColor = true;
            this.commandSendButton.Click += new System.EventHandler(this.commandSendButton_Click_1);
            // 
            // LogBox
            // 
            this.LogBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.LogBox.Location = new System.Drawing.Point(12, 13);
            this.LogBox.Name = "LogBox";
            this.LogBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.LogBox.Size = new System.Drawing.Size(632, 437);
            this.LogBox.TabIndex = 9;
            this.LogBox.Text = "";
            this.LogBox.TextChanged += new System.EventHandler(this.LogBox_TextChanged);
            // 
            // commandBox
            // 
            this.commandBox.Location = new System.Drawing.Point(13, 461);
            this.commandBox.Name = "commandBox";
            this.commandBox.Size = new System.Drawing.Size(532, 20);
            this.commandBox.TabIndex = 10;
            // 
            // connectedClientsCheckerTimer
            // 
            this.connectedClientsCheckerTimer.Enabled = true;
            this.connectedClientsCheckerTimer.Interval = 300;
            this.connectedClientsCheckerTimer.Tick += new System.EventHandler(this.connectedClientsCheckerTimer_Tick);
            // 
            // clearLogBoxButton
            // 
            this.clearLogBoxButton.Location = new System.Drawing.Point(651, 456);
            this.clearLogBoxButton.Name = "clearLogBoxButton";
            this.clearLogBoxButton.Size = new System.Drawing.Size(96, 29);
            this.clearLogBoxButton.TabIndex = 11;
            this.clearLogBoxButton.Text = "Clear All Logs";
            this.clearLogBoxButton.UseVisualStyleBackColor = true;
            this.clearLogBoxButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // column_userUID
            // 
            this.column_userUID.Text = "UID";
            // 
            // column_steam_name
            // 
            this.column_steam_name.Text = "Steam Name";
            this.column_steam_name.Width = 160;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1197, 494);
            this.Controls.Add(this.clearLogBoxButton);
            this.Controls.Add(this.commandBox);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.commandSendButton);
            this.Controls.Add(this.serverStartButton);
            this.Controls.Add(this.serverStopButton);
            this.Controls.Add(this.usersListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button serverStopButton;
        private System.Windows.Forms.Button serverStartButton;
        private System.Windows.Forms.ColumnHeader column_id;
        private System.Windows.Forms.ColumnHeader column_username;
        private System.Windows.Forms.ColumnHeader column_steam_id;
        internal System.Windows.Forms.ListView usersListView;
        private System.Windows.Forms.Button commandSendButton;
        private System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.TextBox commandBox;
        internal System.Windows.Forms.Timer connectedClientsCheckerTimer;
        private System.Windows.Forms.Button clearLogBoxButton;
        private System.Windows.Forms.ColumnHeader column_userUID;
        private System.Windows.Forms.ColumnHeader column_steam_name;
    }
}

