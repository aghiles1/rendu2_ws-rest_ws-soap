using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            IntermediateAsync.getTownsAsync(client,comboBox2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int seconds = 0;
            if (comboBox2.Text != "" && comboBox1.Text != "") {
                if(textBox2.Text != "")
                {
                   seconds = Convert.ToInt32(textBox2.Text);
                }
                IntermediateAsync.getAvailableBikesAsync(client, comboBox2.Text, comboBox1.Text, seconds, textBox1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.ResetText();
            if (comboBox2.Text != "")
            {
                IntermediateAsync.getStationsAsync(client, comboBox2.Text,comboBox1);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
