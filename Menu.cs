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
    public partial class Menu : Form
    {
        List<TrainingLetter> mTotalListTrainingList;
        List<ImageWordSound> mTotalWords;

        public Menu()
        {
            InitializeComponent();
            mTotalListTrainingList = Utilities.parseTrainingLetters();
            mTotalWords = Utilities.parseAllWords();
        }

        private void mTrainingButton_Click(object sender, EventArgs e)
        {
            TrainingForm loginForm = new TrainingForm(mTotalListTrainingList,mTotalWords);       
            loginForm.setPreviousForm(this);
            loginForm.Show();
            this.Hide();
        }

        private void mAddDynamicDataButton_Click(object sender, EventArgs e)
        {
            AddDataForm addDataForm = new AddDataForm(mTotalWords);
            addDataForm.setPreviousForm(this);
            addDataForm.Show();
            this.Hide();
        }

        private void mPlayGameButton_Click(object sender, EventArgs e)
        {
            PlayGameForm playGameForm = new PlayGameForm(mTotalWords);
            playGameForm.setPreviousForm(this);
            playGameForm.Show();
            this.Hide();
        }
    }
}
