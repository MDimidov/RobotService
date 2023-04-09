﻿using RobotService.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Supplement : ISupplement
    {
        private int interfaceStandard;
        private int batteryUsage;

        protected Supplement(int interfaceStandard, int batteryUsage)
        {
            InterfaceStandard = interfaceStandard;
            BatteryUsage = batteryUsage;
        }

        public int InterfaceStandard
        {
            get => interfaceStandard;
            private set
            {
                interfaceStandard = value;
            }
        }

        public int BatteryUsage
        {
            get => batteryUsage;
            private set
            {
                batteryUsage = value;
            }
        }
    }
}
