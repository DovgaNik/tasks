using System.Xml;

namespace tasks
{
    public partial class Form1 : Form
    {
        List<(string, double, double, byte)> values = new List<(string, double, double, byte)>(); // name, deadline, duration, priority
        string loggedInUsername;
        public Form1()
        {
            InitializeComponent();

            Form3 loginPage = new Form3();
            loginPage.ShowDialog();
            loggedInUsername = loginPage.GetLoggedInUsername();
            MessageBox.Show("Logged in as " + loggedInUsername);
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 createTaskForm = new Form2(dataGridView1);
            createTaskForm.ShowDialog();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.SelectedRows[0].Index;
            int selectedColumn = dataGridView1.SelectedCells[0].ColumnIndex;
            byte priority = 0;
            switch (dataGridView1.Rows[selectedRow].Cells[3].Value.ToString())
            {
                case "Low":
                    priority = 0;
                    break;
                case "Normal":
                    priority = 1;
                    break;
                case "High":
                    priority = 2;
                    break;
                case "Urgent":
                    priority = 3;
                    break;
            }

            Form2 createTaskForm = new Form2(dataGridView1, true, dataGridView1.Rows[selectedRow].Cells[0].Value.ToString(), DateTime.Parse(dataGridView1.Rows[selectedRow].Cells[2].Value.ToString()), priority);
            createTaskForm.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlTextReader reader = new XmlTextReader("database.xml");

        }

    }
}