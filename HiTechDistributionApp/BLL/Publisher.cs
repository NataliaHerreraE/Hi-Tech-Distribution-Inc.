using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.DAL;

namespace HiTechDistributionApp.BLL
{
    public class Publisher
    {
        private int publisherId;
        private string publisherName;

        public Publisher()
        {
            publisherId = 0;
            publisherName = string.Empty;
        }
        public Publisher(string publisherName)
        {
            this.publisherName = publisherName;
        }
        public Publisher(int publisherId, string publisherName)
        {
            this.publisherId = publisherId;
            this.publisherName = publisherName;
        }

        public int PublisherId
        {
            get { return publisherId; }
            set { publisherId = value; }
        }

        public string PublisherName
        {
            get { return publisherName; }
            set { publisherName = value; }
        }

        public List<Publisher> GetAllPublishers()
        {
            return PublishersDB.GetAllRecords();
        }
        public void SavePublisher(Publisher publisher)
        {
            PublishersDB.SaveRecord(publisher);
        }
        public void UpdatePublisher(Publisher publisher)
        {
            PublishersDB.UpdateRecord(publisher);
        }
        public void DeletePublisher(Publisher publisher)
        {
            PublishersDB.DeleteRecord(publisher);
        }
        public Publisher SearchPublisher(int publisherId)
        {
            return PublishersDB.SearchRecord(publisherId);
        }

        public Dictionary<string, int> GetAllPublishersWithIds()
        {
            List<Publisher> publishers = PublishersDB.GetAllRecords();
            Dictionary<string, int> publisherDictionary = new Dictionary<string, int>();

            foreach (Publisher publisher in publishers)
            {
                publisherDictionary.Add(publisher.PublisherName, publisher.PublisherId);
            }

            return publisherDictionary;
        }
    }
}
