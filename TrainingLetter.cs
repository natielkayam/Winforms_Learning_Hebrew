using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LettersGame
{
    public class TrainingLetter
    {
        private int mNumber;
        private char mFirstLetter;
        private Image mFirstimgLetter;
        private List<ImageWord> mListOfWords;

        public TrainingLetter(int number,char letter,Image imageLetter,List<ImageWord> words)
        {
            mNumber = number;
            mFirstLetter = letter;
            mFirstimgLetter = imageLetter;
            mListOfWords = words;

        }

        public char getLetter()
        {
            return mFirstLetter;
        }

        public Image getImageLetter()
        {
            return mFirstimgLetter;
        }

        public int getSizeOfWords()
        {
            return mListOfWords.Count;
        }

        public ImageWord getWordFromIndex(int index)
        {
            return mListOfWords[index];
        }
       
    }
}
