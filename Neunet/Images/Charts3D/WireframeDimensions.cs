using System.ComponentModel;
using System;
using Neulib.Exceptions;
using Neulib.Numerics;
using static System.Math;

namespace Neunet.Images.Charts3D
{
    public class WireframeDimensions
    {
        // ----------------------------------------------------------------------------------------
        #region DimensionRange

        public struct WireframeRange
        {
            public float Min;

            public float Max;

            public float Sum;

            public long Count;

            public float Avg
            {
                get { return Count > 0 ? Sum / Count : float.NaN; }
            }

            public float Delta
            {
                get { return Count > 0 ? Max - Min : float.NaN; }
            }

            public WireframeRange(float min, float max, float sum, long count)
            {
                Min = min;
                Max = max;
                Sum = sum;
                Count = count;
            }

            public WireframeRange Add(float x)
            {
                float min = Min;
                float max = Max;
                float sum = Sum;
                long count = Count;
                if (!float.IsNaN(x))
                {
                    if (float.IsNaN(min) || x < min) min = x;
                    if (float.IsNaN(max) || x > max) max = x;
                    if (float.IsNaN(sum)) sum = x; else sum += x;
                    count++;
                }
                return new WireframeRange(min, max, sum, count);
            }

        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Properties

        public WireframeRange XRange { get; set; }

        public WireframeRange YRange { get; set; }

        public WireframeRange ZRange { get; set; }

        public float BiggestSize
        {
            get { return Max(Max(XRange.Delta, YRange.Delta), ZRange.Delta); }
        }

        public Single3 Center
        {
            get
            {
                if (XRange.Count == 0 || YRange.Count == 0 || ZRange.Count == 0) return Single3.NaN;
                return new Single3(XRange.Avg, YRange.Avg, ZRange.Avg);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public WireframeDimensions()
        {
            XRange = new WireframeRange(float.NaN, float.NaN, float.NaN, 0);
            YRange = new WireframeRange(float.NaN, float.NaN, float.NaN, 0);
            ZRange = new WireframeRange(float.NaN, float.NaN, float.NaN, 0);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region WireframeDimensions

        public void UpdateDimensions(Single3 origin)
        {
            XRange = XRange.Add(origin.X);
            YRange = YRange.Add(origin.Y);
            ZRange = ZRange.Add(origin.Z);
        }

        #endregion
    }
}
