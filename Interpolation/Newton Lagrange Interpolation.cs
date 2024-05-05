using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AngouriMath.Extensions;

namespace Численные_методы_ЛР_3._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = true;
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Введите координаты 1ой точки", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("Введите координаты 2ой точки", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Введите координаты 3ей точки", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }
                else if (textBox4.Text == "")
                {
                    MessageBox.Show("Введите координаты 4ой точки", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }

                if (flag)
                {
                    double[] x = new double[4];
                    double[] y = new double[4];

                    double[] arr = textBox1.Text.Split(new[] { ' ', '[', ']', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(Double.Parse).ToArray();
                    x[0] = arr[0];
                    y[0] = arr[1];
                    arr = textBox2.Text.Split(new[] { ' ', '[', ']', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(Double.Parse).ToArray();
                    x[1] = arr[0];
                    y[1] = arr[1];
                    arr = textBox3.Text.Split(new[] { ' ', '[', ']', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(Double.Parse).ToArray();
                    x[2] = arr[0];
                    y[2] = arr[1];
                    arr = textBox4.Text.Split(new[] { ' ', '[', ']', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(Double.Parse).ToArray();
                    x[3] = arr[0];
                    y[3] = arr[1];

                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    Lagrange(x, y);

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные данные", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Введите корректные данные, исходя из области определения x", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void Lagrange(double[] x, double[] y)
        {
            double a1 = y[0] / ((x[0] - x[1]) * (x[0] - x[2]) * (x[0] - x[3]));
            double a2 = y[1] / ((x[1] - x[0]) * (x[1] - x[2]) * (x[1] - x[3]));
            double a3 = y[2] / ((x[2] - x[0]) * (x[2] - x[1]) * (x[2] - x[3]));
            double a4 = y[3] / ((x[3] - x[0]) * (x[3] - x[1]) * (x[3] - x[2]));
            string expr = $"{a1}*((x-{x[1]})*(x-{x[2]})*(x-{x[3]}))  +" + $"{a2}*((x-{x[0]})*(x-{x[2]})*(x-{x[3]})) +" +
               $"{a3}*((x-{x[0]})*(x-{x[1]})*(x-{x[3]})) +" + $"{a4}*((x-{x[0]})*(x-{x[1]})*(x-{x[2]}))";

            var compiled = expr.Compile("x");

            string expr1 = $"{a2}*((x-{x[0]})*(x-{x[2]})*(x-{x[3]})) " +
               $"{a3}*((x-{x[0]})*(x-{x[1]})*(x-{x[3]})) +" + $"{a4}*((x-{x[0]})*(x-{x[1]})*(x-{x[2]}))";

            textBox5.Text = expr1;

            double x_ = x[0];
            double y_ = y[0];
            double step = 0.0005;
            while (x_ <= x[3])
            {
                y_ = compiled.Substitute(x_).Real;
                chart1.Series[0].Points.AddXY(x_, y_);
                x_ += step;
            }

            double temp = compiled.Substitute(3 * Math.PI / 16).Real;
            textBox6.Text = temp.ToString();
            textBox7.Text = (Math.Tan(3 * Math.PI / 16) - temp).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool flag = true;
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Введите координаты 1ой точки", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("Введите координаты 2ой точки", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Введите координаты 3ей точки", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }
                else if (textBox4.Text == "")
                {
                    MessageBox.Show("Введите координаты 4ой точки", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }

                if (flag)
                {
                    double[] x = new double[4];
                    double[] y = new double[4];

                    double[] arr = textBox1.Text.Split(new[] { ' ', '[', ']', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(Double.Parse).ToArray();
                    x[0] = arr[0];
                    y[0] = arr[1];
                    arr = textBox2.Text.Split(new[] { ' ', '[', ']', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(Double.Parse).ToArray();
                    x[1] = arr[0];
                    y[1] = arr[1];
                    arr = textBox3.Text.Split(new[] { ' ', '[', ']', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(Double.Parse).ToArray();
                    x[2] = arr[0];
                    y[2] = arr[1];
                    arr = textBox4.Text.Split(new[] { ' ', '[', ']', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(Double.Parse).ToArray();
                    x[3] = arr[0];
                    y[3] = arr[1];

                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    Newton(x, y);

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные данные", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Введите корректные данные, исходя из области определения x", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Newton(double[] x, double[] y)
        {
            double[] arr1 = new double[y.Length - 1];
            for(int i = 1; i <= arr1.Length; i++)
            {
                arr1[i - 1] = (y[i - 1] - y[i]) / (x[i - 1] - x[i]);
            }

            double[] arr2 = new double[y.Length - 2];
            for (int i = 2; i <= arr2.Length + 1; i++)
            {
                arr2[i - 2] = (arr1[i - 2] - arr1[i - 1]) / (x[i - 2] - x[i]);
            }

            double arr3 = (arr2[0] - arr2[1]) / (x[0] - x[3]);


            string expr = $"{arr1[0]}*(x - {x[0]}) + {arr2[0]} *(x - {x[0]})*(x - {x[1]}) + {arr3}*(x - {x[0]})*(x - {x[1]})*(x - {x[2]})";
            var compiled = expr.Compile("x");
            textBox9.Text = expr;

            double x_ = x[0];
            double y_ = y[0];
            double step = 0.0005;
            while (x_ <= x[3])
            {
                y_ = compiled.Substitute(x_).Real;
                chart1.Series[1].Points.AddXY(x_, y_);
                x_ += step;
            }

            double temp = compiled.Substitute(3 * Math.PI / 16).Real;
            textBox8.Text = temp.ToString();
            textBox10.Text = (Math.Tan(3 * Math.PI / 16) - temp).ToString();
        }
    }
}
