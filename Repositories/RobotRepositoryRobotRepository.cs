using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private readonly List<IRobot> models;


        public RobotRepository()
        {
            models = new List<IRobot>();
        }


        public IReadOnlyCollection<IRobot> Models()
            => models.AsReadOnly();


        public void AddNew(IRobot robot)
        {
            models.Add(robot);
        }

        public bool RemoveByName(string robotModel)
        {
            IRobot robotToRemove =  models.FirstOrDefault(m => m.Model == robotModel);
            return models.Remove(robotToRemove);
        }

        public IRobot FindByStandard(int interfaceStandard) // check this metod
            => models.FirstOrDefault(m => m.InterfaceStandards.Any(iss => iss == interfaceStandard));

       

        
    }
}
