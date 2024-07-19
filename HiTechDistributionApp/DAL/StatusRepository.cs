using HiTechDistributionApp.BLL.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.DAL
{
    public class StatusRepository
    {
        private readonly HiTechDistributionDBContext dBContext;

        public StatusRepository()
        {
            dBContext = new HiTechDistributionDBContext();
        }

        public IEnumerable<Status> GetStatuses() => dBContext.Status.ToList();

        public Status GetStatusById(int id) => dBContext.Status.Find(id);


    }
}
