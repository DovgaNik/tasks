using System.Xml;

namespace tasks
{
    public partial class Form1 : Form
    {
        // Global variables of the form
        List<Task> tasks = new List<Task>(); string loggedInUsername;
        string databaseFilename = "";
        XmlDocument xmlDoc;
        XmlElement root;

        //Constructor
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

            // Bind tasks list to the DataGridView
            //dataGridView1.DataSource = tasks;

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
                    task.Priority = byte.Parse(taskNode.SelectSingleNode("priority").InnerText);

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
            xmlDoc.AppendChild(root); // Add the root element to the XmlDocument
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
            XmlElement taskel = xmlDoc.CreateElement("task");
            root.AppendChild(taskel);
            taskel.AppendChild(xmlDoc.CreateElement("name")).InnerText = createTaskForm.returnTsk().Name;
            taskel.AppendChild(xmlDoc.CreateElement("timecreated")).InnerText = createTaskForm.returnTsk().TimeCreated.ToString();
            taskel.AppendChild(xmlDoc.CreateElement("deadline")).InnerText = createTaskForm.returnTsk().Deadline.ToString();
            taskel.AppendChild(xmlDoc.CreateElement("priority")).InnerText = createTaskForm.returnTsk().Priority.ToString();
            refreshDGV();
            //dataGridView1.Rows.Add(createTaskForm.returnTsk().Name, createTaskForm.returnTsk().TimeCreated, createTaskForm.returnTsk().Deadline, createTaskForm.returnTsk().Priority);
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