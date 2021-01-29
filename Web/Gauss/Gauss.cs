using System;

public class Matrix
{
    public Matrix(int rows, int columns)
    {
        m_data = new double[rows, columns];
    }

    public double this[int x, int y]
    {
        get => m_data[y, x];
        set => m_data[y, x] = value;
    }

    public void push()
    {
        double[,] n = new double[rows + 1, columns + 1];

        for (int y = 0; y < rows - 1; ++y)
            for (int x = 0; x < columns - 1; ++x)
                n[y, x] = m_data[y, x];
        
        for (int y = 0; y < rows - 1; ++y)
            n[y, columns] = m_data[y, columns - 1];

        m_data = n;
    }

    public void pop()
    {
        double [,] n = new double[rows - 1, columns - 1];

        for (int y = 0; y < rows - 1; ++y)
            for (int x = 0; x < columns - 1; ++x)
                n[y, x] = m_data[y, x];
        
        for (int y = 0; y < rows - 1; ++y)
            n[y, columns - 2] = m_data[y, columns - 1];

        m_data = n;
    }

    public void copy(Matrix m, int r)
    {
        for (int i = 0; i < columns; ++i) m_data[r, i] = m[i, r];
    }

    public Matrix multiply(double v, int sr)
    {
        for (int i = 0; i < columns; ++i) m_data[sr, i] *= v;
        return this;
    }

    public Matrix subtract(Matrix m, int sr, int dr)
    {
        for (int i = 0; i < columns; ++i) m_data[sr, i] -= m[i, dr];
        return this;
    }

    public static int start_zeros(Matrix m, int r)
    {
        int x = 0;
        for (int i = 0; i < m.columns; ++i)
            if (m[i, r] == 0)
                ++x;
            else
                break;

        return x;
    }

    public static int[] order_list(Matrix x)
    {
        int[] y = new int[x.rows];
        for (int i = 0; i < x.rows; ++i)
            y[i] = Matrix.start_zeros(x, i);
        
        return y;
    }

    public static Matrix reorder(Matrix x)
    {
        Matrix y = new Matrix(x.rows, x.columns);

        int[] order = order_list(x);

        for (int top_size = 0; top_size < x.columns; ++top_size)
            for (int i = 0; i < x.rows; ++i)
                if (order[i] == top_size)
                    y.copy(x, i);

        return y;
    }

    public static Matrix solve(Matrix m)
    {
        for (int root = 0; root < m.rows - 1; ++root)
        {
            if (m[root, root] != 0)
            {
                for (int i = root + 1; i < m.rows; ++i)
                {
                    if (m[root, i] != 0)
                    {
                        double div = m[root, root] / m[root, i];
                        m.multiply(div, i).subtract(m, i, root);
                    }
                }
            }
        }

        return m;
    }

    public double sum(int r, int beg, int end, double[] arr)
    {
        double s = 0;
        for (; beg < end; ++beg)
            s += m_data[r, beg] * arr[beg];
        return s;
    }

    public static double[] vars(Matrix m)
    {
        double[] res = new double[m.columns - 1];

        for (int i = m.rows - 1; i >= 0; --i)
            if (m[i, i] != 0)
                res[i] = (m[m.columns - 1, i] - m.sum(i, i, m.columns - 1, res)) / m[i, i];
            else
                break;
        
        return res;
    }

    public int rows { get => m_data.GetLength(0); }
    public int columns { get => m_data.GetLength(1); }
    public double[,] data { get => m_data; }

    private double[,] m_data;
}