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
            MatrixNxN Out = new MatrixNxN(n);
            for (int j = 0; j < n; j++)
            {
                Single1D Vec = new Single1D(n);
                for (int i = 0; i < n; i++) Vec[i] = 0f;
                Vec[j] = 1f;
                Vec = Lud.LubKsb(Vec);
                for (int i = 0; i < n; i++) Out[i, j] = Vec[i];
            }
            // test
            // MatrixNxN Test = MulMatrix(Out);
            return Out;
        }

    }

}
