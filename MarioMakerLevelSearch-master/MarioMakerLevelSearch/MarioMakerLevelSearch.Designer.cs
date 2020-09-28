namespace MarioMakerLevelSearch
{
    partial class MarioMakerLevelSearch
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.TextEntriesChecked = new System.Windows.Forms.Label();
            this.TextEntriesAdded = new System.Windows.Forms.Label();
            this.SortingBox = new System.Windows.Forms.ComboBox();
            this.PictureBoxLevelThumbnail = new System.Windows.Forms.PictureBox();
            this.TextTitle = new System.Windows.Forms.Label();
            this.TextAuthor = new System.Windows.Forms.Label();
            this.TextGameStyle = new System.Windows.Forms.Label();
            this.TextDifficulty = new System.Windows.Forms.Label();
            this.TextClearRate = new System.Windows.Forms.Label();
            this.TextStars = new System.Windows.Forms.Label();
            this.TextPlayersPlayed = new System.Windows.Forms.Label();
            this.TextRegion = new System.Windows.Forms.Label();
            this.TextTag = new System.Windows.Forms.Label();
            this.TextID = new System.Windows.Forms.Label();
            this.PictureBoxFullCourse = new System.Windows.Forms.PictureBox();
            this.PageBox = new System.Windows.Forms.ComboBox();
            this.PageNumberBox = new System.Windows.Forms.ComboBox();
            this.LoadCSV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLevelThumbnail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFullCourse)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(448, 399);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 414);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "GetData";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(634, 414);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(821, 414);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "SaveXML";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(902, 414);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "SaveCSV";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(553, 414);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Pause Data";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // TextEntriesChecked
            // 
            this.TextEntriesChecked.AutoSize = true;
            this.TextEntriesChecked.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TextEntriesChecked.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TextEntriesChecked.Location = new System.Drawing.Point(358, 5);
            this.TextEntriesChecked.Name = "TextEntriesChecked";
            this.TextEntriesChecked.Size = new System.Drawing.Size(103, 13);
            this.TextEntriesChecked.TabIndex = 6;
            this.TextEntriesChecked.Text = "TextEntriesChecked";
            this.TextEntriesChecked.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TextEntriesChecked.Click += new System.EventHandler(this.Label1_Click);
            // 
            // TextEntriesAdded
            // 
            this.TextEntriesAdded.AutoSize = true;
            this.TextEntriesAdded.Location = new System.Drawing.Point(358, 20);
            this.TextEntriesAdded.Name = "TextEntriesAdded";
            this.TextEntriesAdded.Size = new System.Drawing.Size(91, 13);
            this.TextEntriesAdded.TabIndex = 7;
            this.TextEntriesAdded.Text = "TextEntriesAdded";
            this.TextEntriesAdded.Click += new System.EventHandler(this.Label2_Click);
            // 
            // SortingBox
            // 
            this.SortingBox.FormattingEnabled = true;
            this.SortingBox.Location = new System.Drawing.Point(13, 8);
            this.SortingBox.Name = "SortingBox";
            this.SortingBox.Size = new System.Drawing.Size(131, 21);
            this.SortingBox.TabIndex = 8;
            this.SortingBox.Text = "Sorting";
            this.SortingBox.SelectedIndexChanged += new System.EventHandler(this.SortingBox_SelectedIndexChanged);
            // 
            // PictureBoxLevelThumbnail
            // 
            this.PictureBoxLevelThumbnail.Location = new System.Drawing.Point(471, 38);
            this.PictureBoxLevelThumbnail.Name = "PictureBoxLevelThumbnail";
            this.PictureBoxLevelThumbnail.Size = new System.Drawing.Size(320, 240);
            this.PictureBoxLevelThumbnail.TabIndex = 9;
            this.PictureBoxLevelThumbnail.TabStop = false;
            // 
            // TextTitle
            // 
            this.TextTitle.AutoSize = true;
            this.TextTitle.Location = new System.Drawing.Point(797, 67);
            this.TextTitle.Name = "TextTitle";
            this.TextTitle.Size = new System.Drawing.Size(27, 13);
            this.TextTitle.TabIndex = 10;
            this.TextTitle.Text = "Title";
            this.TextTitle.Click += new System.EventHandler(this.TextTitle_Click);
            // 
            // TextAuthor
            // 
            this.TextAuthor.AutoSize = true;
            this.TextAuthor.Location = new System.Drawing.Point(797, 85);
            this.TextAuthor.Name = "TextAuthor";
            this.TextAuthor.Size = new System.Drawing.Size(38, 13);
            this.TextAuthor.TabIndex = 11;
            this.TextAuthor.Text = "Author";
            this.TextAuthor.Click += new System.EventHandler(this.TextAuthor_Click);
            // 
            // TextGameStyle
            // 
            this.TextGameStyle.AutoSize = true;
            this.TextGameStyle.Location = new System.Drawing.Point(797, 157);
            this.TextGameStyle.Name = "TextGameStyle";
            this.TextGameStyle.Size = new System.Drawing.Size(58, 13);
            this.TextGameStyle.TabIndex = 12;
            this.TextGameStyle.Text = "GameStyle";
            this.TextGameStyle.Click += new System.EventHandler(this.TextTheme_Click);
            // 
            // TextDifficulty
            // 
            this.TextDifficulty.AutoSize = true;
            this.TextDifficulty.Location = new System.Drawing.Point(797, 103);
            this.TextDifficulty.Name = "TextDifficulty";
            this.TextDifficulty.Size = new System.Drawing.Size(47, 13);
            this.TextDifficulty.TabIndex = 13;
            this.TextDifficulty.Text = "Difficulty";
            this.TextDifficulty.Click += new System.EventHandler(this.TextDifficulty_Click);
            // 
            // TextClearRate
            // 
            this.TextClearRate.AutoSize = true;
            this.TextClearRate.Location = new System.Drawing.Point(797, 121);
            this.TextClearRate.Name = "TextClearRate";
            this.TextClearRate.Size = new System.Drawing.Size(57, 13);
            this.TextClearRate.TabIndex = 14;
            this.TextClearRate.Text = "Clear Rate";
            this.TextClearRate.Click += new System.EventHandler(this.TextClearRate_Click);
            // 
            // TextStars
            // 
            this.TextStars.AutoSize = true;
            this.TextStars.Location = new System.Drawing.Point(797, 139);
            this.TextStars.Name = "TextStars";
            this.TextStars.Size = new System.Drawing.Size(31, 13);
            this.TextStars.TabIndex = 15;
            this.TextStars.Text = "Stars";
            this.TextStars.Click += new System.EventHandler(this.TextStars_Click);
            // 
            // TextPlayersPlayed
            // 
            this.TextPlayersPlayed.AutoSize = true;
            this.TextPlayersPlayed.Location = new System.Drawing.Point(797, 175);
            this.TextPlayersPlayed.Name = "TextPlayersPlayed";
            this.TextPlayersPlayed.Size = new System.Drawing.Size(76, 13);
            this.TextPlayersPlayed.TabIndex = 16;
            this.TextPlayersPlayed.Text = "Players Played";
            this.TextPlayersPlayed.Click += new System.EventHandler(this.TextPlayersPlayed_Click);
            // 
            // TextRegion
            // 
            this.TextRegion.AutoSize = true;
            this.TextRegion.Location = new System.Drawing.Point(797, 193);
            this.TextRegion.Name = "TextRegion";
            this.TextRegion.Size = new System.Drawing.Size(41, 13);
            this.TextRegion.TabIndex = 17;
            this.TextRegion.Text = "Region";
            this.TextRegion.Click += new System.EventHandler(this.TextRegion_Click);
            // 
            // TextTag
            // 
            this.TextTag.AutoSize = true;
            this.TextTag.Location = new System.Drawing.Point(797, 211);
            this.TextTag.Name = "TextTag";
            this.TextTag.Size = new System.Drawing.Size(26, 13);
            this.TextTag.TabIndex = 18;
            this.TextTag.Text = "Tag";
            // 
            // TextID
            // 
            this.TextID.AutoSize = true;
            this.TextID.Location = new System.Drawing.Point(797, 229);
            this.TextID.Name = "TextID";
            this.TextID.Size = new System.Drawing.Size(18, 13);
            this.TextID.TabIndex = 19;
            this.TextID.Text = "ID";
            this.TextID.Click += new System.EventHandler(this.TextID_Click);
            // 
            // PictureBoxFullCourse
            // 
            this.PictureBoxFullCourse.Location = new System.Drawing.Point(471, 284);
            this.PictureBoxFullCourse.Name = "PictureBoxFullCourse";
            this.PictureBoxFullCourse.Size = new System.Drawing.Size(506, 110);
            this.PictureBoxFullCourse.TabIndex = 20;
            this.PictureBoxFullCourse.TabStop = false;
            this.PictureBoxFullCourse.Click += new System.EventHandler(this.PictureBoxFullCourse_Click);
            // 
            // PageBox
            // 
            this.PageBox.FormattingEnabled = true;
            this.PageBox.Location = new System.Drawing.Point(150, 8);
            this.PageBox.Name = "PageBox";
            this.PageBox.Size = new System.Drawing.Size(87, 21);
            this.PageBox.TabIndex = 21;
            this.PageBox.Text = "Page";
            this.PageBox.SelectedIndexChanged += new System.EventHandler(this.PageBox_SelectedIndexChanged);
            // 
            // PageNumberBox
            // 
            this.PageNumberBox.FormattingEnabled = true;
            this.PageNumberBox.Location = new System.Drawing.Point(243, 8);
            this.PageNumberBox.Name = "PageNumberBox";
            this.PageNumberBox.Size = new System.Drawing.Size(109, 21);
            this.PageNumberBox.TabIndex = 22;
            this.PageNumberBox.Text = "EntryPerPage";
            this.PageNumberBox.SelectedIndexChanged += new System.EventHandler(this.PageNumberBox_SelectedIndexChanged);
            // 
            // LoadCSV
            // 
            this.LoadCSV.Location = new System.Drawing.Point(740, 414);
            this.LoadCSV.Name = "LoadCSV";
            this.LoadCSV.Size = new System.Drawing.Size(75, 23);
            this.LoadCSV.TabIndex = 23;
            this.LoadCSV.Text = "LoadCSV";
            this.LoadCSV.UseVisualStyleBackColor = true;
            this.LoadCSV.Click += new System.EventHandler(this.LoadCSV_Click);
            // 
            // MarioMakerLevelSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 449);
            this.Controls.Add(this.LoadCSV);
            this.Controls.Add(this.PageNumberBox);
            this.Controls.Add(this.PageBox);
            this.Controls.Add(this.PictureBoxFullCourse);
            this.Controls.Add(this.TextID);
            this.Controls.Add(this.TextTag);
            this.Controls.Add(this.TextRegion);
            this.Controls.Add(this.TextPlayersPlayed);
            this.Controls.Add(this.TextStars);
            this.Controls.Add(this.TextClearRate);
            this.Controls.Add(this.TextDifficulty);
            this.Controls.Add(this.TextGameStyle);
            this.Controls.Add(this.TextAuthor);
            this.Controls.Add(this.TextTitle);
            this.Controls.Add(this.PictureBoxLevelThumbnail);
            this.Controls.Add(this.SortingBox);
            this.Controls.Add(this.TextEntriesAdded);
            this.Controls.Add(this.TextEntriesChecked);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.Name = "MarioMakerLevelSearch";
            this.Text = "Mario Maker Level Search";
            this.Load += new System.EventHandler(this.MarioMakerLevelSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLevelThumbnail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFullCourse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label TextEntriesChecked;
        private System.Windows.Forms.Label TextEntriesAdded;
        private System.Windows.Forms.ComboBox SortingBox;
        private System.Windows.Forms.PictureBox PictureBoxLevelThumbnail;
        private System.Windows.Forms.Label TextTitle;
        private System.Windows.Forms.Label TextAuthor;
        private System.Windows.Forms.Label TextGameStyle;
        private System.Windows.Forms.Label TextDifficulty;
        private System.Windows.Forms.Label TextClearRate;
        private System.Windows.Forms.Label TextStars;
        private System.Windows.Forms.Label TextPlayersPlayed;
        private System.Windows.Forms.Label TextRegion;
        private System.Windows.Forms.Label TextTag;
        private System.Windows.Forms.Label TextID;
        private System.Windows.Forms.PictureBox PictureBoxFullCourse;
        private System.Windows.Forms.ComboBox PageBox;
        private System.Windows.Forms.ComboBox PageNumberBox;
        private System.Windows.Forms.Button LoadCSV;
    }
}

