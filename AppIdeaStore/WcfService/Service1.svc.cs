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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public IEnumerable<DcApp> GetAppIdea()
        {
            ISTOREEntities3 ent = new ISTOREEntities3();
            return ent.ViewApps.ToList<ViewApp>().ConvertAll<DcApp>(i => WCFConverter.toWCFType(i));
        }

        public IEnumerable<DcSector> GetAppSector()
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

        public IEnumerable<TechnologyType> GetTechnology()
        {
            ISTOREEntities1 ent = new ISTOREEntities1();
            return ent.Technologies.ToList<Technology>().ConvertAll<TechnologyType>(i => WCFConverter.toWCFType(i));
        }


        public IEnumerable<CommentType> GetComment()
        {
            ISTOREEntities1 ent = new ISTOREEntities1();
            return ent.Comments.ToList<Comment>().ConvertAll<CommentType>(i => WCFConverter.toWCFType(i));
        }

        public IEnumerable<ImageType> GetImage()
        {
            ISTOREEntities1 ent = new ISTOREEntities1();
            return ent.Images.ToList<Image>().ConvertAll<ImageType>(i => WCFConverter.toWCFType(i));
        }

        public IEnumerable<SectorType> GetSector()
        {
            ISTOREEntities1 ent = new ISTOREEntities1();
            return ent.SectorOfferings.ToList<SectorOffering>().ConvertAll<SectorType>(i => WCFConverter.toWCFType(i));
        }

        public IEnumerable<UsrType> GetUser()
        {
            ISTOREEntities1 ent = new ISTOREEntities1();
            return ent.Users.ToList<User>().ConvertAll<UsrType>(i => WCFConverter.toWCFType(i));
        }

        public IEnumerable<appType> GetApp()
        {
            ISTOREEntities1 ent = new ISTOREEntities1();
            return ent.Apps.Where(i => i.deleted.Value.Equals(false)).ToList<App>().ConvertAll<appType>(i => WCFConverter.toWCFType(i));
        }


        public IEnumerable<appType> GetAppDetails(int id, string type)
        {
            ISTOREEntities1 ent = new ISTOREEntities1();

            if (type.Contains("tech"))
            {
                return (from app in ent.Apps.ToList()
                        join dtl in ent.GetAppDetails().ToList()
                        on app.appId equals dtl.appId
                        where dtl.techId == id && app.deleted == false
                        select app)
                        .ToList()
                        .ConvertAll<appType>(i => WCFConverter.toWCFType(i));
                //return ent.Apps
                //    .Where(i => i.deleted.Value.Equals(false)
                //        && lst
                //            .Where(i1 => i1.techId.Equals(id))
                //            .Select(i2 => i2.appId)
                //            .Contains(i.appId))
                //    .ToList<App>()
                //    .ConvertAll<appType>(i => WCFConverter.toWCFType(i));
            }
            else
            {
                var rt1 = (from app in ent.Apps.ToList() where app.deleted == false select new { appId = app.appId, count = app.SectorOfferings.Where(i => i.sectorId == id).Count() }).ToList();

                var rt = (from app in ent.Apps.ToList()
                          join dtl in rt1 on app.appId equals dtl.appId
                          where dtl.count > 0
                          select app)
                             .ToList()
                             .ConvertAll<appType>(i => WCFConverter.toWCFType(i));

                //var rt = (from app in ent.Apps.ToList()
                //         join dtl in rt1
                //          on app.appId equals dtl
                //         select app)
                //        .ToList()
                //        .ConvertAll<appType>(i => WCFConverter.toWCFType(i));

                return rt;
            }
        }

       
    }
}
