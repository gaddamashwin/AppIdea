using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace JudgeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        IEnumerable<CarType> GetCars();

        [OperationContract]
        IEnumerable<CarModelType> GetCarModels();
    }

    [DataContract]
    public class CarModelType
    {
        [DataMember]
        public int CarModelID { get; set; }
        [DataMember]
        public int CarID { get; set; }
        [DataMember]
        public string CarModelName { get; set; }
    }

    [DataContract]
    public class CarType
    {
        [DataMember]
        public int CarID { get; set; }
        [DataMember]
        public string CarName { get; set; }
    }
}
