using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace tasks
{
    public partial class Form3 : Form
    {
        XmlTextReader reader;
        XmlDocument xmlDoc;
        private string loggedInUsername;
        public Form3()
        {
            InitializeComponent();
            refresh();
        }

        private void refresh()
        {
            string URLString = "login.xml";
            reader = new XmlTextReader(URLString);
            xmlDoc = new XmlDocument();
            xmlDoc.Load(URLString);

        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var hashOfEnteredPassword = HashPassword(enteredPassword);
            return hashOfEnteredPassword.Equals(storedHash);
        }

        public string GetLoggedInUsername()
        {
            return loggedInUsername;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputUsername = textBox1.Text;
            string inputPassword = textBox2.Text;

            XmlNodeList userList = xmlDoc.SelectNodes("//user");
            foreach (XmlNode user in userList)
            {
                XmlNode usernameNode = user.SelectSingleNode("username");
                XmlNode passwordNode = user.SelectSingleNode("password");

                if (usernameNode != null && passwordNode != null)
                {
                    string storedUsername = usernameNode.InnerText;
                    string storedPassword = passwordNode.InnerText;

                    if (inputUsername == storedUsername && VerifyPassword(inputPassword, storedPassword))
                    {
                        loggedInUsername = storedUsername;
                        this.Close();
                        return;
                    }
                }
            }
            MessageBox.Show("Login unsuccessful!", "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 regForm = new Form4();
            regForm.ShowDialog();
            if (regForm.GetUsername() != null)
            {
                loggedInUsername = regForm.GetUsername();
                refresh();
                Close();
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
                loggedInUsername = "";
                Close();
                return;
            }
        }
    }
}
