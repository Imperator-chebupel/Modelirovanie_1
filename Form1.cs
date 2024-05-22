using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Моделирование_1
{
    public partial class Form1 : Form
    {

        public double[] massiv1;
        public double[] massiv2;
        public int razmer1, razmer2, intervals;
        public int[] kolichestvo1;
        public int[] kolichestvo2;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            int i;
            razmer1 = Int32.Parse(textBox1.Text);
            intervals = Int32.Parse(textBox3.Text);
            //double lambda = Double.Parse(textBox4.Text);
            massiv1 = new double[razmer1];
            kolichestvo1 = new int[intervals];
            for (i = 0; i < intervals; i++)
            {
                kolichestvo1[i] = 0;
            }
            int j;
            Random r = new Random();
            double u;
            for (i = 0; i < razmer1; i++)
            {
                u = r.NextDouble();
                massiv1[i] =  2 - 2 * Math.Sqrt(1.0 - u);//(u-1) / -0.5f; //(-Math.Log(u) / lambda);
            }

            double promezhutok1 = (massiv1.Max() - massiv1.Min()) / intervals;
            for (i = 0; i < razmer1; i++)//расчёт попаданий в промежуток
            {
                for (j = 0; j < intervals; j++)
                {

                    if ((massiv1[i] < ((promezhutok1) * (j + 1))) && (massiv1[i] >= ((promezhutok1) * (j))))
                    {
                        kolichestvo1[j]++;
                    }
                }
            }

            double granica1 = massiv1.Min();//, granica2 = massiv2.Min();
            int Y = 0;
            Array.Sort(massiv1);
            this.chart1.Series[0].Points.Clear();
            this.chart1.Series[1].Points.Clear();
            this.chart2.Series[0].Points.Clear();
            this.chart2.Series[1].Points.Clear();
            while (Y < intervals)
            {
                this.chart1.Series[0].Points.AddXY((granica1 + promezhutok1) / 2.0f, (Convert.ToDouble(kolichestvo1[Y])) / razmer1 / promezhutok1);
                granica1 = granica1 + promezhutok1;
                Y++;
            }

            granica1 = massiv1.Min();
            Y = 0;
            this.chart2.Series[0].Points.Clear();
            double Xi = 0.0;
            while (Y < intervals)
            {
                this.chart2.Series[0].Points.AddXY((granica1 + promezhutok1) / 2.0f, Convert.ToDouble(Summm(kolichestvo1, Y)) / razmer1);
                this.chart2.Series[1].Points.AddXY((granica1 + promezhutok1) / 2.0f, Func(granica1, 0));
                granica1 = granica1 + promezhutok1;

                if (Math.Abs(Convert.ToDouble(Summm(kolichestvo1, Y)) / razmer1 - Func(granica1, 0)) > Xi)
                    Xi = Math.Abs(Convert.ToDouble(Summm(kolichestvo1, Y)) / razmer1 - Func(granica1, 0));
                Y++;
            }
            richTextBox1.Text += Xi;
        }

        public static int Summm(int[] kol, int stop)//накопительная сумма
        {
            int sum = 0;
            for (int i = 0; i < stop + 1; i++)
            {
                sum += kol[i];
            }
            return sum;
        }

        public static double Func(double X, double lambda)
        {
            Random r = new Random();
            double u = r.NextDouble();
            return -X*X/4.0 + X;//1.0 - Math.Pow(Math.E, 0-lambda*X);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
