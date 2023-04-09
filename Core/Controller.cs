using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private string[] valitTypeRobotNames = new string[]
        {
            "DomesticAssistant",
            "IndustrialAssistant"
        };

        private string[] valitSupplementsType = new string[]
        {
            "SpecializedArm",
            "LaserRadar"
        };

        private readonly SupplementRepository supplements;
        private readonly RobotRepository robots;


        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }


        public string CreateRobot(string model, string typeName)
        {
            if (!valitTypeRobotNames.Contains(typeName))
            {
                return String.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }

            IRobot robot = null;
            if (typeName == "IndustrialAssistant")
            {
                robot = new IndustrialAssistant(model);
            }
            else if (typeName == "DomesticAssistant")
            {
                robot = new DomesticAssistant(model);
            }

            robots.AddNew(robot);
            return String.Format(OutputMessages.RobotCreatedSuccessfully, robot.GetType().Name, robot.Model);
        }

        public string CreateSupplement(string typeName)
        {
            if (!valitSupplementsType.Contains(typeName))
            {
                return String.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }

            ISupplement supplement = null;
            if (typeName == "LaserRadar")
            {
                supplement = new LaserRadar();
            }
            else if (typeName == "SpecializedArm")
            {
                supplement = new SpecializedArm();
            }

            supplements.AddNew(supplement);
            return string.Format(OutputMessages.SupplementCreatedSuccessfully, supplement.GetType().Name);
        }

        public string UpgradeRobot(string model, string supplementTypeName) // here may be robot model in Linq will be checked later
        {
            ISupplement supplement = supplements.Models().FirstOrDefault(m => m.GetType().Name == supplementTypeName);

            List<IRobot> robotCollection = robots
                .Models()
                .Where(r => r.Model == model && !r.InterfaceStandards.Contains(supplement.InterfaceStandard)) // chech true or false
                                                                                                              //.OrderByDescending(r => r.BatteryLevel)
                .ToList();//

            if (!robotCollection.Any())
            {
                return string.Format(OutputMessages.AllModelsUpgraded, model);
            }

            IRobot robot = robotCollection.FirstOrDefault();
            robot.InstallSupplement(supplement);
            supplements.RemoveByName(supplementTypeName);
            return string.Format(OutputMessages.UpgradeSuccessful, robot.Model, supplement.GetType().Name);
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            //ISupplement supplement = supplements.Models().FirstOrDefault(m => m.GetType().Name == supplementTypeName);

            IEnumerable<IRobot> robotCollection = robots
                .Models()
                .Where(r => r.InterfaceStandards.Contains(intefaceStandard) == true)
                .OrderByDescending(r => r.BatteryLevel);

            if (!robotCollection.Any())
            {
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }

            int sumBatteryLevel = robotCollection
                .Sum(r => r.BatteryLevel);

            if (sumBatteryLevel < totalPowerNeeded)
            {
                return string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - sumBatteryLevel);
            }

            int totalPowerNeededCopy = totalPowerNeeded;
            int totalRobotUsed = 0;

            foreach (IRobot robot in robotCollection)
            {
                if (robot.BatteryLevel >= totalPowerNeededCopy)
                {
                    robot.ExecuteService(totalPowerNeededCopy);
                    totalRobotUsed++;
                    totalPowerNeededCopy = 0;
                    break;
                }
                else
                {
                    totalPowerNeededCopy -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                    totalRobotUsed++;
                }
            }

            return string.Format(OutputMessages.PerformedSuccessfully, serviceName, totalRobotUsed);
        }

        public string RobotRecovery(string model, int minutes)
        {
            int fedRobots = 0;

            foreach (IRobot robot in robots.Models().Where(r => r.Model == model))
            {
                if (robot.BatteryLevel < robot.BatteryCapacity * 0.5)
                {
                    robot.Eating(minutes);
                    fedRobots++;
                }
            }

            return string.Format(OutputMessages.RobotsFed, fedRobots);
        }

        public string Report()
        {
            StringBuilder sb = new();

            foreach (IRobot robot in robots
                .Models()
                .OrderByDescending(r => r.BatteryLevel)
                .ThenBy(r => r.BatteryCapacity))
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().TrimEnd();
        }




    }
}
