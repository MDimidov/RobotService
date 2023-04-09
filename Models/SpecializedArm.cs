using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class SpecializedArm : Supplement
    {
        private const int DefaultInterfaceStandard = 10045;
        private const int DefaultBatteryUsage = 10000;
        public SpecializedArm() 
            : base(DefaultInterfaceStandard, DefaultBatteryUsage)
        {
        }
    }
}
