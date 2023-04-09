using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class IndustrialAssistant : Robot
    {
        private const int DefaultBatteryCapacity = 40000;
        private const int DefaultConvertionCapacityIndex = 5000;

        public IndustrialAssistant(string model)
            : base(model, DefaultBatteryCapacity, DefaultConvertionCapacityIndex)
        {
        }
    }
}
