using Microsoft.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_Home.Resources
{
    class WPAccHelper
    {
        private Accelerometer accelerometer = new Accelerometer();
        private AxisCollection axisCollection = new AxisCollection();
        public ActionEnum SupportedAction
        {
            get { return axisCollection.SupportedAction; }
            set { axisCollection.SupportedAction = value; }
        }
        public WPAccHelper()
        {
            accelerometer.Start();
            accelerometer.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged);
        }

        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            var newAxis = new Axis() { X = e.SensorReading.Acceleration.X, Y = e.SensorReading.Acceleration.Y, Z = e.SensorReading.Acceleration.Z };
            axisCollection.Add(newAxis);
            if (!axisCollection.IsMoving())
            {
                axisCollection.Reset();
                return;
            }
            axisCollection.CheckCurrentAction();
        }

        public string GetInfo()
        {
            return axisCollection.CurrentActionEnum.ToString();
        }
    }
}
