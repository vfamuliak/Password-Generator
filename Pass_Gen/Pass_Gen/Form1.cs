using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Pass_Gen
{
    public partial class Form1 : Form
    {
        string password="";
        string for_gen = "qwertyuioplkjhgfdsazxcvbnm";
        public Form1()
        {
            InitializeComponent();
        }

        public void generator(string set, int length)
        {
            Random rnd = new Random();
            int position;

            for(int i = 0; i < length; i++)
            {
                position = rnd.Next(0, set.Length - 1);
                password += set[position];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter the length of your password!");
                return;
            }
            textBox3.Clear();
            password = "";
            for_gen = "qwertyuioplkjhgfdsazxcvbnm";

            int length = Convert.ToInt32(textBox1.Text);

            if (checkBox1.Checked)
            {
                for_gen += "QWERTYUIOPLKJHGFDSAZXCVBNM";
            }
            if (checkBox2.Checked)
            {
                for_gen += "0123456789";
            }
            if (checkBox3.Checked)
            {
                for_gen += "!@#$%^&*()-_=+\\|/*`~[{}]';:,<.>?";
            }

            generator(for_gen, length);

            textBox3.Text = password;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox3.Text == "")
            {
                MessageBox.Show("Password has not been generated yet!!!");
                return;
            }
            Clipboard.SetText(textBox3.Text);
            MessageBox.Show("The text is copied to the clipboard!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Password has not been generated yet!!!");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Enter your email");
                return;
            }
            MailAddress fromMailAddress = new MailAddress("pass.generat@gmail.com", "Password Generator");
            MailAddress toMailAddress = new MailAddress(textBox2.Text);

            using (MailMessage mailMessage = new MailMessage(fromMailAddress, toMailAddress))
            using (SmtpClient smtpClient = new SmtpClient())
            {
                mailMessage.Subject = "Generated password";
                mailMessage.Body = "Generated password - " + textBox3.Text;

                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromMailAddress.Address, "passgen2019");

                smtpClient.Send(mailMessage);
            }
            MessageBox.Show("Message sent successfully!");
        }
    }
}
