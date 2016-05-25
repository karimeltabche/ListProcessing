using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ListProcessing
{
    public partial class frmListProcess : Form
    {
        List<int> list = new List<int>();

        public frmListProcess()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader inputFile;

            openFileDialog1.InitialDirectory = "H:\\";
            openFileDialog1.Title = "Open File";
            openFileDialog1.FileName = "";
            int num;

            list.Clear();
            lstProcess.Items.Clear();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    inputFile = File.OpenText(openFileDialog1.FileName);

                    while (!inputFile.EndOfStream)
                    {
                        num = int.Parse(inputFile.ReadLine());
                        lstProcess.Items.Add(num);
                        list.Add(num);
                    }
                    inputFile.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int search = int.Parse(textBox1.Text);
            int count = 0;
            int index = list.IndexOf(search);

            if (index == -1)
            {
                MessageBox.Show("Item not found.");
               
            }
            else
            {
                do
                {
                    count += 1;
                    index = list.IndexOf(search, index + 1);

                } while (index != -1);

                MessageBox.Show("Found the number " + textBox1.Text + "\nTimes found: " + count.ToString());

            }

        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            lstProcess.Items.Clear();

            list.Sort();

            foreach(int x in list)
            {
                lstProcess.Items.Add(x);
            }
        }

        private void btnHighest_Click(object sender, EventArgs e)
        {
                        
            int max = list.Max();

            MessageBox.Show("Highest value is " + max.ToString());
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox1.Text);
            lstProcess.Items.Remove(textBox1.Text);
            list.Remove(x);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int outParse;
            int x;
            x = int.Parse(textBox1.Text);

            if(Int32.TryParse(textBox1.Text, out outParse))
            {
                lstProcess.Items.Add(textBox1.Text);
                list.Add(x);
            }
            else if(textBox1.Text == "")
            {
                MessageBox.Show("Please enter a numeric value to add to the list");
            }
            else
            {
                MessageBox.Show("Please enter a numeric value to add to the list");
            }
        }

        private void btnTotal_Click(object sender, EventArgs e)
        {

            int i = 0, result = 0;
            while (i < list.Count)
            {
                result += Convert.ToInt32(lstProcess.Items[i++]);
            }
            MessageBox.Show("The total value is " + result.ToString(), "Total");
        }

        private void btnLowest_Click(object sender, EventArgs e)
        {

            int min = list.Min();

            MessageBox.Show("Lowest value is " + min.ToString());
        }

        private void btnMean_Click(object sender, EventArgs e)
        {
            double mean = 0;
            int i = 0;
            double total = 0;
            while (i < list.Count)
            {
                total += Convert.ToInt32(lstProcess.Items[i++]);
            }

            mean = total / lstProcess.Items.Count;
            MessageBox.Show("The mean is " + mean.ToString());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = "H:\\";
            saveFileDialog1.Title = "Save File";
            saveFileDialog1.FileName = "";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog1.FileName);

                foreach (object item in list)
                {
                    sw.WriteLine(item.ToString());
                }

                sw.Close();
            }
        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            int mode = list.GroupBy(v => v)
                        .OrderByDescending(g => g.Count())
                        .First()
                        .Key;
            MessageBox.Show("The mode is " + mode.ToString());
        }
    }
}
