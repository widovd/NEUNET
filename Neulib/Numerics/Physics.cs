using System;
using Neulib.Extensions;
using static System.Math;

namespace Neulib.Numerics
{
    public static class Physics
    {

        public static double CriticalAngle(double refrIx1, double refrIx2)
        {
            if (refrIx1 <= refrIx2) return 90d;
            return Asin(refrIx2 / refrIx1).ToDegrees();
        }

        public static Double3 NormalVectorFromRays(Double3 prevDirection, Double3 direction, double refrIx1, double refrIx2)
        {
            Double3 normalVector = (refrIx2 * direction - refrIx1 * prevDirection).Normalize();
            if (prevDirection * normalVector < 0) normalVector = -normalVector;
            return normalVector;
        }

        public static Double3 RefractRay(Double3 prevDirection, Double3 surfaceNormal, double refrIx1, double refrIx2, out bool tir)
        {
            double r = refrIx1 / refrIx2;
            double c = prevDirection * surfaceNormal;
            double s = Sign(c);
            c = Abs(c);
            double b = 1d - (r * r) * (1d - c * c);
            tir = b <= 0d;
            double a;
            if (tir)
            {
                a = 2d * c;
                r = 1d;
            }
            else
            {
                a = r * c - Sqrt(b);
            }
            a *= s;
            return r * prevDirection - a * surfaceNormal;
        }

        public static Double3 ReflectRay(Double3 prevDirection, Double3 surfaceNormal)
        {
            double a = 2d * prevDirection * surfaceNormal;
            return prevDirection - a * surfaceNormal;
        }

        /// <summary>
        /// Calculates the fresnel reflectance of unpolarized light.
        /// </summary>
        /// <param name="Theta">The angle of incidence [deg]</param>
        /// <param name="n1">Index of refraction before incidence.</param>
        /// <param name="n2">Index of refraction after incidence.</param>
        /// <returns>The reflectance.</returns>
        public static double FresnelReflection(double Theta, double n1, double n2)
        {
            double t = Theta.ToRadians();
            double cs = Cos(t);
            double sn = Sin(t);
            if (n2 == 0d) return 0d;
            double r = n1 / n2;
            double a = r * cs;
            double b = r * sn;
            b = 1d - b * b;
            if (b < 0d) return 0d;
            b = Sqrt(b);
            double Rs = (a - b) / (a + b);
            Rs *= Rs;
            a = cs / r;
            double Rp = (b - a) / (b + a);
            Rp *= Rp;
            return (Rs + Rp) / 2d;
        }

        /// <summary>
        /// Calculates the fresnel reflectance of unpolarized light.
        /// </summary>
        /// <param name="k1">The propagation direction of the ray before incidence.</param>
        /// <param name="un">The normal vector of the surface.</param>
        /// <param name="n1">Index of refraction before incidence.</param>
        /// <param name="n2">Index of refraction after incidence.</param>
        /// <returns>The reflectance.</returns>
        public static double FresnelReflection(Double3 k1, Double3 un, double n1, double n2)
        {
            double theta = LinearAlgebra.AngleBetweenVectors(k1, un);
            if (theta > 90d) theta = 180d - theta; // niet zo mooi
            return FresnelReflection(theta, n1, n2);
        }

        /// <summary>
        /// Part of the LensHull algorithm.
        /// Calculates lambda of the virtual ray of the current surface which forms a cartesian oval
        /// that refracts all corresponding incoming rays to the same point on the next surface.
        /// </summary>
        /// <param name="t1">Outgoing direction of the reference ray at the current surface: S1[k]</param>
        /// <param name="s1">Ingoing direction of the reference ray at the current surface: S1[k-1]</param>
        /// <param name="w1">Surface point of the reference ray at the previous surface: W1[k-1]</param>
        /// <param name="o1">Optical path length of the refrerence ray from the (point-?) source to the previous surface: O1[k-1]</param>
        /// <param name="l1">Lambda of the reference ray of the current surface: L1[k]</param>
        /// <param name="lt">Lambda of the reference ray of the next (target-) surface: L1[k+1].
        /// When the target surface is at infinity (far field) use value 0d or double.NaN.</param>
        /// <param name="s2">Ingoing direction of the virtual ray at the current surface: S2[k-1]</param>
        /// <param name="w2">Surface point of the virtual ray at the previous surface: W2[k-1]</param>
        /// <param name="o2">Optical path length of the virtual ray from the (point-?) source to the previous surface: O2[k-1]</param>
        /// <param name="np">Index of refraction of the medium before the current surface: n[k-1]</param>
        /// <param name="nc">Index of refraction of the medium after the current surface: n[k]</param>
        /// <returns>Lambda of the virtual ray of the current surface which forms a cartesian oval with the reference ray.
        /// If a solution doesn't exist Double.NaN is returned.</returns>
        public static double LambdaCartesianOval(Double3 t1, Double3 s1, Double3 w1, double o1, double l1, double lt, Double3 s2, Double3 w2, double o2, double np, double nc)
        // See lensHull.cpp: void surfacePoint
        {
            //Double3 t1 = r1c.RayDirection; // S1[k]
            //Double3 s1 = r1p.RayDirection; // S1[k-1]
            //Double3 s2 = r2p.RayDirection; // S2[k-1]
            //Double3 w1 = r1p.SurfacePoint; // W1[k-1]
            //Double3 w2 = r2p.SurfacePoint; // W2[k-1]
            //double o1 = r1p.OpticalPathLength; // O1[k-1]
            //double o2 = r2p.OpticalPathLength; // O2[k-1]
            //double l1 = r1c.Lambda; // L1[k]
            // farfield = r1n == null || double.IsNaN(r1n.Lambda) || r1n.Lambda <= 0d
            //double lt = r1n.Lambda; // L1[k+1]
            double npc = np / nc; // n[k-1]/n[k]
            double dop = (o1 - o2) / nc;
            Double3 G = w1 - w2 + l1 * s1;
            double l2 = double.NaN; // default
            if (double.IsNaN(lt) || lt <= 0d) // far field
            {
                double a1 = dop + npc * l1 - G * t1;
                double a2 = npc - s2 * t1;
                l2 = a1 / a2;
            }
            else // near field
            {
                Double3 H = G + lt * t1;
                double h = dop + npc * l1 + lt;
                double a = 1d - npc.Sqr();
                double b = 2d * (npc * h - H * s2);
                double c = H * H - h.Sqr();
                if (a == 0d) // np == nc: same medium
                {
                    if (b != 0d) l2 = -c / b;
                }
                else // a != 0d
                {
                    if (c == 0d)
                    {
                        l2 = -b / a;
                    }
                    else // c != 0d
                    {
                        double f = lt * (s2 * t1 - npc); // Lt always > 0 ?!
                        double e = b * b - 4d * a * c;
                        if (e >= 0d)
                        {
                            e = Sqrt(e);
                            if (b < 0d) e = -e;
                            if ((f > 0d) ^ (b >= 0d))
                                l2 = -(2d * c) / (b + e);
                            else
                                l2 = -(b + e) / (2d * a);
                        }
                    }
                }
            }
            return l2;
        }


    }


}
