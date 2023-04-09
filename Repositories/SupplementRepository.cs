using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private readonly List<ISupplement> models;


        public SupplementRepository()
        {
            models = new List<ISupplement>();
        }


        public IReadOnlyCollection<ISupplement> Models()
            => models.AsReadOnly();


        public void AddNew(ISupplement supplement)
        {
            models.Add(supplement);
        }

        public bool RemoveByName(string typeName)
        {
            ISupplement supplementToRemove =  models.FirstOrDefault(m => m.GetType().Name == typeName);
            return models.Remove(supplementToRemove);
        }

        public ISupplement FindByStandard(int interfaceStandard)
            => models.FirstOrDefault(m => m.InterfaceStandard == interfaceStandard);

       

        
    }
}
