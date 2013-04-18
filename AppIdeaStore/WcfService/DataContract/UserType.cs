using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WcfService.DataContract
{
    [DataContract]
    public class UsrType
    {
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public int userId { get; set; }
    }
}