using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WcfService.DataContract
{
    [DataContract]
    public class appType
    {
        [DataMember]
        public byte[] Pict { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]public string desc { get; set; }
        [DataMember]public string impact { get; set; }
        [DataMember]public string issue { get; set; }
        [DataMember]public string idea { get; set; }
        [DataMember]public Nullable<System.DateTime> submittedDate { get; set; }
        [DataMember]public int appId { get; set; }
        [DataMember]public Nullable<int> submittedBy { get; set; }
        [DataMember]public Nullable<bool> deleted { get; set; }

        //[DataMember]
        //public virtual User User { get; set; }
        //[DataMember]
        //public virtual ICollection<Comment> Comments { get; set; }
        //[DataMember]
        //public virtual ICollection<Image> Images { get; set; }
        //[DataMember]
        //public virtual ICollection<SectorOffering> SectorOfferings { get; set; }
        //[DataMember]
        //public virtual ICollection<Technology> Technologies { get; set; }
        //[DataMember]
        //public virtual ICollection<UserType> UserTypes { get; set; }
    }
}