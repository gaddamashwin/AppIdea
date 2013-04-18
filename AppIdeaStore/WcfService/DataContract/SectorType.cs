using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.DataContract
{
    [DataContract]
    public class SectorType
    {
        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public int SectorId
        {
            get;
            set;
        }

        [DataMember]
        public List<int> appID
        {
            get;
            set;
        }
    }
}