using System.Xml;

namespace tasks
{
    public partial class Form4 : Form
    {

        XmlTextReader reader;
        XmlDocument xmlDoc;
        string username;
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
            string confirmPassword = textBox3.Text;

            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please enter username, password and confirmation of password for the new user.", "Not all fields completed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (newPassword != confirmPassword)
            {
                MessageBox.Show("The password and cofnirmation don't match, please try again.", "Passwords don't match", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlElement newUser = xmlDoc.CreateElement("user");

            XmlElement usernameElement = xmlDoc.CreateElement("username");
            usernameElement.InnerText = newUsername;
            XmlElement passwordElement = xmlDoc.CreateElement("password");
            passwordElement.InnerText = Form3.HashPassword(newPassword);

            newUser.AppendChild(usernameElement);
            newUser.AppendChild(passwordElement);

            xmlDoc.DocumentElement.AppendChild(newUser);
            xmlDoc.Save("login.xml");

            MessageBox.Show("New user added successfully.", "Success!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            username = newUsername;
            Close();
            return;
        }

        public string GetUsername()
        {

            return username;

        }

    }
}
