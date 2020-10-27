using System;
using System.ComponentModel;
using System.Linq;
using static System.Convert;
using static System.Math;

namespace Neulib.Numerics
{
    public class MersenneArgs : EventArgs
    {
        public int Index { get; private set; }
        public uint Sample { get; private set; }
        public MersenneArgs(int index, uint sample)
        {
            Index = index;
            Sample = sample;
        }
    }

    public delegate void MersenneEventHandler(object sender, MersenneArgs e);

    public class Mersenne : Random
    //For a w-bit word length, the Mersenne Twister generates integers in the range [0, 2w−1]
    //https://en.wikipedia.org/wiki/Mersenne_Twister
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        [Description("Occurs every next sample")]
        public event MersenneEventHandler NextSample;

        protected virtual void OnNextSample(MersenneArgs e)
        {
            NextSample?.Invoke(this, e);
        }

        //w: word size(in number of bits)
        const int w = 32;
        //n: degree of recurrence
        const int n = 624;
        //m: middle word, an offset used in the recurrence relation defining the series x, 1 ≤ m<n
        const int m = 397;
        //r: separation point of one word, or the number of bits of the lower bitmask, 0 ≤ r ≤ w - 1
        const int r = 31;
        //a: coefficients of the rational normal form twist matrix
        const ulong a = 0x9908B0DF16U;

        private readonly ulong[] MT = new ulong[n];

        int index;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructor

        public Mersenne(ulong seed)
        {
            const ulong f = 1812433253;
            MT[0] = seed;
            for (ulong i = 1; i < n; i++)
            {
                ulong p = MT[i - 1];
                ulong q = p >> (w - 2);
                ulong v = f * (p ^ q) + i;
                MT[i] = v;
            }
            Twist();
        }

        public Mersenne(int seed) : this((ulong)seed)
        { }

        public Mersenne() : this((ulong)DateTime.Now.Ticks)
        { }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Random

        public override int Next()
        {
            return (int)(NextUInt() & 0x7FFFFFFF);
        }

        public override int Next(int minValue, int maxValue)
        {
            double x = NextDouble();
            return ToInt32(Floor(ToDouble(minValue) + ToDouble(maxValue - minValue) * x));
        }

        public override int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            int count = buffer.Length;
            int k = 0;
            uint x = 0;
            for (int i = 0; i < count; i++)
            {
                if (k == 0) x = NextUInt();
                buffer[i] = (byte)(x & 0xFF);
                x >>= 8;
                k = (k + 1) % 4;
            }
        }

        public override double NextDouble()
        //https://en.wikipedia.org/wiki/IEEE_754-1985#IEC_60559
        {
            byte[] bytes = new byte[8];
            NextBytes(bytes);
            // sign = 0 and biases exponent = 1023
            bytes[7] = 63;
            bytes[6] = (byte)(240 + (bytes[6] & 15));
            if (!BitConverter.IsLittleEndian)
                bytes = (byte[])bytes.Reverse();
            return BitConverter.ToDouble(bytes, 0) - 1d;
        }

        protected override double Sample()
        {
            return NextDouble();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Mersenne

        // Generate the next n values from the series x_i 
        private void Twist()
        {
            const ulong one = 1U;
            const ulong lower_mask = (one << r) - one;
            const ulong upper_mask = ~lower_mask;
            for (int i = 0; i < n; i++)
            {
                ulong x = (MT[i] & upper_mask) + (MT[(i + 1) % n] & lower_mask);
                ulong y = x >> 1;
                if ((x & one) == one) y ^= a;
                MT[i] = MT[(i + m) % n] ^ y;
            }
            index = 0;
        }

        public uint NextUInt()
        {
            //Mersenne Twister tempering bit shifts/masks
            const int u = 11;
            const ulong d = 0xFFFFFFFF16U;
            const int s = 7;
            const ulong b = 0x9D2C568016U;
            const int t = 15;
            const ulong c = 0xEFC6000016U;
            const int l = 18;

            if (index == n) Twist();
            ulong y = MT[index];
            y ^= ((y >> u) & d);
            y ^= ((y << s) & b);
            y ^= ((y << t) & c);
            y ^= (y >> l);
            uint sample = (uint)(y & 0xFFFFFFFF);
            OnNextSample(new MersenneArgs(index, sample));
            index++;
            return sample;
        }

        #endregion
    }
}
