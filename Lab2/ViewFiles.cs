using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class ViewFiles : Form
    {
        public ViewFiles()
        {
            InitializeComponent();

            string text1 = "", text2 = "", text3 = "";

            try
            {
                using (StreamReader fs = new StreamReader(@"1.txt", Encoding.Default))
                {
                    while (true)
                    {
                        string temp = fs.ReadLine();

                        if (temp == null) break;
                        text1 += temp + "\n";
                    }
                }

                using (StreamReader fs = new StreamReader(@"2.txt", Encoding.Default))
                {
                    while (true)
                    {
                        string temp = fs.ReadLine();

                        if (temp == null) break;
                        text2 += temp + "\n";
                    }
                }

                using (StreamReader fs = new StreamReader(@"3.txt", Encoding.Default))
                {
                    while (true)
                    {
                        string temp = fs.ReadLine();

                        if (temp == null) break;
                        text3 += temp + "\n";
                    }
                }
                this.richTextBox1.AppendText(text1);
                this.richTextBox2.AppendText(text2);
                this.richTextBox3.AppendText(text3);

            }
            catch
            {
                MessageBox.Show("Файлов нет", "Внимание");
            }

            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}
