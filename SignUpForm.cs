using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace abc
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            validation();
            verification();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtPassword.Text = null;
            this.txtUserName.Text = null;
        }

        bool isOK;

        //will validate the syntax of the username as well as the password
        void validation()
        {
            bool isAlpha = false;
            bool isNumeric = false;
            isOK = true;

            //making both the info labels null so each time on click of submit button, new message can appear in the label
            lblUserNameInfo.Text = null;
            lblPasswordInfo.Text = null;
           
            //restricting the length of username to be in the range 3-8
            if (txtUserName.Text.Length < 3 || txtUserName.Text.Length > 8)
            {
                lblUserNameInfo.Text = "Length of username must be in range 3-8";
                isOK = false;
            }

            //restricting the length of password to be greater than 6
            if (txtPassword.Text.Length < 6)
            {
                lblPasswordInfo.Text = "The length of password must be atleast 6";
                isOK = false;
            }
            //restricting alpha-numeric values only
            else
            {
                foreach (char c in txtPassword.Text)
                {
                    if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    {
                        isAlpha = true;
                        continue;
                    }
                    else if (c >= '0' && c <= '9')
                    {
                        isNumeric = true;
                        continue;
                    }
                    else
                    {
                        isAlpha = false;
                        isNumeric = false;
                        break;
                    }
                }
                if ( ! (isAlpha && isNumeric) )
                {
                    lblPasswordInfo.Text = "Only alpha-numeric values allowed";
                    isOK = false;
                }
            }

        }


        void verification()
        {
            if (isOK)
            {
                string[] allLines = File.ReadAllLines(@"C:\Users\filza\source\repos\abc\users.txt");

                foreach (string s in allLines)
                {
                    if(txtUserName.Text == s.Split(';')[0])
                    {
                        MessageBox.Show("Username already exists");
                        isOK = false;
                    }
                }

                if (isOK)
                {
                    StreamWriter writer = File.AppendText(@"C:\Users\filza\source\repos\abc\users.txt");
                    writer.WriteLine(txtUserName.Text + ";" + txtPassword.Text);
                    writer.Close();
                }

            }
            

        }

    }
}
