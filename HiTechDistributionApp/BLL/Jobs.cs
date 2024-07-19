using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.DAL;

namespace HiTechDistributionApp.BLL
{
    public class Jobs
    {
        private int jobId;
        private string jobTitle;

        public Jobs()
        {
            jobId = 0;
            jobTitle = string.Empty; 
        }

        public int JobId
        {
            get { return jobId; }
            set { jobId = value; }
        }

        public string JobTitle
        {
            get { return jobTitle; }
            set { jobTitle = value; }
        }

        public List<Jobs> GetAllJobs()
        {
            return JobsDB.GetAllJobs();
        }

        public Dictionary<string, int> GetAllJobsWithIds()
        {
            List<Jobs> jobs = JobsDB.GetAllJobs();
            Dictionary<string, int> jobDictionary = new Dictionary<string, int>();

            foreach (Jobs job in jobs)
            {
                jobDictionary.Add(job.JobTitle, job.jobId);
            }

            return jobDictionary;
        }


    }
}
