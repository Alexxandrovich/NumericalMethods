using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабораторная_работа_3._5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Select();
        }

        double F(double x)
        {
            return x / Math.Pow(3 * x + 4, 3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double h1 = 0.5;
            double h2 = 0.25;
            double a = -1;
            double b = 1;

            int n = (int)((b - a) / h1); 

            double[] x = new double[n + 1];
            double[] y = new double[n + 1];

            for(int i = 0; i <= n; i++)
            {
                x[i] = a + i * h1;
                y[i] = F(x[i]);
            }

            double rect1 = Rectangle(x, y, h1);
            double trap1 = Trapeze(x, y, h1);
            double simp1 = Simpson(x, y, h1);

            textBox1.Text = rect1.ToString();
            textBox2.Text = trap1.ToString();
            textBox3.Text = simp1.ToString();

            n = (int)((b - a) / h2);
            x = new double[n + 1];
            y = new double[n + 1];
            for (int i = 0; i <= n; i++)
            {
                x[i] = a + i * h2;
                y[i] = F(x[i]);
            }

            double rect2 = Rectangle(x, y, h2);
            double trap2 = Trapeze(x, y, h2);
            double simp2 = Simpson(x, y, h2);

            textBox4.Text = rect2.ToString();
            textBox5.Text = trap2.ToString();
            textBox6.Text = simp2.ToString();

            textBox8.Text = (rect1 + (rect1 - rect2) / (1f / 4 - 1)).ToString();
            textBox9.Text = (trap1 + (trap1 - trap2) / (1f / 4 - 1)).ToString();
            textBox10.Text = (simp1 + (simp1 - simp2) / (1f / 4 - 1)).ToString();

            textBox11.Text = (Math.Abs(Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox8.Text))).ToString();
            textBox12.Text = (Math.Abs(Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox9.Text))).ToString();
            textBox13.Text = (Math.Abs(Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox10.Text))).ToString();
        }

        double Rectangle(double[] x, double[] y, double h)
        {
            double sum = 0;
            for (int i = 0; i < x.Length - 1; i++)
            {
                sum += h * F((x[i] + x[i + 1]) / 2);
            }

            return sum;
        }

        double Trapeze(double[] x, double[] y, double h)
        {
            double sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (i == 0 || i == x.Length - 1) sum += h * y[i] / 2;
                else sum += h * y[i];
            }

            return sum;
        }

        double Simpson(double[] x, double[] y, double h)
        {
            double sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (i == 0 || i == x.Length - 1) sum += h / 3 * y[i];
                else if (i % 2 == 0) sum += 2f / 3 * h * y[i];
                else sum += 4f / 3 * h * y[i];
            }

            return sum;

        }
    }
}
