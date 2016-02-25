using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_Home.Resources
{
    class AxisCollection
    {
        private List<Axis> AxisList { get; set; }
        public ActionEnum CurrentActionEnum { get; set; }
        public ActionEnum SupportedAction { get; set; }
        public AxisCollection()
        {
            AxisList = new List<Axis>();
            CurrentActionEnum = ActionEnum.None;
        }

        public void Add(Axis axis)
        {
            var Gravity = axis.X * axis.X + axis.Y * axis.Y + axis.Z * axis.Z;
            if (Gravity < 0.5 || Gravity > 1.5)
            {
                AxisList.Add(axis);
                if (-0.2 > axis.X || axis.X > 0.2)
                {
                    if (axis.X < -0.6) axis.AccDirection = AccelerationDirection.Right;
                    else if (axis.X > 0.6) axis.AccDirection = AccelerationDirection.Left;
                }
                else
                {
                    if ((axis.Y * axis.Y + axis.Z * axis.Z > 1.5)) axis.AccDirection = AccelerationDirection.Up;
                    else if (axis.Y * axis.Y + axis.Z * axis.Z < 0.5) axis.AccDirection = AccelerationDirection.Down;
                }
                axis.CurrentDateTime = DateTime.Now;
            }
        }
        public bool IsMoving()
        {
            if (AxisList.Count != 0)
            {
                if (DateTime.Now.Subtract(AxisList.Last().CurrentDateTime).TotalMilliseconds > 666)
                {
                    return false;
                }
            }
            return true;
        }
        public void CheckCurrentAction()
        {
            if (AxisList.Count == 0) CurrentActionEnum = ActionEnum.None;
            else
            {
                if (!(CurrentActionEnum.Equals(ActionEnum.None)))
                {
                    return;
                }
                if (AxisList.Where(al => al.AccDirection.Equals(AccelerationDirection.Left)).Count() > 2)
                    CurrentActionEnum = ActionEnum.LeftShake;
                else if (AxisList.Where(al => al.AccDirection.Equals(AccelerationDirection.Up)).Count() > 2)
                    CurrentActionEnum = ActionEnum.UpShake;
                else if (AxisList.Where(al => al.AccDirection.Equals(AccelerationDirection.Down)).Count() > 2)
                    CurrentActionEnum = ActionEnum.DownShake;
                else if (AxisList.Where(al => al.AccDirection.Equals(AccelerationDirection.Right)).Count() > 2)
                    CurrentActionEnum = ActionEnum.RightShake;
            }
        }

        public bool Reset()
        {
            try
            {
                AxisList.Clear();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
