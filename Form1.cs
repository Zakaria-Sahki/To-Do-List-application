using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace To_Do_List
{
    public partial class Form1 : Form
    {

        int NumberOfTasks = 0;
        int Percentage = 0;

        public Form1()
        {
            InitializeComponent();
        }
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            tvTasks.Nodes.Add(txtTask.Text);
            NumberOfTasks++;
            UpdateProgress();
            txtTask.Text = "";
            txtTask.Focus();

        }
        private void btnRemoveTask_Click(object sender, EventArgs e)
        {
            
            tvTasks.Nodes.Remove(tvTasks.SelectedNode);
            NumberOfTasks--;
            UpdateProgress();
        }
        private int CalculatePercentage() {

            return 100 / NumberOfTasks;
        }
        private void UpdateProgress() {

            Percentage = 0;
            progressBar1.Value = 0;
            foreach (TreeNode Node in tvTasks.Nodes) {

                if (Node.Checked) { 

                    Percentage += CalculatePercentage();
                    progressBar1.Value += CalculatePercentage();
                    progressBar1.Refresh();
                }

            }
            lblPercentage.Text = Percentage.ToString() + " %";
        }
        private void UpdateProgress(bool Checked)
        {
            if (Checked)
            {

                Percentage += CalculatePercentage();
                progressBar1.Value += CalculatePercentage();
                progressBar1.Refresh();
                IsAllChecked();
            }
            else {

                Percentage -= CalculatePercentage();
                progressBar1.Value -= CalculatePercentage();
                progressBar1.Refresh();
                IsAllNotChecked();
            }

            lblPercentage.Text = Percentage.ToString() + " %";
        }
        private void tvTasks_AfterCheck(object sender, TreeViewEventArgs e)
        {

            UpdateProgress(e.Node.Checked);
        }
        private bool IsAllChecked() {

            foreach (TreeNode Node in tvTasks.Nodes) { 
                
                if (!Node.Checked)
                    return false;
            }

            Percentage = 100;
            progressBar1.Value = 100;
            progressBar1.Refresh();
            return true;
        }
        private bool IsAllNotChecked()
        {

            foreach (TreeNode Node in tvTasks.Nodes)
            {

                if (Node.Checked)
                    return false;
            }

            Percentage = 0;
            progressBar1.Value = 0;
            progressBar1.Refresh();
            return true;
        }
        private void tsmDelete_Click(object sender, EventArgs e)
        {
            tvTasks.Nodes.Remove(tvTasks.SelectedNode);
            NumberOfTasks--;
            UpdateProgress();
        }
    }
}
