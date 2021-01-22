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
            Row x = new Row(2);
            Assert.Equal(2, Row.start_zeros(x));

            x = new Row(4);
            x[2] = 1;
            x[3] = 1;
            Assert.Equal(2, Row.start_zeros(x));

            x = new Row(3);
            x[0] = 1;
            Assert.Equal(0, Row.start_zeros(x));
        }

        [Fact]
        public void Matrix_order()
        {
            Matrix x = new Matrix(2, 2);
            x.set(0, 0, 1);
            Assert.Equal(new float[]{ 1, 0, 0, 0, 0, 0 }, x.data);

            x = new Matrix(3, 3);
            x.row(0, )
        }
    }
}
