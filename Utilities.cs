using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LettersGame
{
    public static class Utilities
    {
        public const string TRAINING_LETTERS_FILE_NAME = "firstletterData.txt";
        public const string WORDS_FILE_NAME = "wordImageData.txt";

        public static List<TrainingLetter> parseTrainingLetters()
        {
            List<TrainingLetter> list = new List<TrainingLetter>();
            foreach (var line in File.ReadLines(TRAINING_LETTERS_FILE_NAME))
            {
                string[] splittedLine = line.Split(';');
              
                int number = Int32.Parse(stripStr(splittedLine[0]));
                char letter = char.Parse(stripStr(splittedLine[1]));
                string imgName = String.Format("DIMAGES/{0}", stripStr(splittedLine[2]));
                Image letterImage = Image.FromFile(imgName);
                List<ImageWord> imageWordList = new List<ImageWord>();
                for (int i = 3; i < splittedLine.Length-1; i= i+2)
                {
                    string word = stripStr(splittedLine[i]);
                    string img = stripStr(splittedLine[i + 1]);
                    imageWordList.Add(new ImageWord(word, img));
                }
                list.Add(new TrainingLetter(number, letter, letterImage, imageWordList));
            }

            return list;


        }

        public static List<ImageWordSound> parseAllWords()
        {
 
            List<ImageWordSound> list = new List<ImageWordSound>();
            foreach (var line in File.ReadLines(WORDS_FILE_NAME))
            {
                string[] splittedLine = line.Split(';');

                int index = Int32.Parse(stripStr(splittedLine[0]));
                string word = stripStr(splittedLine[1]);
                string imgName = stripStr(splittedLine[2]);
                string soundName = stripStr(splittedLine[3]);
                int lengthOfWord = Int32.Parse(stripStr(splittedLine[4]));
                char letter = char.Parse(stripStr(splittedLine[5]));
               
            
                list.Add(new ImageWordSound(index, word, imgName, soundName, lengthOfWord, letter));
            }

            return list;
        }

        public static ImageWordSound writeNewWordToFile(string word,string imgPath,string soundPath,int number)
        {
            File.Copy(imgPath, String.Format("DIMAGES/{0}", Path.GetFileName(imgPath),true));
            File.Copy(soundPath, String.Format("VOICE/{0}", Path.GetFileName(soundPath),true));
            string line = String.Format("\n{0};{1};{2};{3};{4};{5}", number, word, Path.GetFileName(imgPath), Path.GetFileName(soundPath), word.Length, word[0]);
            File.AppendAllText(WORDS_FILE_NAME, line);

            ImageWordSound image = new ImageWordSound(number, word, Path.GetFileName(imgPath), Path.GetFileName(soundPath), word.Length, word[0]);
            return image;
       ///File.AppendAllText("file.txt", string.Format("{0}{1}", "OK", Environment.NewLine));

        }

        public static string stripStr(this string str)
        {
            var regex = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");
            return regex.Replace(str, "");
        }

    }
}
