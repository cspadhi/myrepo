using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;

namespace DatatableCRUD.API
{
    [ServiceContract]
    public interface IFeeds
    {
        [WebGet(UriTemplate="/latest/rss")]
        [OperationContract]
        Rss20FeedFormatter GetEmployees();
    }
}
