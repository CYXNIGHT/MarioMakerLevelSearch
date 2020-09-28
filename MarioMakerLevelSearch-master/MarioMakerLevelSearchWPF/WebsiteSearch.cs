using HtmlAgilityPack;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Xml;

namespace MarioMakerLevelSearch
{
    class WebsiteSearch
    {
        // This helps to loop through website in GetLevels using integers

        public string[] gameStylesShort = { "", "sb", "sb3", "sw", "sbu" };
        public string[] gameStylesLong = { "", "Mario Bros", "Mario Bros 3", "Mario World", "Mario Bros U" };
        public string[] gameStyles = { "", "mario_bros", "mario_bros3", "mario_world", "mario_bros_u" };
        public string[] courseThemes = { "", "ground", "underground", "underwater", "gohst_house", "airship", "castle" };
        public string[] regions = { "", "jp", "us", "eu", "others" };
        public string[] difficulties = { "", "easy", "normal", "expert", "super_expert" };
        public string[] uploadDates = { "", "past_day", "past_week", "past_month", "before_one_month" };



        public List<MarioMakerLevel> GetLevels(int page, int gameStyle, int courseTheme, int region, int difficulty, int tag, int uploadDate)
        {
            List<MarioMakerLevel> marioMakerLevels = new List<MarioMakerLevel>(); // Get a new list of marioMakerLevels

            string finalTag = ""; // Create a finalTag variable
            if (tag != 0) // If the tag is not 0
                finalTag = tag.ToString(); // Then make the finalTag equal to the tag value


            string startString = "https://supermariomakerbookmark.nintendo.net/search/result?"; // Starting string of website
            string pageString = $"page={page + 1}"; // Get website string for page
            string skinString = $"&q%5Bskin%5D={ gameStyles[gameStyle] }"; // Get website string for skin
            string sceneString = $"&q%5Bscene%5D={ courseThemes[courseTheme] }"; // Get website string for theme
            string areaString = $"&q%5Barea%5D={ regions[region] }"; // Get website string for region
            string difficultyString = $"&q%5Bdifficulty%5D={ difficulties[difficulty] }"; // Get website string for difficulty
            string tagString = $"&q%5Btag_id%5D={ finalTag }"; // Get website string for tag
            string createdAtString = $"&q%5Bcreated_at%5D={ uploadDates[uploadDate] }"; // Get website string for uploaddate
            string endString = $"&q%5Bsorting_item%5D=like_rate_desc"; // The ending string of website

            // Compile all these strings into one string variable
            string finalString = startString +
                                pageString +
                                skinString +
                                sceneString +
                                areaString +
                                difficultyString +
                                tagString +
                                createdAtString +
                                endString;

            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb(); // Get a html web object
            HtmlAgilityPack.HtmlDocument doc = web.Load(finalString); // Create new doc object using web load of the string


            if (doc != null) // If the doc collected is not null
            {
                for (int i = 0; i < 10; i++)
                {
                    MarioMakerLevel level = new MarioMakerLevel();
                    // Get level title
                    try { level.Title = doc.DocumentNode.SelectNodes("//div[@class='course-title']")[i].InnerText; } catch { }
                    // Get level author
                    try { level.Author = doc.DocumentNode.SelectNodes("//div[@class='name']")[i].InnerText; } catch { }
                    // Get level theme
                    try { level.GameStyle = doc.DocumentNode.SelectNodes("//div[@class='gameskin-wrapper']")[i].FirstChild.Attributes[0].Value.Replace("gameskin bg-image common_gs_", ""); } catch { }
                    // Get level difficulty
                    try { level.Difficulty = doc.DocumentNode.SelectNodes("//div[@class='rank nonprize']")[i].NextSibling.InnerText; } catch { }
                    // Get level clear rate (Redacted)
                    //try { level.ClearRate = GetValueFromFont(doc.DocumentNode.SelectNodes("//div[@class='clear-rate']")[i]); } catch { }
                    // Get level stars
                    try { level.Stars = int.Parse(GetValueFromFont(doc.DocumentNode.SelectNodes("//div[@class='liked-count ']")[i])); } catch { }
                    // Get level players played
                    try { level.PlayersPlayed = int.Parse(GetValueFromFont(doc.DocumentNode.SelectNodes("//div[@class='played-count ']")[i])); } catch { }
                    // Get level clears
                    try { level.Clears = int.Parse(GetValueFromClears(doc.DocumentNode.SelectNodes("//div[@class='tried-count ']")[i])[0]); } catch { }
                    // Get level rounds played
                    try { level.RoundsPlayed = int.Parse(GetValueFromClears(doc.DocumentNode.SelectNodes("//div[@class='tried-count ']")[i])[1]); } catch { }
                    // Get level clear rate
                    try { level.ClearRate = GetValueFromClears(doc.DocumentNode.SelectNodes("//div[@class='tried-count ']")[i])[2]; } catch { }
                    // Get level tag
                    try { level.Tag = doc.DocumentNode.SelectNodes("//div[@class='course-tag radius5']")[i].InnerText; } catch { }
                    // Get level flag
                    try { level.Flag = doc.DocumentNode.SelectNodes("//div[@class='creator-info']")[i].FirstChild.Attributes[0].Value.Replace("flag ", ""); } catch { }
                    // Get level date uploaded
                    //level.DateUploaded = doc.DocumentNode.SelectNodes("//div[@class='created_at']")[i].InnerText;
                    // Get level link
                    try { level.Link = "https://supermariomakerbookmark.nintendo.net" + doc.DocumentNode.SelectNodes("//a[@class='button course-detail link']")[i].GetAttributeValue("href", "null"); } catch { }

                    marioMakerLevels.Add(level); // Add the level to the list of levels
                }
                
                return marioMakerLevels; // Return levels
            }

            return null; // Otherwise return null
        }

        public String GetPictureLink(string link, string className)
        {
            HtmlWeb web = new HtmlWeb(); // Get a html web object
            HtmlDocument doc = web.Load(link); // Create new doc object using web load of the string


            if (doc != null) // If the doc collected is not null
            {
                string imageLink = null;
                try { imageLink = doc.DocumentNode.SelectSingleNode($"//img[@class='{className}']").GetAttributeValue("src", null).ToString(); } catch { }//.GetAttributeValue("src", "null"); } catch { }
                return imageLink; // Otherwise return null
            }
            return null; // Otherwise return null
        }

        private string GetValueFromFont(HtmlNode item)
        {
            string finalValue = ""; // Create final value as nothing

            foreach (var val in item.ChildNodes) // For each value in the child node
            {
                foreach (var attribute in val.Attributes) // And for each attribute
                {
                    string attributeValue = attribute.Value; // Get the attribute value
                    attributeValue = attributeValue.Replace("typography typography-", ""); // Remove unnecessary characters

                    if (attributeValue.Length <= 1) // If the attribute length is lower or equal to one
                        finalValue += attributeValue; // Add it to the final value
                    else if (attributeValue == "second") // Otherwise if the attribute value equals second
                        finalValue += "."; // Add a full stop
                    else if (attributeValue == "percent") // Otherwise if the attribute value is equal to percent
                        finalValue += "%"; // Add a percent
                }
            }
            return finalValue; // Return string
        }

        private string[] GetValueFromClears(HtmlAgilityPack.HtmlNode item)
        {
            string[] finalValue = {"", "", "" }; // Create array of strings
            int position = 0; // Make postition 0

            foreach (var val in item.ChildNodes) // For each value in the child node
            {
                foreach (var attribute in val.Attributes) // And for each attribute
                {
                    string attributeValue = attribute.Value; // Get the attribute value
                    attributeValue = attributeValue.Replace("typography typography-", ""); // Remove unnecessary characters

                    if (attributeValue.Length <= 1) // If the attribute length is lower or equal to one
                        finalValue[position] += attributeValue; // Add the attribute value to the position position in final value
                    else if (attributeValue == "slash") // Otherwise if the attribute value equals second
                        position = 1; // Go to next position in final value array
                }
            }

            finalValue[2] = ((float)int.Parse(finalValue[0]) / (float)int.Parse(finalValue[1])).ToString("P"); // Get the clear rate by dividing clears over plays

            return finalValue; // Return string
        }

        public string GetOriginalFormat()
        {
            string originalFormat = ""; // Make original format equal to nothing
            foreach (var prop in typeof(MarioMakerLevel).GetProperties()) // Foreach property in MarioMakerLevel object
            {
                originalFormat += prop.Name.ToString(); // Add the properties name to the originalFormat
                originalFormat += ","; // And seperate each value using a comma
            }
            return originalFormat.Remove(originalFormat.Length - 1); // Return this format, while removing the last comma
        }

        public List<MarioMakerLevel> ReadCSV(string path)
        {
            var items = new List<MarioMakerLevel>(); // Create list of items

            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                var index = 0;
                while (!parser.EndOfData) // Check if we are not at the end of file
                {
                    //Processing row
                    string[] fields = parser.ReadFields(); // Get the row fields
                    if (index > 0)
                    {
                        var item = new MarioMakerLevel(); // Create a new object

                        var props = typeof(MarioMakerLevel).GetProperties(); // Get the properties of MarioMakerLevel
                        var j = 0;
                        foreach (var prop in props) // Loop through the properties
                        {
                            if (j < fields.Length)
                            {
                                typeof(MarioMakerLevel).GetProperty(prop.Name).SetValue(item, Convert.ChangeType(fields[j], prop.PropertyType)); // Set value of item property to fields[j]
                            }
                            j++;
                        }
                        items.Add(item); // Add item to list
                    }
                    index++; // Iterate
                }
            }
            return items; // Return items
        }

        public void SaveCSV(List<MarioMakerLevel> items, string path, int[] searchValues = null)// string filename)
        {

            // This has errors
            /// TODO - Fix errors

            // I think they're fixed... thanks younger me

            using (StreamWriter sw = new StreamWriter(path))//($@".\{filename}.csv")) // Open file
            {
                sw.WriteLine(GetOriginalFormat()); // Save the format of the file
                foreach (var item in items)
                {
                    sw.WriteLine(item.ConvertItemToString(item));
                    //Console.WriteLine(ConvertItemToString(item));
                }

                if (searchValues != null) sw.WriteLine(searchValues.ArrayToString());
            }
        }

        public void SaveXML(List<MarioMakerLevel> items, string path, bool splitAttributes)
        {

            XmlWriter xmlWriter = XmlWriter.Create(path); // Create XmlWriter object

            var props = typeof(MarioMakerLevel).GetProperties(); // Get properties of the Item class

            xmlWriter.WriteStartDocument(); // Start the xml document write
            xmlWriter.WriteStartElement("Levels"); // Start element that contains Items

            foreach (var item in items) // Loop through items
            {
                xmlWriter.WriteStartElement("Level"); // Start element that contains the attributes of the item


                foreach (var prop in props) // Loop through Item properties
                {
                    string str = "";

                    if (typeof(MarioMakerLevel).GetProperty(prop.Name).GetValue(item) != null)
                    {
                        str = typeof(MarioMakerLevel).GetProperty(prop.Name).GetValue(item).ToString(); // Get the properties value as string
                    }

                    if (str != "") // If the property isn't blank
                    {
                        if (splitAttributes) // If split attributes is true
                        {
                            xmlWriter.WriteStartElement(prop.Name); // Start element of the properties name
                            xmlWriter.WriteString(str); // Write the properties value
                            xmlWriter.WriteEndElement(); // End the element
                        }
                        else // Otherwise
                        {
                            xmlWriter.WriteAttributeString(prop.Name, str); // Save attribute without its own element
                        }
                    }
                }
                xmlWriter.WriteEndElement(); // End the Item element
            }

            xmlWriter.WriteEndElement(); // End the Items element
            xmlWriter.WriteEndDocument(); // End the document
            xmlWriter.Close(); // Close the document
        }

        public bool Isfiletype(string path, string filetype)
        {
            return (path.Substring(Math.Max(0, path.Length - 4)).ToLower() == "." + filetype.ToLower()) ? true : false; // Return true if the filetype string is at the end of the path
        }


    }

    public static class Extensions
    {
        public static string ArrayToString<T>(this T[] array, string limiter = ",")
        {
            string str = "";

            foreach (var item in array)
            {
                str += item.ToString();
            }

            return str;
        }
    }
}
