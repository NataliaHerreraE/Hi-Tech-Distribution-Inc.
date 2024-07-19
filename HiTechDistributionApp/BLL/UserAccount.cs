using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.DAL;

namespace HiTechDistributionApp.BLL
{
    public class UserAccount
    {
        private int userId;
        private string password;
        private DateTime dateCreated;
        private DateTime dateModified;
        private int statusId;
        private string statusDescription;

        public int UserId 
        { 
            get { return userId; } 
            set { userId = value; }
        }

        public string Password 
        {
            get { return password; } 
            set {  password = value; }
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }

        public DateTime DateModified
        {
            get { return dateModified; }
            set { dateModified = value; }
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


        public UserAccount()
        {
            userId = 0;
            password = string.Empty;
            dateCreated = DateTime.Now;
            dateModified = DateTime.Now;
            statusId = 0;
            statusDescription = string.Empty;

        }

        public UserAccount(int userIdForm, string passwordForm, int statusIdForm)
        {
            this.userId = userIdForm;
            this.password = passwordForm;
            this.dateCreated = DateTime.Now.Date;
            this.dateModified = DateTime.Now.Date;
            this.statusId = statusIdForm;
        }

        public UserAccount(int userIdForm, string passwordForm)
        {
            this.userId = userIdForm;
            this.password = passwordForm;
            this.dateCreated = DateTime.Now.Date;
            this.dateModified = DateTime.Now.Date;
        }

        public UserAccount(int userId)
        {
            this.userId = userId;
        }

        public void SaveUserAccount(UserAccount user)
        {
            UserAccountsDB.SaveRecord(user);
        }

        public List<UserAccount> GetAllUserAccount()
        {
            return UserAccountsDB.GetAllRecords();
        }

        public UserAccount SearchUserAccount(int userId)
        {
            return UserAccountsDB.SearchRecord(userId);
        }

        public void UpdateUserAccount(UserAccount user)
        {
            UserAccountsDB.UpdateRecord(user);
        }

        public void UpdateUserAccountByUser(UserAccount user)
        {
            UserAccountsDB.UpdatePasswordByUser(user);
        }

        

        public void DeleteUserAccount(UserAccount user) 
        { 
            UserAccountsDB.DeleteRecord(user);
        }

        public bool LoginUserAccount(int userId, string password)
        {
            return UserAccountsDB.LoginUser(userId, password);
        }

        public String GetUserNameUser(int userId)
        {
            return UserAccountsDB.GetNameUser(userId);
        }

    }
}
