using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neunet.Forms
{
    public partial class TestFloatForm : Form
    {
        
        
        public TestFloatForm()
        {
            InitializeComponent();
        }


        public static float FloatTest(int n)
        {
            float a = 1.11493569f, b = 0.175481986f, c = 2.682294563f;
            float x = 0f;
            for (int i = 0; i < n; i++)
            {
                x = c;
                x = a * x + b;
            }
            return x;
        }

        public static double DoubleTest(int n)
        {
            double a = 1.11493569d, b = 0.175481986d, c = 2.682294563d;
            double x = 0d;
            for (int i = 0; i < n; i++)
            {
                x = c;
                x = a * x + b;
            }
            return x;
        }

        public static double HybrideTest(int n)
        {
            float a = 1.11493569f, b = 0.175481986f, c = 2.682294563f;
            double x = 0f;
            for (int i = 0; i < n; i++)
            {
                x = c;
                x = a * x + b;
            }
            return x;
        }



        private void RunButton_Click(object sender, EventArgs e)
        {
            const int n = 1000000000;
            
            Stopwatch timer = new Stopwatch();
            timer.Start();
            double yd = DoubleTest(n);
            label1.Text = $"Double {yd} in {timer.Elapsed.TotalSeconds} s.";

            timer.Restart();
            float yf = FloatTest(n);
            label2.Text = $"Float {yf} in {timer.Elapsed.TotalSeconds} s.";

            timer.Restart();
            double yh = HybrideTest(n);
            label3.Text = $"Hybride {yh} in {timer.Elapsed.TotalSeconds} s.";

            timer.Stop();
        }
    }
}
