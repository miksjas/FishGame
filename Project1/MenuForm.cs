using System;
using System.Windows.Forms;

namespace FishGame
{
    public partial class MenuForm : Form
    {
        public int SensorsAmount { get { return Int32.Parse(enterSensorAmount.SelectedItem.ToString()); } }
        public int HiddenNeuronAmount { get { return Int32.Parse(enterHiddenNeuronAmount.SelectedItem.ToString()); } }
        public int FishAmount { get { return Int32.Parse(enterFishAmount.SelectedItem.ToString()); } }
        public MenuForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            enterSensorAmount.SelectedItem = "2";
            enterHiddenNeuronAmount.SelectedItem = "3";
            enterFishAmount.SelectedItem = "10";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void comboBox4_Loaded(object sender, EventArgs e)
        {
            enterFishAmount.SelectedIndex = 1;

        }

    }
}
