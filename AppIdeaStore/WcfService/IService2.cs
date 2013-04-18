using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface IService2
    {
        [OperationContract]
        IEnumerable<DcApp> GetApp();

        [OperationContract]
        IEnumerable<DcSector> GetSector();

        [OperationContract]
        IEnumerable<DcAppDetail> GetAppDetail();

        [OperationContract]
        IEnumerable<DcTech> GetTech();

        [OperationContract]
        IEnumerable<DcAppDetails2> GetAppDetails2();
    }

    [DataContract]
    public class DcApp
    {
        [DataMember]
        public int appId { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string desc { get; set; }
        [DataMember]
        public string impact { get; set; }
        [DataMember]
        public string issue { get; set; }
        [DataMember]
        public string idea { get; set; }
        [DataMember]
        public Nullable<System.DateTime> submittedDate { get; set; }
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public byte[] pict { get; set; }
    }

    [DataContract]
    public class DcSector
    {
        [DataMember]
        public int appId { get; set; }
        [DataMember]
        public int sectorId { get; set; }
        [DataMember]
        public string name { get; set; }
    }

    [DataContract]
    public class DcAppDetail
    {
        [DataMember]
        public int appId { get; set; }
        [DataMember]
        public byte[] pict { get; set; }
        [DataMember]
        public int imageId { get; set; }
    }

    [DataContract]
    public class DcTech
    {
        [DataMember]
        public int appId { get; set; }
        [DataMember]
        public int techId { get; set; }
        [DataMember]
        public string name { get; set; }
    }

    [DataContract]
    public class DcAppDetails2
    {
        [DataMember]
        public string comment { get; set; }
        [DataMember]
        public int commentId { get; set; }
        [DataMember]
        public int appId { get; set; }
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public int rating { get; set; }
        [DataMember]
        public Nullable<int> status { get; set; }
        [DataMember]
        public Nullable<System.DateTime> date { get; set; }
    }
}
