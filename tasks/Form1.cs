using System.Xml;

namespace tasks
{
    public partial class Form1 : Form
    {
        List<Task> tasks = new List<Task>(); string loggedInUsername;
        string databaseFilename = "";
        XmlDocument xmlDoc;
        XmlElement root;

        public Form1()
        {
            InitializeComponent();

            while (true)
            {
                Form3 loginPage = new Form3();
                loginPage.ShowDialog();
                loggedInUsername = loginPage.GetLoggedInUsername();
                if (loggedInUsername != null)
                {
                    MessageBox.Show("Logged in as " + loggedInUsername, "Successful login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    databaseFilename = loggedInUsername + ".xml";
                    LoadTasksFromXml();
                    break;
                }
                else if (loggedInUsername == "")
                {
                    Application.Exit();
                }

            }

        }


        private void LoadTasksFromXml()
        {
            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(databaseFilename);
                XmlNodeList taskNodes = xmlDoc.SelectNodes("/tasks/task");
                root = xmlDoc.ChildNodes[0] as XmlElement;

                foreach (XmlNode taskNode in taskNodes)
                {
                    Task task = new Task();

                    task.Name = taskNode.SelectSingleNode("name").InnerText;
                    task.TimeCreated = DateTime.Parse(taskNode.SelectSingleNode("timecreated").InnerText);
                    task.Deadline = DateTime.Parse(taskNode.SelectSingleNode("deadline").InnerText);
                    task.Priority = taskNode.SelectSingleNode("priority").InnerText;

                    tasks.Add(task);
                    refreshDGV();

                }
            }
            catch (FileNotFoundException)
            {
                InitializeXmlDocument("tasks");
            }

        }



        private void SaveXmlDocument()
        {
            try
            {
                xmlDoc = new XmlDocument();
                root = xmlDoc.CreateElement("tasks");
                xmlDoc.AppendChild(root);
                foreach (Task task in tasks)
                {
                    XmlElement taskel = xmlDoc.CreateElement("task");
                    taskel.AppendChild(xmlDoc.CreateElement("name")).InnerText = task.Name;
                    taskel.AppendChild(xmlDoc.CreateElement("timecreated")).InnerText = task.TimeCreated.ToString();
                    taskel.AppendChild(xmlDoc.CreateElement("deadline")).InnerText = task.Deadline.ToString();
                    taskel.AppendChild(xmlDoc.CreateElement("priority")).InnerText = task.Priority.ToString();
                    root.AppendChild(taskel);
                }

                xmlDoc.Save(databaseFilename);
                MessageBox.Show("Data saved successfully.", "Success!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Error saving data: " + ex.Message, "Error!!!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    SaveXmlDocument();
                }
            }
        }

        private void InitializeXmlDocument(string element)
        {
            xmlDoc = new XmlDocument();
            root = xmlDoc.CreateElement(element);
            xmlDoc.AppendChild(root);
        }


        //buttons
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlTextReader reader = new XmlTextReader("database.xml");
        }

        private void refreshDGV()
        {
            dataGridView1.DataSource = typeof(List<>);
            dataGridView1.DataSource = tasks;
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 createTaskForm = new Form2(dataGridView1);
            createTaskForm.ShowDialog();
            tasks.Add(createTaskForm.returnTsk());

            refreshDGV();

        }


        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tasks.RemoveAt(dataGridView1.SelectedRows[0].Index);
            refreshDGV();
        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.SelectedRows[0].Index;
            int selectedColumn = dataGridView1.SelectedCells[0].ColumnIndex;
            string priority = dataGridView1.Rows[selectedRow].Cells[3].Value.ToString();

            Form2 createTaskForm = new Form2(dataGridView1, tasks[selectedRow], true, tasks[selectedRow].Name, tasks[selectedRow].Deadline, tasks[selectedRow].Priority);
            createTaskForm.ShowDialog();
            tasks[selectedRow] = createTaskForm.returnTsk();
            refreshDGV();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveXmlDocument();
        }

        private void creatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 infopage = new Form5();
            infopage.ShowDialog();
        }
    }
}