using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace LettersGame
{
    public class ImageWordSound : ImageWord
    {
        private SoundPlayer mSound;
        private int mIndex;
        private int lengthOfWord;
        private char letter;

        public ImageWordSound(int index,string word,string img,string sound, int lengthOfWord, char letter) : base(word,img)
        {
            mIndex = index;
            mSound = new SoundPlayer(String.Format("VOICE/{0}", sound));
            this.lengthOfWord = lengthOfWord;
            this.letter = letter;
        }

  

        public void play()
        {
            try
            {
                mSound.Play();
            }
            catch
            {

            }
        }

        public string getSoundFileName()
        {
            return System.IO.Path.GetFileName(mSound.SoundLocation);
        }

        public string getLetter()
        {
            return letter.ToString();
        }
    }
}
