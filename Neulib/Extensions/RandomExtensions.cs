using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Neulib.Extensions
{
    public static class RandomExtensions
    {
        public static  (double, double) BoxMuller(this Random random, double sigma = 1d, double mu = 0d)
        // https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
        {
            double u1 = random.NextDouble();
            double u2 = random.NextDouble();
            double t = sigma * Sqrt(-2d / Log(u1));
            double z1 = t * Cos(2 * PI * u2) + mu;
            double z2 = t * Sin(2 * PI * u2) + mu;
            return (z1, z2);
        }


    }
}
