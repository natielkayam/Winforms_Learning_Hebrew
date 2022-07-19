using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LettersGame
{
    public partial class AddDataForm : Form
    {

        private Form mPreviousForm;
        private List<ImageWordSound> mImageSounds;


        public AddDataForm(List<ImageWordSound> imageSoundsList)
        {
            InitializeComponent();
            this.mImageSounds = imageSoundsList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Sound files (*.wav) |  *.wav";
            dialog.Title = "Please select a wav sound";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mSoundTextBox.Text = dialog.FileName;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png) |  *.png";
            dialog.Title = "Please select an png image";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mImageTextBox.Text = dialog.FileName;
            }


        }

        public void setPreviousForm(Form menu)
        {
            mPreviousForm = menu;
        }

        private void verifyTextBoxIsNotEmpty()
        {
            if (String.IsNullOrEmpty(mWordTextBox.Text) || String.IsNullOrEmpty(mSoundTextBox.Text) || String.IsNullOrEmpty(mImageTextBox.Text))
            {
                throw new InvalidInputExpition();
            }


        }

        private void mAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                verifyTextBoxIsNotEmpty();
                verifyWordImgAndSoundIsNotExists();

           
            }
            catch (InvalidInputExpition exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(ElemntAlreadyExists exception2)
            {
                MessageBox.Show(exception2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void verifyWordImgAndSoundIsNotExists()
        {
            string wordText = mWordTextBox.Text;
            string imgText = System.IO.Path.GetFileName(mImageTextBox.Text);
            string soundText = System.IO.Path.GetFileName(mSoundTextBox.Text);
            foreach (ImageWordSound img in mImageSounds)
            {
                if (img.getWord().Equals(wordText))
                {
                    throw new ElemntAlreadyExists("word");
                }
                if (img.getImageName().Equals(imgText))
                {
                    throw new ElemntAlreadyExists("image");
                }
                if (img.getSoundFileName().Equals(soundText))
                {
                    throw new ElemntAlreadyExists("sound");
                }

            }
            ImageWordSound result = Utilities.writeNewWordToFile(wordText, mImageTextBox.Text, mSoundTextBox.Text,mImageSounds.Count+1);
            mImageSounds.Add(result);
            MessageBox.Show("Word added", "Sucsses", MessageBoxButtons.OK, MessageBoxIcon.None);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            mPreviousForm.Show();
            this.Hide();
        }
    }


    public class ElemntAlreadyExists : Exception
    {
        public ElemntAlreadyExists(string cause) : base(String.Format("{0}  already exists",cause))
        {

        }
    }

    public class InvalidInputExpition : Exception
    {
        public InvalidInputExpition() : base("Inputs are empty")
        {
           
        }
    }
}
