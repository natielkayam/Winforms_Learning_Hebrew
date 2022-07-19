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
    public partial class TrainingForm : Form
    {
        private Form mPreviousForm;
        private List<TrainingLetter> mTotalListTrainingList;
        private List<ImageWordSound> mImageSounds;
        //Train play
        private bool mIsTimer = false;
        private TrainingLetter mChosenTrainingLetter;
        private int mChosenIndexStep = 0;
        private Timer mTimer = new Timer();

        public TrainingForm(List<TrainingLetter> mTotalListTrainingList,List<ImageWordSound> imageSoundsList)
        {
            InitializeComponent();
            this.mImageSounds = imageSoundsList;
            this.mTotalListTrainingList = mTotalListTrainingList;
            foreach (TrainingLetter t in mTotalListTrainingList)
            {
                this.mlettersComboBox.Items.Add(t.getLetter().ToString());
            }
            showOrHideTrainControls(false);

            mTimer.Interval = 2000;
            mTimer.Tick += new EventHandler(timer1_Tick);
        }

        public void setPreviousForm(Form menu)
        {
            mPreviousForm = menu;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            mPreviousForm.Show();
        }

        private void mStartButton_Click(object sender, EventArgs e)
        {
            try
            {
                verifyInputBeforeStart();
                showOrHideMainControls(false);
                startShowTraining();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void verifyInputBeforeStart()
        {
            if (mManualRadioButton.Checked == false && mTimerRadioButton.Checked == false)
            {
                throw new Exception("You must to selecte time or manual training");
            }
            if (mlettersComboBox.SelectedItem == null)
            {
                throw new Exception("You must to select letter");
            }
        }

        private void startShowTraining()
        {
          mChosenIndexStep = 0;
          mIsTimer = mTimerRadioButton.Checked;
          mChosenTrainingLetter = mTotalListTrainingList[mlettersComboBox.SelectedIndex];       
          showLetter();
          showOrHideTrainControls(true);
        }

        private void showNextWord()
        {
            if(mChosenIndexStep == mChosenTrainingLetter.getSizeOfWords())
            {
                mTimer.Stop();
                MessageBox.Show("Well Done!!","You are finished", MessageBoxButtons.OK, MessageBoxIcon.None);
                showOrHideMainControls(true);
                showOrHideTrainControls(false);           
                return;
            }
            ImageWord word = mChosenTrainingLetter.getWordFromIndex(mChosenIndexStep++);      
            mPictureBox.Image = word.getImage();
            mWordArea.Text = word.getWord();
            foreach (ImageWordSound sound in mImageSounds)
            {
                if (sound.CompareTo(word) == 0)
                {
                    sound.play();
                    break;
                }
            
            }
        }

        private void showLetter()
        {
            mPictureBox.Image = mChosenTrainingLetter.getImageLetter();
            mWordArea.Text = mChosenTrainingLetter.getLetter().ToString();
            if (mIsTimer)
            {
                mTimer.Start();
            }
        }

  

        private void timer1_Tick(object sender, EventArgs e)
        {
            showNextWord();
        }


        private void showOrHideMainControls(bool show)
        {
            label1.Visible = show;
            label2.Visible = show;
            label3.Visible = show;
            mTimerRadioButton.Visible = show; ;
            mManualRadioButton.Visible = show; ;
            mlettersComboBox.Visible = show;
            mStartButton.Visible = show; ;
            button1.Visible = show; ;

        }

        private void showOrHideTrainControls(bool show)
        {
            mPictureBox.Visible = show;
            mWordArea.Visible = show;           
            if (show == true && mIsTimer == false)
            {
                mNextButton.Visible = show;
            }
            else
            {
                mNextButton.Visible = false;
            }
            
        }

        private void mNextButton_Click(object sender, EventArgs e)
        {
            showNextWord();
        }
    }
}
