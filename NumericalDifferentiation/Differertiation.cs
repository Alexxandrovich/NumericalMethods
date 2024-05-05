using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Численные_методы_ЛР_3._4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] x = new double[] { 1, 1.5, 2, 2.5, 3 };
            double[] y = new double[] { 0, 0.40547, 0.69135, 0.91629, 1.0986 };

            textBox1.Text = ((y[2] - y[1]) / (x[2] - x[1])).ToString();
            textBox2.Text = ((y[3] - y[2]) / (x[3] - x[2])).ToString();

            textBox3.Text = ((y[2] - y[1]) / (x[2] - x[1]) + (2 * 2 - x[1] - x[2])*((y[3] - y[2]) / (x[3] - x[2]) - (y[2] - y[1]) / (x[2] - x[1])) /
                (x[3] - x[1])).ToString();

            textBox4.Text = (2 * ((y[3] - y[2]) / (x[3] - x[2]) - (y[2] - y[1]) / (x[2] - x[1])) /
                (x[3] - x[1])).ToString();
        }
    }
}

//1, 1, 1.5, 2, 2.5, 3  // 0, 0.40547, 0.69135, 0.91629, 1.0986
//0, 0, 0.1, 0.2, 0.3, 0.4   // 1, 1.1052, 1.2214, 1.3499, 1.4918
