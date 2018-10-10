using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {

        const int tasks = 100;

        int[] numFile = new int[tasks];
        char[] characters = new char[tasks];
        int[] count = new int[tasks];
        Random rd = new Random();
        public Form1()
        {
            InitializeComponent();
            this.dataGridView1.Columns.Add("Номер файла", "Номер файла");
            this.dataGridView1.Columns.Add("2", "Буква");
            this.dataGridView1.Columns.Add("3", "Количество букв");

            System.IO.File.Delete(@"1.txt");
            System.IO.File.Delete(@"2.txt");
            System.IO.File.Delete(@"3.txt");

            for (int i = 0; i < tasks; i++)
            {
                numFile[i] = rd.Next(1, 4);
                characters[i] = (char)rd.Next(0x0410, 0x44F);
                count[i] = rd.Next(1, 101);
            }

            for (int i = 0; i < tasks; i++)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[i].Cells[0].Value = numFile[i];
                this.dataGridView1.Rows[i].Cells[1].Value = characters[i];
                this.dataGridView1.Rows[i].Cells[2].Value = count[i];
            }  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Semaphore semaphoreObject = new Semaphore(initialCount: 5, maximumCount: 5, name: "PrinterApp");
            Printer printerObject = new Printer();
            for (int i = 0; i < 50; i++)
            {
                int j = i;
                Task.Factory.StartNew(() =>
                {
                    semaphoreObject.WaitOne();
                    Thread.CurrentThread.Name = rd.Next(1, 6).ToString();
                    printerObject.Print(numFile[j], characters[j], count[j], Thread.CurrentThread.Name);
                    semaphoreObject.Release();
                });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewFiles v = new ViewFiles();
            v.Show();
        }
    }

    class Printer
    {
        string file;
        static object locker = new object();
        public void Print(int numFile, char ch, int count, string name)
        {
            lock (locker)
            {
                switch (numFile)
                {
                    case 1: file = @"1.txt"; break;
                    case 2: file = @"2.txt"; break;
                    case 3: file = @"3.txt"; break;
                }
                try
                {
                    using (StreamWriter sw = new StreamWriter(file, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("-= Поток {0}=- ", name);
                        for (int i = 0; i <= count; i++)
                        {
                            sw.Write(ch);
                            sw.Write(" ");
                        }
                        sw.WriteLine();
                        sw.WriteLine("-= Поток {0}=- ", name);
                        sw.WriteLine();
                        sw.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
