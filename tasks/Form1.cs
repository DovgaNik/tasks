using System.Collections.Generic;
using System.Xml;

namespace tasks
{
    public partial class Form1 : Form
    {
        List<Task> tasks = new List<Task>(); string loggedInUsername;
        string databaseFilename = "";
        XmlDocument xmlDoc;

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
                    MessageBox.Show("Logged in as " + loggedInUsername);
                    databaseFilename = loggedInUsername + ".xml";
                    break;
                }
                else if (loggedInUsername == "")
                {
                    System.Windows.Forms.Application.Exit();
                }

            }


            LoadTasksFromXml();

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

                foreach (XmlNode taskNode in taskNodes)
                {
                    Task task = new Task();

                    task.Name = taskNode.SelectSingleNode("name").InnerText;
                    task.TimeCreated = DateTime.Parse(taskNode.SelectSingleNode("timecreated").InnerText);
                    task.Deadline = DateTime.Parse(taskNode.SelectSingleNode("deadline").InnerText);
                    task.Priority = byte.Parse(taskNode.SelectSingleNode("priority").InnerText);

                    tasks.Add(task);
                }
            }
            catch (Exception ex)
            {
                InitializeXmlDocument("task");
            }
        }



        private void SaveXmlDocument()
        {
            try
            {
                xmlDoc.Save(databaseFilename);
                MessageBox.Show("Data saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
        }

        private void InitializeXmlDocument(string element)
        {
            xmlDoc = new XmlDocument();

            XmlElement root = xmlDoc.CreateElement(element);
            xmlDoc.AppendChild(root);
        }



        //buttons
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlTextReader reader = new XmlTextReader("database.xml");
        }


        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 createTaskForm = new Form2(dataGridView1);
            createTaskForm.ShowDialog();
            tasks.Add(createTaskForm.returnTsk());
            dataGridView1.DataSource = typeof(List<>);
            dataGridView1.DataSource = tasks;
            //dataGridView1.Rows.Add(createTaskForm.returnTsk().Name, createTaskForm.returnTsk().TimeCreated, createTaskForm.returnTsk().Deadline, createTaskForm.returnTsk().Priority);
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

            //Form2 createTaskForm = new Form2(dataGridView1, true, dataGridView1.Rows[selectedRow].Cells[0].Value.ToString(), DateTime.Parse(dataGridView1.Rows[selectedRow].Cells[2].Value.ToString()), priority);
            Form2 createTaskForm = new Form2(dataGridView1, tasks[selectedRow], true, tasks[selectedRow].Name, tasks[selectedRow].Deadline, tasks[selectedRow].Priority);
            createTaskForm.ShowDialog();
            tasks.Add(createTaskForm.returnTsk());
        }
    }
}