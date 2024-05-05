using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Численные_методы_ЛР_1._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

		bool first = true;
		int height = 0;
		double determinant = 1;
		private void button1_Click(object sender, EventArgs e)
        {
			richTextBox1.Text = "";
			richTextBox2.Text = "";
			determinant = 1;
			bool flag = true;

			try
			{
				if (textBox1.Text == "")
				{
					MessageBox.Show("Введите коэффициенты матрицы.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					flag = false;
				}
				else if (textBox2.Text == "")
				{
					MessageBox.Show("Введите коэффициенты столбца свободных членов.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					flag = false;
				}

				if (flag)
				{
					if (first)
					{
						height = richTextBox1.Height;
						first = false;
					}

					var mas1 = textBox1.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
					double[,] matrix = StringToDouble(mas1);

					double[] column = textBox2.Text.Split(new[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(Double.Parse).ToArray();

					richTextBox1.Height = height / 3 * (matrix.GetLength(0)) - 3;
					richTextBox2.Height = height / 3 * (matrix.GetLength(0)) - 3;

					richTextBox1.Text = Print(matrix, column);

					double[,] arr = new double[column.Length, 1];

					for (int i = 0; i < column.Length; i++)
					{
						arr[i, 0] = column[i];
					}

					double[,] matr = new double[matrix.GetLength(0), matrix.GetLength(1)];
					for (int i = 0; i < matrix.GetLength(0); i++)
						for (int j = 0; j < matrix.GetLength(1); j++)
							matr[i, j] = matrix[i, j];

					List<double> result = Gauss(matrix, column);

					int m = 1;
					for (int i = 0; i < result.Count; i++)
					{
						//if ((1 - Math.Abs((Math.Truncate(result[i]) - result[i]))) < 0.1 || (1 - Math.Abs((Math.Truncate(result[i]) - result[i])) > 0.9))
							richTextBox2.Text += "x" + m.ToString() + " = " + String.Format("{0:f2}", result[i]) + "\n";
						//else richTextBox2.Text += "x" + m.ToString() + " = " + String.Format("{0:f2}", result[i]) + "\n";

						m++;
					}

					textBox3.Text = determinant.ToString();
                    richTextBox3.Text = PrintMatrix(InverseMatrix(matr));
					richTextBox4.Text = PrintMatrix(MultyMatrix(matrix, InverseMatrix(matr)));

					richTextBox2.Height -= 5;
				}
			}
			catch (FormatException)
			{
				MessageBox.Show("Введите корректные данные", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textBox1.Text = "";
			}
		}

        private void button2_Click(object sender, EventArgs e)
        {
			textBox1.Text = "";
			textBox2.Text = "";
			textBox3.Text = "";
			richTextBox1.Text = "";
			richTextBox2.Text = "";
			richTextBox3.Text = "";
			richTextBox4.Text = "";
		}

		private double[,] StringToDouble(string[] mas)
		{
			int i = 0, j = 0;
			bool temp = true;

			// определяем размерность матрицы полинома
			for (int k = 0; k < mas.Length; k++)
			{
				if (mas[k] == ";")
				{
					i++;
					temp = false;
				}
				else if (temp)
				{
					j++;
				}
			}

			double[,] matrix = new double[i + 1, j];

			i = j = 0;
			for (int k = 0; k < mas.Length; k++)
			{
				if (mas[k] == ";")
				{
					i++;
					j = 0;
					continue;
				}
				else
				{
					matrix[i, j] = Convert.ToDouble(mas[k]);
					j++;
				}
			}

			return matrix;
		}

		private string Print(double[,] matrix, double[] column)
		{
			string result = "";
			int n = 1;
			bool flag1 = false;
			// печать
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (matrix[i, j] != 0)
					{
						if (j == 0)
						{
							if (matrix[i, j] == 1)
							{
								result += "x" + n.ToString() + " ";
							}
							else if (matrix[i, j] == -1)
							{
								result += "-x" + n.ToString() + " ";
							}
							else result += String.Format("{0}", matrix[i, j]) + "x" + n.ToString() + " ";

							if (!flag1) flag1 = true;
						}
						else if (matrix[i, j] > 0)
						{
							if (!flag1)
							{
								if (matrix[i, j] == 1)
								{
									result += "x" + n.ToString() + " ";
								}
								else result += String.Format("{0}", matrix[i, j]) + "x" + n.ToString() + " ";
								flag1 = true;
							}
							else
							{
								if (matrix[i, j] == 1)
								{
									result += "+ x" + n.ToString() + " ";
								}
								else result += "+ " + String.Format("{0}", matrix[i, j]) + "x" + n.ToString() + " ";

								flag1 = true;
							}
						}
						else if (matrix[i, j] < 0)
						{
							if (matrix[i, j] == -1)
							{
								result += "-x" + n.ToString() + " ";
							}
							else result += String.Format("{0}", matrix[i, j]) + "x" + n.ToString() + " ";
						}
					}

					n++;
				}

				result += "= " + column[i].ToString() + "\n";
				n = 1;
				flag1 = false;
			}

			return result;
		}

		private string PrintMatrix(double[,] matrix)
        {
			string result = "";
			for(int i = 0; i < matrix.GetLength(0); i++)
            {
				for(int j = 0; j < matrix.GetLength(1); j++)
                {
					result += string.Format("{0:F3}",matrix[i, j]) + "  ";
                }

				if(i < matrix.GetLength(0) - 1) result += "\n";
            }

			return result;
        }


		private List<double> Gauss(double[,] matrix, double[] column)
		{
			List<double> roots = new List<double>();
			List<double> b = new List<double>();
			//int n = 0;
			int number = column.Length - 1;
			int m = 0;

			for (int n = 0; n < number; n++)
			{
				// Прямой ход
				if (matrix[n, n] == 0)
				{
					double[] arr = new double[matrix.GetLength(1)];
					double temp = 0;
					int index = 0;
					for (int k = 0; k < arr.Length; k++)
					{
						arr[k] = matrix[n, k];
					}

					for (int k = 0; k < matrix.GetLength(0); k++)
					{
						if (matrix[k, n] != 0) index = k;
					}

					for (int j = 0; j < matrix.GetLength(1); j++)
					{
						matrix[n, j] = matrix[index, j];
						matrix[index, j] = arr[j];
					}

					temp = column[n];
					column[n] = column[index];
					column[index] = temp;
				}

				for (int i = 1 + n; i < matrix.GetLength(0); i++)
				{
					double koef = matrix[i, n] / matrix[n, n];
					for (int j = 0; j < matrix.GetLength(1); j++)
					{
						matrix[i, j] -= koef * matrix[n, j];
					}
					column[i] -= koef * column[n];
				}
			}

			for (int i = 0; i < column.Length; i++)
				determinant *= matrix[i, i];

			// Обратный ход
			roots.Add(column[number] / matrix[number, number]);
			int l = number - 1;
			m = 1;
			double sum;
			while (l >= 0)
			{
				sum = 0;
				for (int i = 0; i < m; i++)
				{
					sum += matrix[l, number - i] * roots[i];
				}

				roots.Add((column[l] - sum) / matrix[l, l]);
				m++;
				l--;
			}

			roots.Reverse();

			return roots;
		}

		private double[,] InverseMatrix(double[,] matrix)
        {
			double[,] inverse = new double[matrix.GetLength(0), matrix.GetLength(1)];
			List<double> list = new List<double>();
			double[] column = new double[matrix.GetLength(0)];

			for(int i = 0; i < matrix.GetLength(0); i++)
            {
				column[i] = 1;
				list.AddRange(Gauss(matrix, column));
				column[i] = 0;
            }

			int k = 0;
			for (int j = 0; j < matrix.GetLength(1); j++)
			{
				for (int i = 0; i < matrix.GetLength(0); i++)
				{
					inverse[i, j] = list[k];
					k++;
				}
			}
			return inverse;
        }

		private double[,] MultyMatrix(double[,] A, double[,] B)
        {
			double[,] C = new double[A.GetLength(0), B.GetLength(1)];
			for (int i = 0; i < A.GetLength(0); i++)
			{
				for (int j = 0; j < B.GetLength(1); j++)
				{
					for (int k = 0; k < B.GetLength(0); k++)
					{
						C[i, j] += A[i, k] * B[k, j];
					}
				}
			}
			return C;
		}

        private void button3_Click(object sender, EventArgs e)
        {
			Form2 form = new Form2();
			form.ShowDialog();
        }
    }
}

// 9 -5 -6 3 ; 1 -7 1 0 ; -3 -4 9 0 ; 6 -1 9 8
// -8 38 47 -8
