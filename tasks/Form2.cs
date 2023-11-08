using System.Runtime.InteropServices;

namespace tasks
{
    public partial class Form2 : Form
    {
        Task tasks;
        int selectedRow = 0;
        bool editmode = false;

        public Form2(DataGridView dgv, [Optional] Task tasks_, [Optional] bool editMode, [Optional] string task, [Optional] DateTime dt, [Optional] byte priority)
        {
            InitializeComponent();
            tasks = tasks_;
            if (editMode)
            {
                textBox1.Text = task;
                dateTimePicker1.Value = dt;
                switch (priority)
                {
                    case 0:
                        radioButton1.Checked = true;
                        break;
                    case 1:
                        radioButton2.Checked = true;
                        break;
                    case 2:
                        radioButton3.Checked = true;
                        break;
                    case 3:
                        radioButton4.Checked = true;
                        break;
                }
                textBox1.Text = task;
                dateTimePicker1.Value = dt;
                button1.Text = "Edit";
                selectedRow = dgv.SelectedRows[0].Index;
                editmode = editMode;
                this.Name = "Edit Task";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte priority = 0;
            if (radioButton1.Checked)
            {
                priority = 1;
            }
            else if (radioButton2.Checked)
            {
                priority = 2;
            }
            else if (radioButton3.Checked)
            {
                priority = 3;
            }
            else
            {
                priority = 4;
            }
            if (editmode)
            {
                //datagrid.Rows[selectedRow].Cells[0].Value = textBox1.Text;
                //datagrid.Rows[selectedRow].Cells[1].Value = DateTime.Now.ToString();
                //datagrid.Rows[selectedRow].Cells[2].Value = dateTimePicker1.Value;
                //datagrid.Rows[selectedRow].Cells[3].Value = priority;

                Task task = new Task();
                task.Name = textBox1.Text;

                task.TimeCreated = DateTime.Now;
                task.Deadline = dateTimePicker1.Value;
                task.Priority = priority;
                tasks = task;


                //(textBox1.Text, DateTime.Now.ToString(), dateTimePicker1.Value, priority);
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
            this.Close(); // Close the form
        }

        public Task returnTsk()
        {
            return tasks;
        }
    }
}
