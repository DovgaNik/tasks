using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace DOVHANProject
{
    public partial class Form2 : Form
    {
        DataGridView datagrid;
        int selectedRow = 0;
        bool editmode = false;

        public Form2(DataGridView dgv, [Optional] bool editMode, [Optional] string task, [Optional] DateTime dt, [Optional] byte priority)
        {
            InitializeComponent();
            datagrid = dgv;
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
            string priority = "";
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
            else
            {
                priority = "Urgent";
            }
            if (editmode)
            {
                datagrid.Rows[selectedRow].Cells[0].Value = textBox1.Text;
                //datagrid.Rows[selectedRow].Cells[1].Value = DateTime.Now.ToString();
                datagrid.Rows[selectedRow].Cells[2].Value = dateTimePicker1.Value;
                datagrid.Rows[selectedRow].Cells[3].Value = priority;



                //(textBox1.Text, DateTime.Now.ToString(), dateTimePicker1.Value, priority);
            }
            else
            {
                datagrid.Rows.Add(textBox1.Text, DateTime.Now.ToString(), dateTimePicker1.Value, priority);
            }
            this.Close(); // Close the form
        }
    }
}
