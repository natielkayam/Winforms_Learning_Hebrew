using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LettersGame
{
    public class ImageWord : IComparable
    {
        private Image mImage;
        private string mWord;
        private string mImageLocation;

        public ImageWord(string word, string img)
        {
            mWord = word;
            mImageLocation = img;
            mImage = Image.FromFile(String.Format("DIMAGES/{0}", img));
        }

        public Image getImage()
        {
            return mImage;
        }

        public string getWord()
        {
            return mWord;
        }

        public string getImageName()
        {
            return mImageLocation;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            ImageWord imageWord = obj as ImageWord;
            if (imageWord.getWord().Equals(this.mWord))
            {
                return 0;
            }
            return 1;
        }
    }
}
