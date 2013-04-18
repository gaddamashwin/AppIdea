using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class Service2 : IService2
    {
        public IEnumerable<DcApp> GetApp()
        {
            ISTOREEntities3 ent = new ISTOREEntities3();
            return ent.ViewApps.ToList<ViewApp>().ConvertAll<DcApp>(i => WCFConverter.toWCFType(i));
        }

        public IEnumerable<DcSector> GetSector()
        {
            ISTOREEntities3 ent = new ISTOREEntities3();
            return ent.ViewSectors.ToList<ViewSector>().ConvertAll<DcSector>(i => WCFConverter.toWCFType(i));
        }

        public IEnumerable<DcAppDetail> GetAppDetail()
        {
            ISTOREEntities3 ent = new ISTOREEntities3();
            return ent.ViewAppDetails.ToList<ViewAppDetail>().ConvertAll<DcAppDetail>(i => WCFConverter.toWCFType(i));
        }

        public IEnumerable<DcTech> GetTech()
        {
            ISTOREEntities3 ent = new ISTOREEntities3();
            return ent.ViewTeches.ToList<ViewTech>().ConvertAll<DcTech>(i => WCFConverter.toWCFType(i));
        }

        public IEnumerable<DcAppDetails2> GetAppDetails2()
        {
            ISTOREEntities3 ent = new ISTOREEntities3();
            return ent.ViewAppDetails2.ToList<ViewAppDetails2>().ConvertAll<DcAppDetails2>(i => WCFConverter.toWCFType(i));
        }
    }
}
