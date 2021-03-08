using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c2048
{
    static class Tests
    {
        public static int[,] Test2048()
        {
            int[,] table = new int[4, 4];
            table[0, 0] = 1024;
            table[1, 0] = 1024;
            return table;
        }

        public static int[,] TestP9()
        {
            int[,] table = new int[4, 4];
            table[0, 0] = 16;
            table[3, 2] = 8;
            table[0, 3] = 2;
            table[1, 3] = 2;
            table[2, 3] = 4;
            table[3, 3] = 4;
            return table;
        }
    }
}
