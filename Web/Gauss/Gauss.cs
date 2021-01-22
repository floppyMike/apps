using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

public class Row
{
    public Row(int s) => m_data = new List<float>(new float[s]);

    public Row(List<float> l) => m_data = l;

    public float this[int i]
    {
        get => m_data[i];
        set => m_data[i] = value;
    }
    
    public Row subtract(Row b)
    {
        for (int i = 0; i < b.size; ++i)
            m_data[i] -= b[i];

        return this;
    }

    public Row multiply(float v)
    {
        for (int i = 0; i < m_data.Count; ++i)
            m_data[i] *= v;

        return this;
    }

    public static Row operator*(Row a, float v)
    {
        Row z = new Row(a.size);

        for (int i = 0; i < z.size; ++i)
            z[i] = (int)((float)a[i] * v);

        return z;
    }

    public void copy(Row b)
    {
        for (int i = 0; i < m_data.Count; ++i) m_data[i] = b[i];
    }

    public List<float> parameters { get => m_data.GetRange(0, m_data.Count - 1); set => m_data = value; }
    public float result { get => m_data[m_data.Count - 1]; set => m_data[m_data.Count - 1] = value; }

    public List<float> data { get => m_data; }

    public int size { get => m_data.Count; }


    private List<float> m_data;
}





public class Matrix
{
    public Matrix(int rows, int columns)
    {
        m_data = new float[rows, columns];
    }

    public float this[int x, int y]
    {
        get => m_data[y, x];
        set => m_data[y, x] = value;
    }

    public void push()
    {
        float[,] n = new float[rows + 1, columns + 1];

        for (int y = 0; y < rows - 1; ++y)
            for (int x = 0; x < columns - 1; ++x)
                n[y, x] = m_data[y, x];
        
        for (int y = 0; y < rows - 1; ++y)
            n[y, columns] = m_data[y, columns - 1];

        m_data = n;
    }

    public void pop()
    {
        float [,] n = new float[rows - 1, columns - 1];

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

    public int rows { get => m_data.GetLength(0); }
    public int columns { get => m_data.GetLength(1); }
    public float[,] data { get => m_data; }

    private float[,] m_data;
}