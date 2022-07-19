using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LettersGame
{

    public class GameLogic
    {
        private List<ImageWordSound> mReminaingImageWord;
        private ImageWordSound mCurrentQuestion;
        private int mStep = 0;
        private int mNumberOfQuestionAsked = 0;
        private int mNumberOfCorrectedAnswer = 0;
        private int mIntervalTimer = 4;
        
        public GameLogic(List<ImageWordSound> imageSoundsList)
        {
            mReminaingImageWord = new List<ImageWordSound>(imageSoundsList);
        }

        public void reset()
        {
            mStep = 0;
        }

        public bool isFinished()
        {
            return mStep == 3 || mReminaingImageWord.Count == 0;
        }

        public ImageWordSound getQuetion()
        {
            mStep++;
            mNumberOfQuestionAsked++;
            Random rnd = new Random();
            int index = rnd.Next(0, mReminaingImageWord.Count);
            mCurrentQuestion = mReminaingImageWord[index];
            mReminaingImageWord.RemoveAt(index);
            return mCurrentQuestion;
        }

        public List<ImageWordSound> getAnswers(int mTypeOfChosenGame, List<ImageWordSound> mImageSounds, ImageWordSound question)
        {
            List<ImageWordSound> answers = new List<ImageWordSound>();
            answers.Add(question);
            int lengthOfAnswers = 0;
            if(mTypeOfChosenGame == 1 || mTypeOfChosenGame == 2 || mTypeOfChosenGame == 3)
            {
                lengthOfAnswers = 4;
            }
            if(mTypeOfChosenGame == 4)
            {
                return answers;
            }
            Random rnd = new Random();         
            while (answers.Count < lengthOfAnswers)
            {
                bool addToAnswers = true;
                ImageWordSound elem = mImageSounds[rnd.Next(0, mImageSounds.Count)];
                if(elem.CompareTo(question) != 0)
                {
                    foreach(ImageWordSound ptr in answers)
                    {
                        if(ptr.CompareTo(elem) == 0)
                        {
                            addToAnswers = false;
                            break;
                        }
                    }
                }
                else
                {
                    addToAnswers = false;
                }
                if (addToAnswers)
                {
                    answers.Add(elem);
                }
            }
            return answers;
        }

        public bool setAnswer(int mTypeOfChoseGame, Image answerImage)
        {
            if(mTypeOfChoseGame == 1)
            {
                if (answerImage == mCurrentQuestion.getImage())
                {
                    mNumberOfCorrectedAnswer++;
                    return true;
                }
                return false;
            }
            if (mTypeOfChoseGame == 2)
            {
                if (answerImage != null && answerImage == mCurrentQuestion.getImage())
                {
                    mNumberOfCorrectedAnswer++;
                    mIntervalTimer--;
                    return true;
                }
                mIntervalTimer++;
                return false;
            }
            

            return false;         
        }

        public int getTimeInterval()
        {
            return mIntervalTimer;
        }

        public bool setAnswerCustomAnswer(int mTypeOfChosenGame, ImageWordSound actualAnswer)
        {
            if (mTypeOfChosenGame == 3)
            {
                if (actualAnswer.getWord().Contains(mCurrentQuestion.getLetter()))
                {
                    mNumberOfCorrectedAnswer++;
                    return true;
                }
                return false;
            }

            return false;
        }


        public bool setAnswerCustomAnswerText(int mTypeOfChosenGame, string txt)
        {
            if (mTypeOfChosenGame == 4)
            {
                if(mCurrentQuestion.getWord().Equals(txt))
                {
                    mNumberOfCorrectedAnswer++;
                    return true;
                }           
                return false;
            }
            return false;
        }
    }
}
