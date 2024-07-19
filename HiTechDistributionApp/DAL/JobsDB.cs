using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.BLL;

namespace HiTechDistributionApp.DAL
{
    public static class JobsDB
    {
        public static List<Jobs> GetAllJobs()
        {
            List<Jobs> listJob = new List<Jobs>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectAll = new SqlCommand("select JobId, JobTitle from Jobs ", connDB);
            SqlDataReader sqlReader = cmdSelectAll.ExecuteReader();
            Jobs job;

            while (sqlReader.Read())
            {
                job = new Jobs();
                job.JobId = Convert.ToInt32(sqlReader["JobId"]);
                job.JobTitle = sqlReader["JobTitle"].ToString();
                listJob.Add(job);
            }

            connDB.Close();
            return listJob;

        }
    }
}
