using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;

namespace Neulib.Numerics
{
    public static class Interpolation
    {

        public static double Evaluate(double v00, double v01, double v10, double v11, double fu, double fv)
        {
            double sum = 0d, count = 0d, value;
            value = v00; if (!double.IsNaN(value)) { double w = (1 - fu) * (1 - fv); sum += w * value; count += w; }
            value = v01; if (!double.IsNaN(value)) { double w = (1 - fu) * fv; sum += w * value; count += w; }
            value = v10; if (!double.IsNaN(value)) { double w = fu * (1 - fv); sum += w * value; count += w; }
            value = v11; if (!double.IsNaN(value)) { double w = fu * fv; sum += w * value; count += w; }
            if (count > 0) sum /= count; else sum = double.NaN;
            return sum;
        }
        public static Double3 Evaluate(Double3 v00, Double3 v01, Double3 v10, Double3 v11, double fu, double fv)
        {
            Double3 sum = new Double3(0d, 0d, 0d), value;
            double count = 0d;
            value = v00; if (!Double3.IsNaN(value)) { double w = (1 - fu) * (1 - fv); sum += w * value; count += w; }
            value = v01; if (!Double3.IsNaN(value)) { double w = (1 - fu) * fv; sum += w * value; count += w; }
            value = v10; if (!Double3.IsNaN(value)) { double w = fu * (1 - fv); sum += w * value; count += w; }
            value = v11; if (!Double3.IsNaN(value)) { double w = fu * fv; sum += w * value; count += w; }
            if (count > 0) sum /= count; else sum = Double3.NaN;
            return sum;
        }

        public static Double2 Evaluate(Double2 v00, Double2 v01, Double2 v10, Double2 v11, double fu, double fv)
        {
            Double2 sum = new Double2(0d, 0d), value;
            double count = 0d;
            value = v00; if (!Double2.IsNaN(value)) { double w = (1 - fu) * (1 - fv); sum += w * value; count += w; }
            value = v01; if (!Double2.IsNaN(value)) { double w = (1 - fu) * fv; sum += w * value; count += w; }
            value = v10; if (!Double2.IsNaN(value)) { double w = fu * (1 - fv); sum += w * value; count += w; }
            value = v11; if (!Double2.IsNaN(value)) { double w = fu * fv; sum += w * value; count += w; }
            if (count > 0) sum /= count; else sum = Double2.NaN;
            return sum;
        }

        public static void InterpolateFrom(int uCount1, int vCount1, int uCount2, int vCount2, bool uPeriodic, bool vPeriodic, Action<int, int, int, int, int, int, double, double> action)
        {
            for (int i1 = 0; i1 < uCount1; i1++)
            {
                double u = ParamRange.Val_static(i1, uCount1, uPeriodic);
                double fu = ParamRange.Inv_static(u, uCount2, uPeriodic);
                int i21 = ToInt32(fu);
                if (i21 < 0) i21 = 0; else if (i21 > uCount2 - 2) i21 = uCount2 - 2;
                fu -= i21;
                int i22 = (i21 + 1) % uCount2;
                for (int j1 = 0; j1 < vCount1; j1++)
                {
                    double v = ParamRange.Val_static(j1, vCount1, vPeriodic);
                    double fv = ParamRange.Inv_static(v, vCount2, vPeriodic);
                    int j21 = ToInt32(fv);
                    if (j21 < 0) j21 = 0; else if (j21 > vCount2 - 2) j21 = vCount2 - 2;
                    fv -= j21;
                    int j22 = (j21 + 1) % vCount2;
                    action(i1, j1, i21, i22, j21, j22, fu, fv);
                }
            }
        }

    }
}
