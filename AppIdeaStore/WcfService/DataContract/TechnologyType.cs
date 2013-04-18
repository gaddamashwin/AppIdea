using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.DataContract
{
    [DataContract]
    public class TechnologyType
    {
        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public int TechnologyId
        {
            get;
            set;
        }
    }
}