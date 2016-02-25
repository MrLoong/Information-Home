using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_Home.Resources
{
    public enum ActionEnum
    {
        None = 0,
        LeftShake = 1,
        RightShake = 2,
        UpShake = 3,
        DownShake = 4
    }

    public enum AccelerationDirection
    {
        Down,
        Up,
        Left,
        Right,
        None
    }
}
