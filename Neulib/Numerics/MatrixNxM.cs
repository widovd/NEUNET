using Neulib.Exceptions;

namespace Neulib.Numerics
{
    public class MatrixNxM
    {
        private readonly double[,] _values;

        public double this[int i, int j]
        {
            get { return _values[i, j]; }
            set { _values[i, j] = value; }
        }

        public int Count1
        {
            get { return _values.GetLength(0); }
        }

        public int Count2
        {
            get { return _values.GetLength(1); }
        }

        public MatrixNxM(int n, int m)
        {
            _values = new double[n, m];
        }

        public MatrixNxM MulMatrix(MatrixNxM Mat)
        {
            int n = Count1;
            int m = Mat.Count2;
            int h = Count2; 
            if (h != Mat.Count1) // Matrix sizes do not match
                throw new UnequalValueException(h, Mat.Count1, 921084);
            MatrixNxM Out = new MatrixNxM(n, m);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double Sum = 0;
                    for (int k = 0; k < h; k++)
                    {
                        Sum += _values[i, k] * Mat._values[k, j];
                    }
                    Out._values[i, j] = Sum;
                }
            }
            return Out;
        }

        public MatrixNxN ToMatrixNxN()
        {
            int n = Count1;
            int m = Count2;
            if (n != m) throw // Matrix is not square
                    new UnequalValueException(n, m, 125543);
            MatrixNxN MatNxN = new MatrixNxN(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    MatNxN._values[i, j] = _values[i, j];
                }
            }
            return MatNxN;
        }
    }

}
