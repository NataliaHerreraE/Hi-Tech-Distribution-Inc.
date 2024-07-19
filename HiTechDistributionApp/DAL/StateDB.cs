using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.BLL;

namespace HiTechDistributionApp.DAL
{
    public static class StateDB
    {
        public static List<State> GetAllStatus()
        {
            List<State> listState = new List<State>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectAll = new SqlCommand("select StatusId, State from Status ", connDB);
            SqlDataReader sqlReader = cmdSelectAll.ExecuteReader();
            State state;

            while (sqlReader.Read())
            {
                state = new State();
                state.StatusId = Convert.ToInt32(sqlReader["StatusId"]);
                state.StatusDescription = sqlReader["State"].ToString();
                listState.Add(state);
            }
            connDB.Close();
            return listState;
        }

        public static List<State> GetAllStatusEmployee()
        {
            List<State> listState = new List<State>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectAll = new SqlCommand("select StatusId, State from Status Where StatusId in (6,7) ", connDB);
            SqlDataReader sqlReader = cmdSelectAll.ExecuteReader();
            State state;

            while (sqlReader.Read())
            {
                state = new State();
                state.StatusId = Convert.ToInt32(sqlReader["StatusId"]);
                state.StatusDescription = sqlReader["State"].ToString();
                listState.Add(state);
            }
            connDB.Close();
            return listState;
        }


        public static List<State> GetAllStatusUser()
        {
            List<State> listState = new List<State>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectAll = new SqlCommand("select StatusId, State from Status Where StatusId in (1,2) ", connDB);
            SqlDataReader sqlReader = cmdSelectAll.ExecuteReader();
            State state;

            while (sqlReader.Read())
            {
                state = new State();
                state.StatusId = Convert.ToInt32(sqlReader["StatusId"]);
                state.StatusDescription = sqlReader["State"].ToString();
                listState.Add(state);
            }
            connDB.Close();
            return listState;
        }

    }
}
