using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeGenerator
{
    public struct Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Cell(int p1, int p2)
        {
            X = p1;
            Y = p2;
        }

    }
}
