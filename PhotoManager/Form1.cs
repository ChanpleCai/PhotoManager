using System;
using System.Windows.Forms;
using System.IO;

namespace PhotoManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //来源文件
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = "请选择文件源"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                label1.Text = label2.Text = dialog.SelectedPath;
                textBox1.Text = Path.GetFileName(dialog.SelectedPath);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //目标文件夹
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = "请选择目标文件夹"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                label2.Text = dialog.SelectedPath;
                textBox1.Text = Path.GetFileName(dialog.SelectedPath);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //开始
            if (string.IsNullOrEmpty(label1.Text) || string.IsNullOrEmpty(label2.Text) || string.IsNullOrEmpty(textBox1.Text))
                return;

            this.Enabled = false;
            string year = textBox1.Text.Trim();

            DirectoryInfo directory = new DirectoryInfo(label1.Text);

            foreach (var f in directory.GetFiles())
            {
                if (f.Name.Contains(year))
                {
                    //照片or视频中包含相应年份信息；
                    int i = f.Name.IndexOf(year) + 4;
                    string sub = f.Name.Substring(i, 4);
                    if (sub.Length == 4 && Convert.ToInt32(sub) != 0)
                    {
                        if (!Directory.Exists(Path.Combine(label2.Text, sub)))
                            Directory.CreateDirectory(Path.Combine(label2.Text, sub));

                        File.Move(f.FullName, Path.Combine(label2.Text, sub, f.Name));
                    }
                }
            }

            this.Enabled = true;
        }
    }
}
