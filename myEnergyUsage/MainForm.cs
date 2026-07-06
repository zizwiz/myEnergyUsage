using System.Reflection;
using System.Windows.Forms;

namespace myEnergyUsage
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            Text += " : v" + Assembly.GetExecutingAssembly().GetName().Version; // put in the version number
        }

        private void btn_close_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
