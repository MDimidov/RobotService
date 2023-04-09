using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class LaserRadar : Supplement
    {
        private const int DefaultInterfaceStandard = 20082;
        private const int DefaultBatteryUsage = 5000;
        public LaserRadar() 
            : base(DefaultInterfaceStandard, DefaultBatteryUsage)
        {
        }
    }
}
