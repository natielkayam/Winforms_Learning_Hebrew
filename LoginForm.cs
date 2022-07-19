using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LettersGame
{
    public partial class LoginForm : Form
    {
 
        public LoginForm()
        {
            InitializeComponent();
        }

        private void enterButton_Click(object sender, EventArgs e)
        {          
            try
            {
              verifyEmail(emailTextField.Text);                
                Menu menu = new LettersGame.Menu();
                this.Hide();
                menu.Closed += (s, args) => this.Close();
                menu.Show();
            }
            catch(InvalidEmailException mail)
            {
                MessageBox.Show("Error login", mail.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool verifyEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            bool isMatch = regex.IsMatch(email);
            if (!isMatch)
            {
                throw new InvalidEmailException(email);
            }
            return true;
        }
    }

    class InvalidEmailException : Exception
    {
        public InvalidEmailException(string mail)
            : base(String.Format("Invalid Email : {0}", mail))
        {

        }
    }
}
