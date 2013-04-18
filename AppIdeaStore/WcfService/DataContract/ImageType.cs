using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WcfService.DataContract
{
    [DataContract]
    public class ImageType
    {
        [DataMember]
        public byte[] Pict { get; set; }
        [DataMember]
        public int Sequence { get; set; }
        [DataMember]
        public int Type { get; set; }
        [DataMember]
        public int ImageId { get; set; }
        [DataMember]
        public int AppId { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}