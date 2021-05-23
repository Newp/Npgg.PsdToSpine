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

        private void MainForm_Load(object sender, EventArgs e)
        {

            string boneMapPath = "boneMap.json";

            var boneMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(boneMapPath));
            var result = Npgg.PsdToSpine.Converter.Convert("sample1.psd", boneMap);

            
        }
    }
}
