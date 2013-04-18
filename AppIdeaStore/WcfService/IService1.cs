using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService.DataContract;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        IEnumerable<TechnologyType> GetTechnology();

        [OperationContract]
        IEnumerable<CommentType> GetComment();

        [OperationContract]
        IEnumerable<ImageType> GetImage();

        [OperationContract]
        IEnumerable<SectorType> GetSector();

        [OperationContract]
        IEnumerable<UsrType> GetUser();

        [OperationContract]
        IEnumerable<appType> GetApp();

        [OperationContract]
        IEnumerable<appType> GetAppDetails(int id, string type);

        [OperationContract]
        IEnumerable<DcApp> GetAppIdea();

        [OperationContract]
        IEnumerable<DcSector> GetAppSector();

        [OperationContract]
        IEnumerable<DcAppDetail> GetAppDetail();

        [OperationContract]
        IEnumerable<DcTech> GetTech();

        [OperationContract]
        IEnumerable<DcAppDetails2> GetAppDetails2();
    }
}
