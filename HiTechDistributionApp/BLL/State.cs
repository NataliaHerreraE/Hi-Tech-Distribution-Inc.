using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.DAL;

namespace HiTechDistributionApp.BLL
{
    public class State
    {
        private int statusId;
        private string statusDescription;

        public State()
        { 
            statusId = 0;
            statusDescription = string.Empty; 
        }

        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }

        public string StatusDescription
        {
            get { return statusDescription; }
            set { statusDescription = value; }
        }

        public List<State> GetAllState()
        {
            return StateDB.GetAllStatus();
        }

        public Dictionary<string, int> GetAllStateWithIds(string process)
        {
            if (process == "Employee")
            {
                List<State> states = StateDB.GetAllStatusEmployee();
                Dictionary<string, int> stateDictionary = new Dictionary<string, int>();
                foreach (State state in states)
                {
                    stateDictionary.Add(state.StatusDescription, state.StatusId);
                }
                return stateDictionary;
            }
            else
            {
                List<State> states = StateDB.GetAllStatusUser();
                Dictionary<string, int> stateDictionary = new Dictionary<string, int>();
                foreach (State state in states)
                {
                    stateDictionary.Add(state.StatusDescription, state.StatusId);
                }
                return stateDictionary;
            }
            
        }


    }
}
