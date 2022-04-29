using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserData
{
    public partial class UserData : Form
    {
        public UserData()
        {
            InitializeComponent();
        }

        private void usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //in order to save again:
            //databaseDataset ->
            //rightclick on usersTableAdaper and selecet configure ->
            //select advance and deselect use optimistic concurrency
            this.Validate();
            this.usersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseDataSet);

            Stream stream;
            DataTable dt = databaseDataSet.Users;
            var result = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == dt.Columns.Count - 1 ? "\n" : ",");
                }
            }
            
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = dialog.OpenFile()) != null)
                {
                    stream.Close();
                    File.AppendAllText(dialog.FileName, result.ToString());

                }
            }
        }
        

private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'databaseDataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.databaseDataSet.Users);
            DataTable dt = databaseDataSet.Users;
            dt.Rows.Add();
        }

        private void marital_StatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (marital_StatusComboBox.SelectedIndex == marital_StatusComboBox.FindStringExact("Married"))
                marital_StatusComboBox.Text = "Married";
            else if (marital_StatusComboBox.SelectedIndex == marital_StatusComboBox.FindStringExact("Divorced"))
                marital_StatusComboBox.Text = "Divorced";
            else if (marital_StatusComboBox.SelectedIndex == marital_StatusComboBox.FindStringExact("Widowed"))
                marital_StatusComboBox.Text = "Widowed";
            else if (marital_StatusComboBox.SelectedIndex == marital_StatusComboBox.FindStringExact("Single"))
                marital_StatusComboBox.Text = "Single";
            else
                marital_StatusComboBox.Text = "Other";
        }

        
       
        /*
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            
            int id = (int)usersDataGridView.CurrentRow.Cells[0].Value;
                        
            if (bindingNavigatorDeleteItem.Checked) {
                id -= 1;
            }
            databaseDataSet.Tables.Add(databaseDataSet.Users.IdColumn.ToString());


        }
        
        */
        



        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.ReadOnlyChecked = true;
            string path;
            DataTable dt = databaseDataSet.Users;
            dt.Rows.Clear();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filename = dialog.FileName;
                path = Path.GetFullPath(filename);
                string[] lines = File.ReadAllLines(path);
                Char[] split = new char[] { ',' };
                foreach (string col in lines[0].Split(','))
                {
                    dt.Columns.Add(new DataColumn(col));
                }
                for (int i = 0; i < lines.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    string[] items = lines[i].Split(split);
                    for (int j = 0; j < items.Length; j++)
                    {
                        dr[j] = items[j];
                    }

                    dt.Rows.Add(dr);
                }
                usersDataGridView.DataSource = dt;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.usersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseDataSet);

            Stream stream;
            DataTable dt = databaseDataSet.Users;
            var result = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == dt.Columns.Count - 1 ? "\n" : ",");
                }
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = dialog.OpenFile()) != null)
                {
                    stream.Close();
                    File.AppendAllText(dialog.FileName, result.ToString());

                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(System.Windows.Forms.Application.MessageLoop)
            {
                System.Windows.Forms.Application.Exit();
            } else
            {
                System.Environment.Exit(0);
            }
        }
    }
}
