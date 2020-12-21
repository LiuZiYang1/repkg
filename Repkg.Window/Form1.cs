using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RePKG.Command;
using System.IO;

namespace Repkg.Window
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "PKG文件|*.pkg;";
            file.ShowDialog();
            textBox1.Text = file.FileName;
            textBox2.Text = Path.GetDirectoryName(file.FileName) + "\\" + Path.GetFileNameWithoutExtension(file.FileName);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            textBox2.Text = path.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var filePath = textBox1.Text;
            var savePath = textBox2.Text;
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(savePath))
            {
                MessageBox.Show("文件路径为空或者输出地址为空");
            }
            //判断有没有路径 有的话删除
            try
            {
                //如果存在这个文件夹删除之
                if (Directory.Exists(savePath))
                {
                    DeleteFolder(savePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败" + ex.Message + "点击确认将解压到带时间后缀的文件夹");
                savePath = savePath + DateTime.Now.ToString("_MMdd_HHmmss");
            }
            ExtractOptions extractOptions = new ExtractOptions()
            {
                Input = filePath,
                OutputDirectory = savePath
            };
            Extract.Action(extractOptions);
            System.Diagnostics.Process.Start(savePath);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/notscuffed/repkg");
            linkLabel1.LinkVisited = true;
        }

        /// <summary>
        /// 删除文件夹下所有文件
        /// </summary>
        /// <param name="dir"></param>
        public void DeleteFolder(string dir)
        {
            //如果存在这个文件夹删除之 
            if (Directory.Exists(dir))
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d);//直接删除其中的文件 
                    else DeleteFolder(d);//递归删除子文件夹  
                }
                Directory.Delete(dir);
            }
        }
    }
}
