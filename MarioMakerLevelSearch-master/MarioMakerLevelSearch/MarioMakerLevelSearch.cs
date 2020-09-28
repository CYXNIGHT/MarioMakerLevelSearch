using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarioMakerLevelSearch
{
    public partial class MarioMakerLevelSearch : Form
    {
        List<MarioMakerLevel> marioMakerLevels = new List<MarioMakerLevel>();
        MarioMakerLevel[] shownLevels;
        int levelsChecked = 0;
        int maxRowCount = 2;
        int pagenumber = 0;
        int[] searchValues; // To keep track of where we are up to when searching
        Thread thread;
        Thread sapThread;

        public MarioMakerLevelSearch()
        {
            InitializeComponent();

            shownLevels = new MarioMakerLevel[maxRowCount]; // Make the shown level size equal to maxRowCount

            ResetAll();
            ResetPageNumberBox();

            // Make box non resizable
            MinimizeBox = true;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            thread = new Thread(GetAllData); // Create Thread which when called calls GetAllData
            thread.IsBackground = true; // Make the thread run in background (Stop the application from running when closed for some reason)

            TextEntriesChecked.Text = ""; // Change text to nothing
            TextEntriesAdded.Text = ""; // Change text to nothing
        }



        private void MarioMakerLevelSearch_Load(object sender, EventArgs e)
        {

        }

        private void Datagridview1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //if ((sender is DataGridView) && (e != null))
            //    ((DataGridView)sender).Rows[e.RowIndex].Visible = !(e.RowIndex > MaxRowCount);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread.IsAlive)
                thread.Abort(); // Abort the thread if running

            // Increasingly forceful exit statements
            Application.Exit();
            Environment.Exit(0);
            Process.GetCurrentProcess().Kill();
            Environment.FailFast("Forced Closed");
        }


        private void GetAllData()
        {
            WebsiteSearch webSearch = new WebsiteSearch();

            // Loop through all permutations of searching
            for (int o = 0; o < webSearch.uploadDates.Length; o++)
                for (int n = 0; n < 16; n++)
                    for (int m = 0; m < webSearch.difficulties.Length; m++)
                        for (int l = 0; l < webSearch.regions.Length; l++)
                            for (int k = 0; k < webSearch.courseThemes.Length; k++)
                                for (int j = 0; j < webSearch.gameStyles.Length; j++)
                                {
                                    for (int i = 0; i < 10; i++)
                                    {
                                        List<MarioMakerLevel> tempLevels = new List<MarioMakerLevel>();
                                        bool hasNull = false;

                                        foreach (var level in webSearch.GetLevels(i, j, k, l, m, n, o))
                                        {
                                            //level.ID = marioMakerLevels.Count;
                                            level.ID = level.Link.Substring(level.Link.Length - 19); // Copy the last 19 characters (Result example 0000-0000-0000-0000)

                                            hasNull = checkHasNull(level); // Check if the level has a null value (This causes the data to be shifted)
                                            tempLevels.Add(level); // Add level to temp

                                            levelsChecked += 1; // Add one to levels checked
                                        }

                                        //if (!hasNull) // If it didn't find null in previous search
                                        //    foreach (var level in tempLevels) // Loop through levels
                                        //    {
                                        //        bool exists = false; // To check if the entry already exists
                                        //        foreach (var lev in marioMakerLevels) // Loop through entries collected
                                        //            if (lev.ID == level.ID) // If the entry already exists then make exists equal to true
                                        //                exists = true;
                                        
                                        //        if (!exists) // If the entry doesn't exist then add it
                                        //            marioMakerLevels.Add(level);
                                        //    }

                                        // This does the same as the commented code above
                                        if (!hasNull)
                                            marioMakerLevels.AddRange(tempLevels.Where(p => marioMakerLevels.Where(q => q.ID == p.ID).ToArray().Length == 0));

                                        searchValues = new int[] {i, j, k, l, m, n, o};
                                        Thread.Sleep(1500); // Sleep for 1.5 seconds, if we do this too fast Nintendo will block access
                                    }
                                    
                                }
        }

        private bool checkHasNull(MarioMakerLevel level)
        {
            foreach (var prop in typeof(MarioMakerLevel).GetProperties()) // Foreach property
                if (prop.GetValue(level) == null) // Check if null
                {
                    Console.WriteLine(prop.GetValue(level));
                    return true; // If null return true
                }
                    

            return false; // If none found null, return false
        }

        // Get some of dat data dude
        private void GetSomeData() // Placeholder to check if data sniffing worked
        {
            WebsiteSearch webSearch = new WebsiteSearch();
            for (int i = 0; i < 1; i++)
            {
                foreach (var level in webSearch.GetLevels(i, 0, 0, 0, 0, 0, 0))
                {
                    level.ID = level.Link.Substring(level.Link.Length - 19);
                    marioMakerLevels.Add(level);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //GetSomeData();
            if (!thread.IsAlive) // If thread is not alive
            {
                //marioMakerLevels = new List<MarioMakerLevel>(); // Create a new list of MarioMakerLevel's
                levelsChecked = 0; // Reset levelsChecked
                thread.Start(); // Create a new thread of data sniffing
            }
            else // If thread alive
            {
                thread.Suspend();
                thread.Resume(); // Resume thread
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetAll(); // ResetAll...
        }

        private void ResetAll()
        {
            ResetDataSource(); // Reset datasource of datagridview
            ResetSortingBox(); // Reset sortfd oksmf
            ResetPageBox(); // Rseaodn  oad
            ResetShownLevels(pagenumber); // afijf

            // You get the picture, it resets stuff
        }

        private void ResetPageBox()
        {
            PageBox.Items.Clear(); // Clear combobox

            PageBox.Items.Add(1);

            for (int i = 0; i < Math.Ceiling(marioMakerLevels.Count * 1.0f / maxRowCount) - 1; i++)
                PageBox.Items.Add((i + 2).ToString());

            if (marioMakerLevels.Count != 0)
                if (PageBox.Items.Count >= 0)
                {
                    if (PageBox.Items.Count - 1 < pagenumber)
                        pagenumber = PageBox.Items.Count - 1;
                    else
                        pagenumber = 0;
                    PageBox.SelectedIndex = pagenumber;
                }
                else
                    PageBox.SelectedIndex = 0;
            // I honestly don't think these are different, should of put a comment here before...
            // Yeah, they're the same...
            // I mean, you can remove it if you want
            //if (marioMakerLevels.Count != 0)
            //    if (PageBox.Items.Count >= 0)
            //    {
            //        if (PageBox.Items.Count - 1 < pagenumber)
            //            pagenumber = PageBox.Items.Count - 1;
            //        else
            //            pagenumber = 0;
            //        PageBox.SelectedIndex = pagenumber;
            //    }
            //    else
            //        PageBox.SelectedIndex = 0;


        }

        private void ResetDataSource()
        {
            var source = new BindingSource(); // Create a new binding source
            source.DataSource = shownLevels; // Make the bindingsource datasource to shownLevels
            dataGridView1.DataSource = source; // Make the datagridviews datasource this created source
        }

        private void ResetSortingBox()
        {
            SortingBox.Items.Clear(); // Clear combobox
            foreach (var prop in typeof(MarioMakerLevel).GetProperties())
                SortingBox.Items.Add(prop.Name); // Add all properties to the combobox
        }

        private void ResetShownLevels(int page)
        {
            // For the length of shownLevels, add the corresponding MarioMakerLevel in marioMakerLevels
            for (int i = 0; i < shownLevels.Length; i++)
            {
                int index = page * shownLevels.Length + i;

                if (marioMakerLevels.Count > index && marioMakerLevels.Count != 0)
                    shownLevels[i] = marioMakerLevels[index];
                else
                    shownLevels[i] = null;

            }

            TextEntriesChecked.Text = $"{levelsChecked} Entries checked"; // Change the text to show how many entries have been checked
            TextEntriesAdded.Text = $"{marioMakerLevels.Count} Entries Added"; // Change the text to show how many entries have been added
        }

        private void ResetPageNumberBox()
        {
            // Fucken add as many as you like!! Make one a million, that would be fun for your ram and processor!!!
            PageNumberBox.Items.Add("10");
            PageNumberBox.Items.Add("20");
            PageNumberBox.Items.Add("50");
            PageNumberBox.Items.Add("100");
            PageNumberBox.Items.Add("250");
            PageNumberBox.Items.Add("500");
            PageNumberBox.Items.Add("1000");

            PageNumberBox.SelectedValue = "50";
            PageNumberBox.SelectedIndex = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            thread.Suspend(); // Suspend thread...
            //thread.Abort();
        }

        #region Label Clicks, I hate these

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextTitle_Click(object sender, EventArgs e)
        {

        }

        private void TextTheme_Click(object sender, EventArgs e)
        {

        }

        private void TextAuthor_Click(object sender, EventArgs e)
        {

        }

        private void TextDifficulty_Click(object sender, EventArgs e)
        {

        }

        private void TextClearRate_Click(object sender, EventArgs e)
        {

        }

        private void TextStars_Click(object sender, EventArgs e)
        {

        }

        private void TextPlayersPlayed_Click(object sender, EventArgs e)
        {

        }

        private void TextRegion_Click(object sender, EventArgs e)
        {

        }

        private void TextID_Click(object sender, EventArgs e)
        {

        }

        private void ErrorText_Click(object sender, EventArgs e)
        {

        }

        #endregion // I hate these things

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;

            if (col > 0 && col < dataGridView1.Width && row > 0 && row < dataGridView1.Height) // If the cell is within the grid (for some reason pressing the column name caused an out of bounds error \_(ツ)_/ )
            {

                //if (col < 0) col = 0; else if (col > dataGridView1.Width) col = dataGridView1.Width; 
                //if (row < 0) row = 0; else if (row > dataGridView1.Height) row = dataGridView1.Height; 

                var cell = ((DataGridView)sender)[col, row];

                // This just grabs the data from the cell clicked, and puts it into the text objects
                if (cell.Value != null)
                {
                    DataGridViewRow rows = cell.OwningRow;
                    WebsiteSearch web = new WebsiteSearch();

                    TextTitle.Text = "Title - " + rows.Cells["Title"].Value.ToString();
                    TextAuthor.Text = "Author - " + rows.Cells["Author"].Value.ToString();
                    TextDifficulty.Text = "Difficulty - " + rows.Cells["Difficulty"].Value.ToString();
                    TextClearRate.Text = "Clear Rate - " + rows.Cells["ClearRate"].Value.ToString();
                    TextStars.Text = "Stars - " + rows.Cells["Stars"].Value.ToString();
                    var index = Array.FindIndex(web.gameStylesShort, i => i == rows.Cells["GameStyle"].Value.ToString());
                    TextGameStyle.Text = "Game Style - " + web.gameStylesLong[index];
                    //TextGameStyle.Text = "GameStyle - " + rows.Cells["GameStyle"].Value.ToString();
                    TextPlayersPlayed.Text = "Players Played - " + rows.Cells["PlayersPlayed"].Value.ToString();
                    TextRegion.Text = "Region - " + rows.Cells["Flag"].Value.ToString();
                    TextTag.Text = "Tag - " + rows.Cells["Tag"].Value.ToString();
                    TextID.Text = "ID - " + rows.Cells["ID"].Value.ToString();

                    //StartGetPictureThread(rows.Cells["Link"].Value.ToString(), "course-image", PictureBoxLevelThumbnail);
                    //StartGetPictureThread(rows.Cells["Link"].Value.ToString(), "course-image-full", PictureBoxFullCourse);
                    // Replaced the previous two with a single one, therefore I just need to run one thread! (Also, accessing a site two times that quickly makes it a bit suspish, shhh)
                    StartSetAllPicturesThread(rows.Cells["Link"].Value.ToString(), "course-image", PictureBoxLevelThumbnail, "course-image-full", PictureBoxFullCourse);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public void StartSetPictureThread(string link, string className, PictureBox picBox)
        {
            // This shit can go!
            var t1 = new Thread(() => SetPicture(link, className, picBox));
            t1.Start();
        }

        private void SetPicture(string link, string className, PictureBox picBox)
        {
            // This can go to

            WebsiteSearch web = new WebsiteSearch();
            // Get Image link
            string imageLink = web.GetPictureLink(link, className);

            // Load images if they return a value
            Console.WriteLine(imageLink);
            if (imageLink != null)
                picBox.Load(imageLink);
        }

        public void StartSetAllPicturesThread(string link, string className1, PictureBox picBox1, string className2, PictureBox picBox2)
        {
            if (sapThread != null) if (sapThread.IsAlive) sapThread.Abort();

            // Create a thread that loads the pictures
            sapThread = new Thread(() => SetAllPictures(link, className1, picBox1, className2, picBox2));
            sapThread.Start();
        }

        private void SetAllPictures(string link, string className1, PictureBox picBox1, string className2, PictureBox picBox2)
        {
            Thread.Sleep(1000); //

            WebsiteSearch web = new WebsiteSearch();
            // Get links
            string imageLink1 = web.GetPictureLink(link, className1);
            string imageLink2 = web.GetPictureLink(link, className2);

            // Load images if they return a value
            if (imageLink1 != null)
                picBox1.Load(imageLink1);

            if (imageLink2 != null)
                picBox2.Load(imageLink2);
        }

        private void SortingBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MarioMakerLevel item = new MarioMakerLevel();

            ComboBox comboBox = (ComboBox)sender; // Create object using sender
            var sortingValue = (string)SortingBox.SelectedItem; // Turn the comboBox selected item into a string

            item.SortList(marioMakerLevels, sortingValue); // Sort the list by the sortingValue

            ResetShownLevels(pagenumber);
            ResetDataSource();
            ResetPageBox();
        }

        private void PictureBoxFullCourse_Click(object sender, EventArgs e)
        {

        }

        private void PageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            pagenumber = comboBox.SelectedIndex;
            ResetShownLevels(pagenumber);
            ResetDataSource();
        }

        private void PageNumberBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            //if (comboBox.SelectedValue != null)
            //{
            //    maxRowCount = (comboBox.SelectedIndex + 1) * 10;
            //}

            maxRowCount = int.Parse(comboBox.SelectedItem.ToString());
            shownLevels = new MarioMakerLevel[maxRowCount]; // Make the shown level size equal to maxRowCount
            ResetShownLevels(pagenumber);
            ResetDataSource();
            ResetPageBox();
        }

        private void LoadCSV_Click(object sender, EventArgs e)
        {
            // There are multiples of this code, probs should make it a function

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "csv"; // Set default extension to CSV
            openFileDialog.AddExtension = true; // Auto add csv to end of file if user doesn't add it
            WebsiteSearch websiteSearch = new WebsiteSearch();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (websiteSearch.Isfiletype(openFileDialog.FileName, "csv")) // If the file is of csv
                {
                    try
                    {
                        marioMakerLevels = websiteSearch.ReadCSV(openFileDialog.FileName); // Read CSV file from path, putting the list of levels into the marioMakerLevels list
                        levelsChecked = 0; // Reset levelsChecked
                        ResetAll(); // Call ResetAll
                    }
                    catch (Exception exception)
                    {
                        ShowErrorDialog(exception.Message, "Error opening file"); // Display dialogue box with error
                    }
                }
                else
                {
                    ShowErrorDialog("Open A CSV Ya Nong-Head", "Error opening file"); // Display dialogue box with error
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //WebsiteSearch webSearch = new WebsiteSearch(); // Create WebsiteSearch object
            //webSearch.SaveXML(marioMakerLevels, "MarioMakerLevels", false); // Save the marioMakerLevels list as an XML

            SaveFileDialog saveFileDialog = new SaveFileDialog(); // Create SaveFileDialog box
            saveFileDialog.DefaultExt = "xml"; // Set default extension to XML
            saveFileDialog.AddExtension = true; // Auto add csv to end of file if user doesn't add it
            WebsiteSearch websiteSearch = new WebsiteSearch(); // Create WebsiteSearch object
            if (saveFileDialog.ShowDialog() == DialogResult.OK) // If the result is ok
            {
                if (websiteSearch.Isfiletype(saveFileDialog.FileName, "xml")) // If the file extension is xml
                {
                    try
                    {
                        websiteSearch.SaveXML(marioMakerLevels, saveFileDialog.FileName, false); // Save the marioMakerLevels list as an XML
                    }
                    catch (Exception exception)
                    {
                        ShowErrorDialog(exception.Message, "Error opening file"); // Display dialogue box with error
                    }
                }
                else
                {
                    ShowErrorDialog("Save it as an XML, Sir TrynaBeCool", "Error saving file"); // Display dialogue box with error
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //WebsiteSearch webSearch = new WebsiteSearch(); // Create WebsiteSearch object
            //webSearch.SaveCSV(marioMakerLevels, "MarioMakerLevels"); // Save the marioMakerLevels list as a CSV

            SaveFileDialog saveFileDialog = new SaveFileDialog(); // Create SaveFileDialog box
            saveFileDialog.DefaultExt = "csv"; // Set default extension to CSV
            saveFileDialog.AddExtension = true; // Auto add csv to end of file if user doesn't add it
            WebsiteSearch websiteSearch = new WebsiteSearch(); // Create WebsiteSearch object
            if (saveFileDialog.ShowDialog() == DialogResult.OK) // If the result is ok
            {
                if (websiteSearch.Isfiletype(saveFileDialog.FileName, "csv")) // If the file extension is csv
                {
                    try
                    {
                        websiteSearch.SaveCSV(marioMakerLevels, saveFileDialog.FileName); // Save the marioMakerLevels list as a CSV
                    }
                    catch (Exception exception)
                    {
                        ShowErrorDialog(exception.Message, "Error saving file"); // Display dialogue box with error
                    }
                }
                else
                {
                    ShowErrorDialog("Save it as a CSV, shoe-brain", "Error saving file"); // Display dialogue box with error
                }
            }
        }

        private void ShowErrorDialog(string error, string caption = "ErrorDialogue")
        {
            MessageBox.Show(error, caption, MessageBoxButtons.OK); // I mean, I didn't HAVE to make this function
        }
    }
}
