namespace Neulib.Numerics
{
    public class MatrixNxN : MatrixNxM
    {

        public MatrixNxN(int n) : base(n, n) { }

        public MatrixNxN MulMatrix(MatrixNxN Mat)
        {
            return base.MulMatrix(Mat).ToMatrixNxN();
        }

        public MatrixNxN Inverse()
        {
            int n = Count1;
            LudCmp Lud = new LudCmp(this);
            MatrixNxN result = new MatrixNxN(n);
            for (int j = 0; j < n; j++)
            {
                double[] vec = new double[n];
                for (int i = 0; i < n; i++) vec[i] = 0d;
                vec[j] = 1d;
                vec = Lud.LubKsb(vec);
                for (int i = 0; i < n; i++) result[i, j] = vec[i];
            }
            return result;
        }

    }
}
