using Newtonsoft.Json;
using Npgg.PsdToSpine;
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


namespace Npgg.PsdToSpineGUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        static readonly Encoding encoding = new UTF8Encoding(false);
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.PsdFileLabel.Text = "G:/내 드라이브/기본케릭터/character1.psd";
            this.BoneMapLabel.Text = "G:/내 드라이브/기본케릭터/character1_config.json";
        }

        private void FileSelectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            var dialogResult = dialog.ShowDialog(this);

            if (dialogResult != DialogResult.OK)
                return;

            PsdFileLabel.Text = dialog.FileName;

        }

        private void BoneMapSelectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            var dialogResult = dialog.ShowDialog(this);

            if (dialogResult != DialogResult.OK)
                return;

            BoneMapLabel.Text = dialog.FileName;
        }


        private void ConvertButton_Click(object sender, EventArgs e)
        {
            string filename = this.PsdFileLabel.Text;
            string boneMapPath = this.BoneMapLabel.Text;
            var xx = File.ReadAllText(boneMapPath, encoding);
            var config = JsonConvert.DeserializeObject<Configuration>(xx);

            FileInfo file = new FileInfo(filename);

            var result = Npgg.PsdToSpine.Converter.Convert(filename, config, file.Directory.FullName);

            
            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText(filename + ".json", json, encoding);

            MessageBox.Show("완료");
        }
    }

}

