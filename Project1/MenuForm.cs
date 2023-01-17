using System;
using System.Windows.Forms;

namespace FishGame
{
    public partial class MenuForm : Form
    {
        public int SensorsAmount { get { return Int32.Parse(enterSensorAmount.SelectedItem.ToString()); } }
        public int HiddenNeuronAmount { get { return Int32.Parse(enterHiddenNeuronAmount.SelectedItem.ToString()); } }
        public int FishAmount { get { return Int32.Parse(enterFishAmount.SelectedItem.ToString()); } }

        public RunMode RunMode { get; set; } = RunMode.Simulation;

        public MenuForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            enterSensorAmount.SelectedItem = "6";
            enterHiddenNeuronAmount.SelectedItem = "6";
            enterFishAmount.SelectedItem = "10";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RunMode = RunMode.Solo;
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
