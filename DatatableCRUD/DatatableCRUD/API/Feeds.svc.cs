using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Text;
using System.Data.Entity;
using DatatableCRUD.Models;

namespace DatatableCRUD.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Feeds" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Feeds.svc or Feeds.svc.cs at the Solution Explorer and start debugging.
    public class Feeds : IFeeds
    {
        public Rss20FeedFormatter GetEmployees()
        {
            DatatableCRUDEntities db = new DatatableCRUDEntities();
            var employees =
                from e in db.Employees
                orderby e.FirstName descending
                select new SyndicationItem(
                    e.FirstName + e.LastName, 
                    "The employee's email address is " + e.EmailId, 
                    new Uri("http://google.com"));

            SyndicationFeed feed = new SyndicationFeed(employees);
            feed.Title = new TextSyndicationContent("Employees of google.com");

            return new Rss20FeedFormatter(feed);
        }
    }
}
