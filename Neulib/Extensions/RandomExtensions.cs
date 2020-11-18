using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Neulib.Extensions
{
    public static class RandomExtensions
    // https://en.wikipedia.org/wiki/Probability_distribution
    // https://onlinelibrary.wiley.com/doi/pdf/10.1002/9781119197096.app03
    {
        public static (double, double) BoxMuller(this Random random, double sigma = 1d, double mu = 0d)
        // https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
        {
            double u1 = random.NextDouble();
            double u2 = random.NextDouble();
            double t = sigma * Sqrt(-2d / Log(u1));
            double z1 = t * Cos(2 * PI * u2) + mu;
            double z2 = t * Sin(2 * PI * u2) + mu;
            return (z1, z2);
        }

        public static double BoxMuller1(this Random random, double sigma = 1d, double mu = 0d)
        {
            (double z1, _) = BoxMuller(random, sigma, mu);
            return z1;
        }

        public static double Gamma(this Random random, double delta)
        // https://en.wikipedia.org/wiki/Gamma_distribution
        {
            double e = Exp(1d);
            double eps, eta;
            do
            {
                double u = random.NextDouble();
                double v = random.NextDouble();
                double w = random.NextDouble();
                if (u <= e / (e + delta))
                {
                    eps = Pow(v, 1 / delta);
                    eta = w * Pow(eps, delta - 1);
                }
                else
                {
                    eps = 1d - Log(v);
                    eta = w * Exp(-eps);
                }
            } while (eta > Pow(eps, delta - 1) * Exp(-eps));
            return eps;
        }

        /// <summary>
        /// Weibull
        /// </summary>
        /// <param name="random">Random number generator.</param>
        /// <param name="lambda">Scale parameter > 0. Rayleigh: Sqrt(2)*sigma.</param>
        /// <param name="k">Shape parameter > 0. Exponential: 1. Rayleigh: 2.</param>
        /// <returns>Weibull</returns>
        public static double Weibull(this Random random, double lambda, double k)
        // https://en.wikipedia.org/wiki/Weibull_distribution
        // https://en.wikipedia.org/wiki/Gamma_function
        // f(x; lambda, k) = (k/lambda) * (x/lambda)^(k-1) * exp(-(x/lambda)^k)
        // mean = lambda * Gamma(1 + 1/k), where Gamma = gamma function
        {
            double u = random.NextDouble();
            return lambda * Pow(-Log(u), 1d / k);
        }

    }
}
