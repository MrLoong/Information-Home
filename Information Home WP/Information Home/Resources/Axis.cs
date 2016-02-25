using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_Home.Resources
{
    class Axis
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public AccelerationDirection AccDirection { get; set; }
    }
}
