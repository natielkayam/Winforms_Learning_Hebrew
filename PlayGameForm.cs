using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LettersGame
{
    public partial class PlayGameForm : Form
    {

        private List<ImageWordSound> mImageSounds;
        private Form mPreviousForm;
        private int mTypeOfChosenGame = -1;
        private GameLogic mGameLogic;
        private bool mIsTimer = false;
        private Timer mTimer  = new Timer();


        public PlayGameForm(List<ImageWordSound> imageSoundsList)
        {
            InitializeComponent();
            mGameLogic = new GameLogic(imageSoundsList);
            this.mImageSounds = imageSoundsList;
        }

        public void hideShowMenuButtons(bool isVisible)
        {
            label2.Visible = isVisible;
            mGuessWordSample.Visible = isVisible;
            button2.Visible = isVisible;
            button3.Visible = isVisible;
            button4.Visible = isVisible;
            mBackButton.Visible = isVisible; ;
        }

        private void mGuessWordSample_Click(object sender, EventArgs e)
        {
            mTypeOfChosenGame = 1;
            startPlayGame();
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mTypeOfChosenGame = 2;
            startPlayGame();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mTypeOfChosenGame = 3;
            startPlayGame();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mTypeOfChosenGame = 4;
            startPlayGame();
        }

        public void setPreviousForm(Form menu)
        {
            mPreviousForm = menu;
        
        }

        private void mBackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            mPreviousForm.Show();
        }

        private void startPlayGame()
        {
            hideShowMenuButtons(false);
            mGameLogic.reset();
            showNextQuetion();
        }

        private void showNextQuetion()
        {
            if (mGameLogic.isFinished() == true)
            {
                MessageBox.Show("Well Done!!", "You are finished ", MessageBoxButtons.OK, MessageBoxIcon.None);
                hideShowQuetion(false);
                hideShowMenuButtons(true);
                return;
            }
            ImageWordSound question = mGameLogic.getQuetion();
            List<ImageWordSound> answers = mGameLogic.getAnswers(mTypeOfChosenGame, mImageSounds, question);
            displayQuetion(question, answers);
        }

        private void displayQuetion(ImageWordSound question, List<ImageWordSound> answers)
        {
            if (mTypeOfChosenGame == 1 || mTypeOfChosenGame == 2)
            {
                mQuestion.Text = question.getWord();
                mAnswer1.Image = answers[0].getImage();
                mAnswer2.Image = answers[1].getImage();
                mAnswer3.Image = answers[2].getImage();
                mAnswer4.Image = answers[3].getImage();
                if (mTypeOfChosenGame == 2)
                {
                    mTimer = new Timer();
                    mTimer.Enabled = true;
                    mTimer.Tick += new EventHandler(timer1_Tick);
                    mTimer.Interval = mGameLogic.getTimeInterval() * 1000;
                    mTimer.Start();
                }
            }
            else if (mTypeOfChosenGame == 3)
            {
                mQuestion.Text = "Pick picture containts letter - " + question.getLetter();
                mAnswer1.Tag = answers[0];
                mAnswer1.Image = answers[0].getImage();
                mAnswer2.Tag = answers[1];
                mAnswer2.Image = answers[1].getImage();
                mAnswer3.Tag = answers[2];
                mAnswer3.Image = answers[2].getImage();
                mAnswer4.Tag = answers[3];
                mAnswer4.Image = answers[3].getImage();
            }
            else if (mTypeOfChosenGame == 4)
            {
                mAnswer1.Image =question.getImage();
                mTextBoxForQuetionFour.Text = "";
            }

            hideShowQuetion(true);
            
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            mTimer.Stop();
            showAnswerCorrectOrWrong(mGameLogic.setAnswer(mTypeOfChosenGame, null));
        }


        private void hideShowQuetion(bool isVisible)
        {
            if(mTypeOfChosenGame == 1 || mTypeOfChosenGame == 2 || mTypeOfChosenGame ==3)
            {
                mAnswer1.Visible = isVisible;
                mAnswer2.Visible = isVisible;
                mAnswer3.Visible = isVisible;
                mAnswer4.Visible = isVisible;
                mQuestion.Visible = isVisible;
            }
            else
            {
                mConfirmButton.Visible = isVisible;
                mAnswer1.Visible = isVisible;
                mTextBoxForQuetionFour.Visible = isVisible;
            }
        }

        private void mAnswer1_Click(object sender, EventArgs e)
        {
            if (mTypeOfChosenGame == 1 || mTypeOfChosenGame == 2)
            {
                mTimer.Stop();
                PictureBox answer = sender as PictureBox;
                Image answerImage = answer.Image;          
                showAnswerCorrectOrWrong(mGameLogic.setAnswer(mTypeOfChosenGame, answerImage));
            }
            else if(mTypeOfChosenGame == 3)
            {
                PictureBox answer = sender as PictureBox;
                ImageWordSound actualAnswer = answer.Tag as ImageWordSound;    
                showAnswerCorrectOrWrong(mGameLogic.setAnswerCustomAnswer(mTypeOfChosenGame, actualAnswer));
            }
            else if(mTypeOfChosenGame == 4)
            {
                showAnswerCorrectOrWrong(mGameLogic.setAnswerCustomAnswerText(mTypeOfChosenGame, mTextBoxForQuetionFour.Text));
            }
        }

        private void showAnswerCorrectOrWrong(bool isCorrect)
        {
            mTimer.Stop();
            MessageBox.Show(String.Format("{0} answer",isCorrect == true ? "Correct" : "Wrong"), "Question", MessageBoxButtons.OK, MessageBoxIcon.None);
            showNextQuetion();
                     
        }

       
    }  
}
