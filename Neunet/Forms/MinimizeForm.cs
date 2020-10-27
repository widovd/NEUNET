using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neulib.Numerics;
using Neulib.Exceptions;
using static Neulib.Extensions.FloatExtensions;
using Neunet.Forms;

namespace Neunet.Forms
{
    public partial class MinimizeForm : BaseForm
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private readonly Random _random = new Random();

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public MinimizeForm()
        {
            InitializeComponent();
            RandomizeFunction();
            RandomizePoint();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region MainForm

        private Multifunc multifunc;
        private float alpha = 0.01f;
        private int maxIter = 100;

        private void RandomizeFunction()
        {
            multifunc = new Multifunc(3);
            multifunc.Randomize(_random);
        }

        private void RandomizeFunctionButton_Click(object sender, EventArgs e)
        {
            try
            {
                RandomizeFunction();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }


        private float[] _point;

        private void RandomizePoint()
        {
            int n = multifunc.N;
            _point = new float[n];
            for (int i = 0; i < n; i++)
            {
                _point[i] = -1f + 2f * (float)_random.NextDouble();
            }
            Console.WriteLine($"new point: {Minimization.ArrayToString(_point)}");
            Console.WriteLine();
        }

        private void RandomizePointButton_Click(object sender, EventArgs e)
        {
            try
            {
                RandomizePoint();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void SteepestDescent()
        {
            Minimization minimization = new Minimization()
            {
                MaxIter = maxIter,
            };
            int n = multifunc.N;
            float[] p = new float[n];
            float[] df = new float[n];
            for (int i = 0; i < n; i++) p[i] = _point[i];
            minimization.SteepestDescent(p, df, () =>
            {
                return multifunc.Calculate(p, df);
            }, alpha);
            Console.WriteLine();
        }

        private void TestSteepestDescentButton_Click(object sender, EventArgs e)
        {
            try
            {
                SteepestDescent();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        private void ConjugateGradient()
        {
            Minimization minimization = new Minimization()
            {
                MaxIter = maxIter,
            };
            int n = multifunc.N;
            float[] p = new float[n];
            for (int i = 0; i < n; i++) p[i] = _point[i];
            minimization.ConjugateGradient(p, multifunc.Calculate, alpha);
            Console.WriteLine();
        }

        private void TestConjugateGradientButton_Click(object sender, EventArgs e)
        {
            try
            {
                ConjugateGradient();
            }
            catch (Exception ex)
            {
                ExceptionDialog.Show(ex);
            }
        }

        #endregion

    }
}
