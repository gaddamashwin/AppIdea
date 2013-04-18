using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WcfService.DataContract
{
    [DataContract]
    public class CommentType
    {
        [DataMember]
        public string comment1 { get; set; }
        [DataMember]
        public int commentId { get; set; }
        [DataMember]
        public int appId { get; set; }
        [DataMember]
        public int userId { get; set; }
        [DataMember]
        public int rating { get; set; }
        [DataMember]
        public Nullable<int> status { get; set; }
        [DataMember]
        public Nullable<System.DateTime> date { get; set; }
    }
}