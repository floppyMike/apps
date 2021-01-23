using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace Gauss
{
    public class UnitTest1
    {
        [Fact]
        public void Row_whitespaces()
        {
            Matrix x = new Matrix(3, 4);
            Assert.Equal(4, Matrix.start_zeros(x, 0));

            x[2, 1] = 1;
            x[3, 1] = 1;
            Assert.Equal(2, Matrix.start_zeros(x, 1));

            x[0, 2] = 1;
            Assert.Equal(0, Matrix.start_zeros(x, 2));
        }

        [Fact]
        public void Matrix_order()
        {
            Matrix x = new Matrix(3, 4);
            x[0, 2] = 2; x[1, 2] = 3; x[2, 2] = 4;
            x[0, 1] = 0; x[1, 1] = 1; x[2, 1] = 5;
            x[0, 0] = 0; x[1, 0] = 0; x[2, 0] = 7;
            var y = Matrix.reorder(x);
            Assert.Equal(new double[,]{ { 0, 0, 7, 0 }, { 0, 1, 5, 0 }, { 2, 3, 4, 0 } }, y.data);
        }

        [Fact]
        public void Matrix_push_column()
        {
            Matrix x = new Matrix(2, 3);
            x[0, 0] = 1;
            x[1, 1] = 1;
            x.push();
            Assert.Equal(new double[,]{ { 1, 0, 0, 0 }, { 0, 1, 0, 0 } }, x.data);
        }

        [Fact]
        public void Matrix_solve()
        {
            Matrix x = new Matrix (3, 4);
            x[0, 0] = 1; x[1, 0] = 2;  x[2, 0] = 3;  x[3, 0] = 4; 
            x[0, 1] = 5; x[1, 1] = 6;  x[2, 1] = 7;  x[3, 1] = 8;
            x[0, 2] = 9; x[1, 2] = 10; x[2, 2] = 11; x[3, 2] = 12;
            Matrix y = Matrix.reorder(x);
            Matrix.solve(y);
        }

        [Fact]
        public void Matrix_vars()
        {
            Matrix mat = new Matrix (3, 4);
            mat[0, 0] = 3; mat[1, 0] = 6; mat[2, 0] = -2;  mat[3, 0] = -15; 
            mat[0, 1] = 3; mat[1, 1] = 2; mat[2, 1] = 1;  mat[3, 1] = 2;
            mat[0, 2] = 2; mat[1, 2] = 5; mat[2, 2] = -5; mat[3, 2] = -23;
            Matrix.solve(mat);
            Assert.Equal(new double[] { 1, -2, 3 }, Matrix.vars(mat));
        }
    }
}
