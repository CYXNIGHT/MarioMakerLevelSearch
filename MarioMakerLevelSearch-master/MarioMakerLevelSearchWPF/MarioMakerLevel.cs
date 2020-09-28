using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioMakerLevelSearch
{
    class MarioMakerLevel
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        //public string Theme { get; set; }
        public string GameStyle { get; set; }
        public string Difficulty { get; set; }
        public string ClearRate { get; set; }
        public int Stars { get; set; }
        public int PlayersPlayed { get; set; }
        public int Clears { get; set; }
        public int RoundsPlayed { get; set; }
        //public string DateUploaded { get; set; }
        public string Flag { get; set; }
        public string Tag { get; set; }
        public string Link { get; set; }


        public List<MarioMakerLevel> SortList(List<MarioMakerLevel> items, string sortingValue)
        {
            var toString = true;
            if (typeof(MarioMakerLevel).GetProperty(sortingValue).PropertyType.Name.Equals("Int32")) // If the value type is int then don't compare using string
            {
                toString = false;
            }

            items.Sort(delegate (MarioMakerLevel item1, MarioMakerLevel item2)
            {
                int diff = 0;
                if (toString)
                {
                    diff = typeof(MarioMakerLevel).GetProperty(sortingValue).GetValue(item1, null).ToString().CompareTo(typeof(MarioMakerLevel).GetProperty(sortingValue).GetValue(item2, null).ToString()); // Sort items based on int
                }
                else
                    diff = (int)typeof(MarioMakerLevel).GetProperty(sortingValue).GetValue(item1, null) - (int)typeof(MarioMakerLevel).GetProperty(sortingValue).GetValue(item2, null); // Sort items based on string

                if (diff != 0) return diff;
                else return item1.Title.CompareTo(item2.Title); // Then by item code
            });

            return items;
        }

        public string ConvertItemToString(MarioMakerLevel item)
        {
            var final = "";
            var props = typeof(MarioMakerLevel).GetProperties(); // Get the properties from the Item class using reflection
            foreach (var prop in props)
            {
                string str = prop.GetValue(item, null).ToString(); // Get the value of the property of item as a string

                str = str.Replace(",", "");
                str = str.Replace(@"\", "");
                str = str.Replace("'", "");

                final += str; // Add each properties value to the final output
                final += ","; // Seperate each property
            }
            //final += "\n"; // End line of final string
            return final.Substring(0, final.Length - 1); // Return the final string with the last "," removed
        }

        public MarioMakerLevel ConvertStringToItem(string str)
        {
            MarioMakerLevel level = new MarioMakerLevel();
            return level;

        }
    }


}
