using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PHashCS
{
    public class HashEventArgs : EventArgs
    {
        public int retCode;
        public ulong hash;

        public HashEventArgs(int ret, ulong hash)
        {
            this.retCode = ret;
            this.hash = hash;
        }
    }

    public class DistanceEventArgs : EventArgs
    {
        public int distance;

        public DistanceEventArgs(int dist)
        {
            this.distance = dist;
        }
    }

    public class CompareEventArgs : EventArgs
    {
        public int distance;

        public CompareEventArgs(int dist)
        {
            this.distance = dist;
        }
    }
}
