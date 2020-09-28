using MarioMakerLevelSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarioMakerLevelSearchWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MarioMakerLevel> marioMakerLevels = new List<MarioMakerLevel>();
        MarioMakerLevel[] shownLevels;
        int levelsChecked = 0;
        int maxRowCount = 2;
        int pagenumber = 0;
        int[] searchValues; // To keep track of where we are up to when searching
        Thread thread;
        Thread sapThread;

        BitmapImage thumbnailImage;
        BitmapImage fullImage;

        readonly SynchronizationContext syncContext;

        public MainWindow()
        {
            InitializeComponent();

            shownLevels = new MarioMakerLevel[maxRowCount]; // Make the shown level size equal to maxRowCount

            //ResetAll();
            //ResetPageNumberBox();

            syncContext = SynchronizationContext.Current;

            thread = new Thread(GetAllData); // Create Thread which when called calls GetAllData
            thread.IsBackground = true; // Make the thread run in background (Stop the application from running when closed for some reason)

            //TextEntriesChecked.Text = ""; // Change text to nothing
            //TextEntriesAdded.Text = ""; // Change text to nothing
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridCellInfo info = LevelGrid.SelectedCells[0];
            MarioMakerLevel selected = info.Item as MarioMakerLevel;

            if (selected != null)
            {
                StartSetAllPicturesThread(selected.Link, "course-image", LevelImageThumbnail, "course-image-full", LevelImageFull);
            }



            //if (col > 0 && col < dataGridView1.Width && row > 0 && row < dataGridView1.Height) // If the cell is within the grid (for some reason pressing the column name caused an out of bounds error \_(ツ)_/ )
            //{

            //    //if (col < 0) col = 0; else if (col > dataGridView1.Width) col = dataGridView1.Width; 
            //    //if (row < 0) row = 0; else if (row > dataGridView1.Height) row = dataGridView1.Height; 

            //    var cell = ((DataGridView)sender)[col, row];

            //    // This just grabs the data from the cell clicked, and puts it into the text objects
            //    if (cell.Value != null)
            //    {
            //        DataGridViewRow rows = cell.OwningRow;
            //        WebsiteSearch web = new WebsiteSearch();

            //        TextTitle.Text = "Title - " + rows.Cells["Title"].Value.ToString();
            //        TextAuthor.Text = "Author - " + rows.Cells["Author"].Value.ToString();
            //        TextDifficulty.Text = "Difficulty - " + rows.Cells["Difficulty"].Value.ToString();
            //        TextClearRate.Text = "Clear Rate - " + rows.Cells["ClearRate"].Value.ToString();
            //        TextStars.Text = "Stars - " + rows.Cells["Stars"].Value.ToString();
            //        var index = Array.FindIndex(web.gameStylesShort, i => i == rows.Cells["GameStyle"].Value.ToString());
            //        TextGameStyle.Text = "Game Style - " + web.gameStylesLong[index];
            //        //TextGameStyle.Text = "GameStyle - " + rows.Cells["GameStyle"].Value.ToString();
            //        TextPlayersPlayed.Text = "Players Played - " + rows.Cells["PlayersPlayed"].Value.ToString();
            //        TextRegion.Text = "Region - " + rows.Cells["Flag"].Value.ToString();
            //        TextTag.Text = "Tag - " + rows.Cells["Tag"].Value.ToString();
            //        TextID.Text = "ID - " + rows.Cells["ID"].Value.ToString();

            //        //StartGetPictureThread(rows.Cells["Link"].Value.ToString(), "course-image", PictureBoxLevelThumbnail);
            //        //StartGetPictureThread(rows.Cells["Link"].Value.ToString(), "course-image-full", PictureBoxFullCourse);
            //        // Replaced the previous two with a single one, therefore I just need to run one thread! (Also, accessing a site two times that quickly makes it a bit suspish, shhh)
            //        StartSetAllPicturesThread(rows.Cells["Link"].Value.ToString(), "course-image", PictureBoxLevelThumbnail, "course-image-full", PictureBoxFullCourse);
            //    }
            //}
        }

        #region CustomMethods

        private void ResetDataSource()
        {
            List<MarioMakerLevel> levels = new List<MarioMakerLevel>();
            levels = marioMakerLevels.Where(p => p != null).ToList();
            LevelGrid.DataContext = levels;
        }

        //private void ResetPageBox()
        //{
        //    PageBox.Items.Clear(); // Clear combobox

        //    PageBox.Items.Add(1);

        //    for (int i = 0; i < Math.Ceiling(marioMakerLevels.Count * 1.0f / maxRowCount) - 1; i++)
        //        PageBox.Items.Add((i + 2).ToString());

        //    if (marioMakerLevels.Count != 0)
        //        if (PageBox.Items.Count >= 0)
        //        {
        //            if (PageBox.Items.Count - 1 < pagenumber)
        //                pagenumber = PageBox.Items.Count - 1;
        //            else
        //                pagenumber = 0;
        //            PageBox.SelectedIndex = pagenumber;
        //        }
        //        else
        //            PageBox.SelectedIndex = 0;
        //}

        //private void ResetSortingBox()
        //{
        //    SortingBox.Items.Clear(); // Clear combobox
        //    foreach (var prop in typeof(MarioMakerLevel).GetProperties())
        //        SortingBox.Items.Add(prop.Name); // Add all properties to the combobox
        //}

        //private void ResetShownLevels(int page)
        //{
        //    // For the length of shownLevels, add the corresponding MarioMakerLevel in marioMakerLevels
        //    for (int i = 0; i < shownLevels.Length; i++)
        //    {
        //        int index = page * shownLevels.Length + i;

        //        if (marioMakerLevels.Count > index && marioMakerLevels.Count != 0)
        //            shownLevels[i] = marioMakerLevels[index];
        //        else
        //            shownLevels[i] = null;

        //    }

        //    TextEntriesChecked.Text = $"{levelsChecked} Entries checked"; // Change the text to show how many entries have been checked
        //    TextEntriesAdded.Text = $"{marioMakerLevels.Count} Entries Added"; // Change the text to show how many entries have been added
        //}

        //private void ResetPageNumberBox()
        //{
        //    // Fucken add as many as you like!! Make one a million, that would be fun for your ram and processor!!!
        //    PageNumberBox.Items.Add("10");
        //    PageNumberBox.Items.Add("20");
        //    PageNumberBox.Items.Add("50");
        //    PageNumberBox.Items.Add("100");
        //    PageNumberBox.Items.Add("250");
        //    PageNumberBox.Items.Add("500");
        //    PageNumberBox.Items.Add("1000");

        //    PageNumberBox.SelectedValue = "50";
        //    PageNumberBox.SelectedIndex = 0;
        //}

        private void ResetAll()
        {
            ResetDataSource(); // Reset datasource of datagridview
            //ResetSortingBox(); // Reset sortfd oksmf
            //ResetPageBox(); // Rseaodn  oad
            //ResetShownLevels(pagenumber); // afijf

            // You get the picture, it resets stuff
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

                                        if (!hasNull)
                                            marioMakerLevels.AddRange(tempLevels.Where(p => marioMakerLevels.Where(q => q.ID == p.ID).ToArray().Length == 0));

                                        searchValues = new int[] { i, j, k, l, m, n, o };
                                        Thread.Sleep(1500); // Sleep for 1.5 seconds, if we do this too fast Nintendo will block access
                                    }

                                }
        }

        public void StartSetPictureThread(string link, string className, Image picBox)
        {


            // This shit can go!
            var t1 = new Thread(() => SetPicture(link, className, picBox));
            t1.Start();
        }

        private void SetPicture(string link, string className, Image picBox)
        {
            // This can go to

            WebsiteSearch web = new WebsiteSearch();
            // Get Image link
            string imageLink = web.GetPictureLink(link, className);

            // Load images if they return a value
            Console.WriteLine(imageLink);
            if (imageLink != null)
                picBox.Source = GetPicture(imageLink);
        }

        public void StartSetAllPicturesThread(string link, string className1, Image picBox1, string className2, Image picBox2)
        {
            if (sapThread != null) if (sapThread.IsAlive) sapThread.Abort();

            // Create a thread that loads the pictures
            sapThread = new Thread(() => SetAllPictures(link, className1, picBox1, className2, picBox2));
            sapThread.Start();
        }

        private void SetAllPictures(string link, string className1, Image picBox1, string className2, Image picBox2)
        {
            Thread.Sleep(1000);

            WebsiteSearch web = new WebsiteSearch();
            // Get links
            string imageLink1 = web.GetPictureLink(link, className1);
            string imageLink2 = web.GetPictureLink(link, className2);

            thumbnailImage = GetPicture(imageLink1);
            fullImage = GetPicture(imageLink2);

            syncContext.Post(o => RefreshPictures(), null);
        }

        private BitmapImage GetPicture(string imageLink)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(imageLink);
            bitmapImage.EndInit();

            return bitmapImage;
        }

        private void RefreshPictures()
        {
            LevelImageThumbnail.Source = thumbnailImage;
            LevelImageFull.Source = fullImage;
        }

        private void ShowErrorDialog(string error, string caption = "ErrorDialogue")
        {
            MessageBox.Show(error, caption, MessageBoxButton.OK); // I mean, I didn't HAVE to make this function
        }

        #endregion

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
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

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }
    }
}
