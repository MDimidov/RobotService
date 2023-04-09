using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private int convertionCapacityIndex;
        private readonly List<int> interfaceStandards;

        protected Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            ConvertionCapacityIndex = conversionCapacityIndex;
            BatteryLevel = BatteryCapacity;
            interfaceStandards = new List<int>();
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNullOrWhitespace);
                }

                model = value;
            }
        }

        public int BatteryCapacity
        {
            get => batteryCapacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
                }

                batteryCapacity = value;
            }
        }


        public int ConvertionCapacityIndex
        {
            get => convertionCapacityIndex;
            private set
            {
                convertionCapacityIndex = value;
            }
        }


        public int BatteryLevel
        {
            get => batteryLevel;
            private set
            {
                batteryLevel = value;
            }
        }



        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards;

        public void Eating(int minutes)
        {
            int producedEnergy = ConvertionCapacityIndex * minutes;

            if (producedEnergy + BatteryLevel >= BatteryCapacity)
            {
                BatteryLevel = BatteryCapacity;
            }
            else
            {
                BatteryLevel += producedEnergy;
            }
        }


        public void InstallSupplement(ISupplement supplement)
        {
            interfaceStandards.Add(supplement.InterfaceStandard);
            BatteryCapacity -= supplement.BatteryUsage;
            BatteryLevel -= supplement.BatteryUsage;
        }


        public bool ExecuteService(int consumedEnergy)
        {
            if (BatteryLevel >= consumedEnergy)
            {
                BatteryLevel -= consumedEnergy;
                return true;
            }

            return false;
        }


        public override string ToString() // check this if conition and AppendLibne
        {
            StringBuilder sb = new();

            sb.AppendLine($"{this.GetType().Name} {Model}:");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
            sb.Append($"--Supplements installed: ");
            if (interfaceStandards.Any())
            {
                sb.AppendLine(string.Join(" ", interfaceStandards));

            }
            else
            {
                sb.AppendLine($"none");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
