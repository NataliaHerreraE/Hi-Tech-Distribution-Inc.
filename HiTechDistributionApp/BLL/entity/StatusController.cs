using HiTechDistributionApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.BLL.entity
{
    public class StatusController
    {
        private readonly StatusRepository statusRepository;

        public StatusController()
        {
            statusRepository = new StatusRepository();
        }

        public IEnumerable<Status> GetStatuses() => statusRepository.GetStatuses();

        public Dictionary<string, int> GetStatusDictionary()
        {
            var statuses = this.GetStatuses();
            statuses = statuses.Where(status => status.StatusId == 3 || status.StatusId == 4 || status.StatusId == 5 || status.StatusId == 10 || status.StatusId == 9).ToList();
            if (statuses == null || !statuses.Any())
            {
                // Log the error or handle the null case as appropriate for your application
                return new Dictionary<string, int>();
            }
            return statuses.ToDictionary(status => status.State, status => status.StatusId);
        }


        public Status GetStatusById(int id) => statusRepository.GetStatusById(id);
    }
}
