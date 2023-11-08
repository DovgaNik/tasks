using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace tasks
{
    public partial class Form4 : Form
    {

        XmlTextReader reader;
        XmlDocument xmlDoc;
        public Form4()
        {
            InitializeComponent();
            string URLString = "login.xml";
            reader = new XmlTextReader(URLString);
            xmlDoc = new XmlDocument();
            xmlDoc.Load(URLString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string newUsername = textBox1.Text;
            string newPassword = textBox2.Text;

            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Please enter both username and password for the new user.");
                return;
            }

            // Create a new user element
            XmlElement newUser = xmlDoc.CreateElement("user");

            // Create username and password elements for the new user
            XmlElement usernameElement = xmlDoc.CreateElement("username");
            usernameElement.InnerText = newUsername;
            XmlElement passwordElement = xmlDoc.CreateElement("password");
            passwordElement.InnerText = Form3.HashPassword(newPassword);

            // Add username and password elements to the new user element
            newUser.AppendChild(usernameElement);
            newUser.AppendChild(passwordElement);

            // Add the new user element to the XML document
            xmlDoc.DocumentElement.AppendChild(newUser);
            xmlDoc.Save("login.xml");

            MessageBox.Show("New user added successfully.");
        }

    }
}
