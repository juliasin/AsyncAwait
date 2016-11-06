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
using System.Diagnostics;

namespace AsyncAwait
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); using (StreamWriter DestinationWriter = new StreamWriter("FileLogger.txt", false)) { }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            if (path == "") { MessageBox.Show("Path is empty!"); return; }
            else
            {

                if (Path.GetExtension(path) != ".txt") { MessageBox.Show("File is not .txt!"); return; }
                else
                {

                    using (StreamReader SourceReader = File.OpenText(path))
                    {
                        using (StreamWriter DestinationWriter = new StreamWriter("FileLogger.txt", true))
                        {
                            await CopyFilesAsync(SourceReader, DestinationWriter);
                        }

                    }

                }
            }
        }

        public async Task CopyFilesAsync(StreamReader Source, StreamWriter Destination)
        {
            char[] buffer = new char[0x1000];
            int numRead;

            while ((numRead = await Source.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                await Destination.WriteAsync(buffer, 0, numRead);
            }
        }
    }
}
