using System.Runtime.InteropServices;

namespace tasks
{
    public partial class Form2 : Form
    {
        Task tasks;
        int selectedRow = 0;
        bool editmode = false;

        public Form2(DataGridView dgv, [Optional] Task tasks_, [Optional] bool editMode, [Optional] string task, [Optional] DateTime dt, [Optional] string priority)
        {
            InitializeComponent();
            tasks = tasks_;
            if (editMode)
            {
                textBox1.Text = task;
                dateTimePicker1.Value = dt;
                switch (priority)
                {
                    case "Low":
                        radioButton1.Checked = true;
                        break;
                    case "Normal":
                        radioButton2.Checked = true;
                        break;
                    case "High":
                        radioButton3.Checked = true;
                        break;
                    case "Urgent":
                        radioButton4.Checked = true;
                        break;
                    case "None":
                        radioButton5.Checked = true;
                        break;
                }
                textBox1.Text = task;
                dateTimePicker1.Value = dt;
                button1.Text = "Edit";
                selectedRow = dgv.SelectedRows[0].Index;
                editmode = editMode;
                this.Name = "Edit Task";
            }
            else
            {
                radioButton5.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string priority = "None";
            if (radioButton1.Checked)
            {
                priority = "Low";
            }
            else if (radioButton2.Checked)
            {
                priority = "Normal";
            }
            else if (radioButton3.Checked)
            {
                priority = "High";
            }
            else if (radioButton4.Checked)
            {
                priority = "Urgent";
            }
            else if (radioButton5.Checked)
            {
                priority = "None";
            }
            else
            {
                MessageBox.Show("You haven't selected any priority!", "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DateTime.Compare(dateTimePicker1.Value, DateTime.Now) < 0)
            {
                MessageBox.Show("You can't set a deadline in the past!", "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox1.Text == "")
            {
                MessageBox.Show("You haven't entered a task name!", "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (editmode)
            {

                Task task = new Task();
                task.Name = textBox1.Text;

                task.TimeCreated = DateTime.Now;
                task.Deadline = dateTimePicker1.Value;
                task.Priority = priority;
                tasks = task;

            }
            else
            {
                Task task = new Task();
                task.Name = textBox1.Text;

                task.TimeCreated = DateTime.Now;
                task.Deadline = dateTimePicker1.Value;
                task.Priority = priority;
                tasks = task;
            }
            this.Close();
        }

        public Task returnTsk()
        {
            return tasks;
        }
    }
}
