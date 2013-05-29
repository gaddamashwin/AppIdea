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

        [OperationContract]
        void SaveJudgement(CarJudgementType carJudgement);
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

    [DataContract]
    public class CarJudgementType
    {
        [DataMember]
        public int CarId { get; set; }
        [DataMember]
        public int OverAllExterior { get; set; }
        [DataMember]
        public int FitNFinishExterior { get; set; }
        [DataMember]
        public int UniquenessOfProductExterior { get; set; }
        [DataMember]
        public int OneOfCustomizationsExterior { get; set; }
        [DataMember]
        public int PaintCleanlinessExterior { get; set; }
        [DataMember]
        public int PaintApperanceExterior { get; set; }
        [DataMember]
        public int PaintCompletenessExterior { get; set; }
        [DataMember]
        public int PaintOriginalityExterior { get; set; }
        [DataMember]
        public int CleanlinessEngine { get; set; }
        [DataMember]
        public int ModificationsEngine { get; set; }
        [DataMember]
        public int AccessoriesEngine { get; set; }
        [DataMember]
        public int UniquenessOfProductsEngine { get; set; }
        [DataMember]
        public int OneOfCustomizationsEngine { get; set; }
        [DataMember]
        public int CleanlinessUCDetails { get; set; }
        [DataMember]
        public int PaintOrPowercoatUCDetails { get; set; }
        [DataMember]
        public int BreaksUCPerformance { get; set; }
        [DataMember]
        public int SuspensionUCPerformance { get; set; }
        [DataMember]
        public int CleanlinessWheels { get; set; }
        [DataMember]
        public int AppearanceWheels { get; set; }
        [DataMember]
        public int UniquenessOfProductsWheels { get; set; }
        [DataMember]
        public int OverallTiers { get; set; }
        [DataMember]
        public int CleanlinessInterior { get; set; }
        [DataMember]
        public int AccessoriesInterior { get; set; }
        [DataMember]
        public int UpholsteryInterior { get; set; }
        [DataMember]
        public int UniquenessOfProductsInterior { get; set; }
        [DataMember]
        public int SoundICEDetails { get; set; }
        [DataMember]
        public int ICEICEDetails { get; set; }
        [DataMember]
        public int UniquenessOfProductsICEDetails { get; set; }
        [DataMember]
        public int QualityOfInstallICEDetails { get; set; }
        [DataMember]
        public int UniquenessOfInstallation { get; set; }
        [DataMember]
        public int OverallAppearanceODetails { get; set; }
        [DataMember]
        public int DisplayODetails { get; set; }
        [DataMember]
        public string JudgedByJudgedDetails { get; set; }
        [DataMember]
        public DateTime JudgedDateJudgedDetails { get; set; }
        [DataMember]
        public int CarJudgementID { get; set; }
    }
}
