using System;
using System.Diagnostics;
using System.Collections.Generic;

public class Row
{
    public Row(int s) => m_data = new List<int>(new int[s]);

    public Row(List<int> l) => m_data = l;

    public int this[int i]
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
            m_data[i] *= (int)v;

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

    public List<int> data { get => m_data; }

    public int size { get => m_data.Count; }

    public static int start_zeros(Row r)
    {
        int x = 0;
        for (int i = 0; i < r.size; ++i)
            if (r[i] == 0)
                ++x;
            else
                break;

        return x;
    }

    private List<int> m_data;
}

public class Matrix
{
    public Matrix(int rows, int columns)
    {
        m_rows = rows;
        m_columns = columns;

        m_data = new List<int>(new int[rows * columns + rows]);
    }

    public int get(int x, int y) => m_data[x + y * m_rows];
    public void set(int x, int y, int v) => m_data[x + y * m_rows] = v;

    public Row row(int r) => new Row(m_data.GetRange(r, m_columns + 1));
    public void row(int r, Row l)
    {
        for (int i = 0; i < m_columns; ++i)
        {
            int idx = r * m_columns + i;
            m_data[idx] = l[idx];
        }
    }
    //public int result(int r) => m_equ[r];

    public static int[] order_list(Matrix x)
    {
        int[] y = new int[x.rows];
        for (int i = 0; i < x.rows; ++i)
            y[i] = Row.start_zeros(x.row(i));
        
        return y;
    }

    public static Matrix reorder(Matrix x)
    {
        Matrix y = new Matrix(x.rows, x.columns);

        int[] order = order_list(x);

        for (int top_size = 0; top_size < x.columns; ++top_size)
            for (int i = 0; i < x.rows; ++i)
                if (order[i] == top_size)
                    y.row(top_size, x.row(i));

        return y;
    }

    public int rows { get => m_rows; }
    public int columns { get => m_columns; }

    private int m_rows, m_columns;
    private List<int> m_data;
    //private int[] m_equ;
}