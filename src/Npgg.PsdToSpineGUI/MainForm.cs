using Newtonsoft.Json;
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
            string filename = "sample1.psd";
            string boneMapPath = "boneMap.json";

            var boneMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(boneMapPath, encoding));
            var result = Npgg.PsdToSpine.Converter.Convert(filename, boneMap);


            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText(filename + ".json", json, encoding);
        }
    }
}
