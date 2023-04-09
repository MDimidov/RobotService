using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class DomesticAssistant : Robot
    {
        private const int DefaultBatteryCapacity = 20000;
        private const int DefaultConvertionCapacityIndex = 2000;

        public DomesticAssistant(string model)
            : base(model, DefaultBatteryCapacity, DefaultConvertionCapacityIndex)
        {
        }
    }
}
