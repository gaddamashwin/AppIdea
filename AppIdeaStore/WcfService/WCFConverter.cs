using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.DataContract;

namespace WcfService
{
    public class WCFConverter
    {
        private WCFConverter(){ }

        public static DcApp toWCFType(ViewApp objIn)
        {
            DcApp ObjOut = new DcApp();
            ObjOut.appId = objIn.appId;
            ObjOut.desc = objIn.desc;
            ObjOut.email = objIn.email;
            ObjOut.firstName = objIn.firstName;
            ObjOut.idea = objIn.idea;
            ObjOut.impact = objIn.impact;
            ObjOut.issue = objIn.issue;
            ObjOut.lastName = objIn.lastName;
            ObjOut.name = objIn.name;
            ObjOut.pict = objIn.pict;
            ObjOut.submittedDate = objIn.submittedDate;
            return ObjOut;
        }

        public static DcAppDetail toWCFType(ViewAppDetail objIn)
        {
            DcAppDetail ObjOut = new DcAppDetail();
            ObjOut.appId = objIn.appId;
            ObjOut.pict = objIn.pict;
            ObjOut.imageId = objIn.imageId;
            return ObjOut;
        }

        public static DcAppDetails2 toWCFType(ViewAppDetails2 objIn)
        {
            DcAppDetails2 ObjOut = new DcAppDetails2();
            ObjOut.appId = objIn.appId;
            ObjOut.comment = objIn.comment;
            ObjOut.commentId = objIn.commentId;
            ObjOut.date = objIn.date;
            ObjOut.email = objIn.email;
            ObjOut.firstName = objIn.firstName;
            ObjOut.lastName = objIn.lastName;
            ObjOut.rating = objIn.rating;
            ObjOut.status = objIn.status;
            return ObjOut;
        }

        public static DcSector toWCFType(ViewSector objIn)
        {
            DcSector ObjOut = new DcSector();
            ObjOut.appId = objIn.appId;
            ObjOut.name = objIn.name;
            ObjOut.sectorId = objIn.sectorId;
            return ObjOut;
        }

        public static DcTech toWCFType(ViewTech objIn)
        {
            DcTech ObjOut = new DcTech();
            ObjOut.appId = objIn.appId;
            ObjOut.name = objIn.name;
            ObjOut.techId = objIn.TechId;
            return ObjOut;
        }

        public static TechnologyType toWCFType(Technology tech)
        {
            TechnologyType tt = new TechnologyType();
            tt.Name = tech.name;
            tt.TechnologyId = tech.technologyId;
            return tt;
        }

        public static SectorType toWCFType(SectorOffering sector)
        {
            SectorType st = new SectorType();
            st.Name = sector.name;
            st.SectorId = sector.sectorId;
            st.appID = sector.Apps.Select(i => i.appId).ToList();
            return st;
        }

        public static ImageType toWCFType(Image img)
        {
            ImageType it = new ImageType();
            it.AppId = img.appId;
            it.CreatedDate = img.createdDate;
            it.ImageId = img.imageId;
            it.Pict = img.pict;
            it.Sequence = img.sequence;
            it.Type = img.type;
            return it;
        }

        public static CommentType toWCFType(Comment cmt)
        {
            CommentType ct = new CommentType();
            ct.appId = cmt.appId;
            ct.comment1 = cmt.comment1;
            ct.commentId = cmt.commentId;
            ct.date = cmt.date;
            ct.rating = cmt.rating;
            ct.status = cmt.status;
            ct.userId = cmt.userId;
            return ct;
        }

        public static UsrType toWCFType(User usr)
        {
            UsrType ut = new UsrType();
            ut.email = usr.email;
            ut.firstName = usr.firstName;
            ut.lastName = usr.lastName;
            ut.userId = usr.userId;
            return ut;
        }

        public static appType toWCFType(App app)
        {
            ISTOREEntities1 ent = new ISTOREEntities1();
            appType at = new appType();
            at.appId = app.appId;
            at.deleted = app.deleted;
            at.desc = app.desc;
            at.idea = app.idea;
            at.impact = app.impact;
            at.issue = app.issue;
            at.name = app.name;
            at.submittedBy = app.submittedBy;
            at.submittedDate = app.submittedDate;
            at.Pict = ent.Images.Where(i => i.appId == app.appId).FirstOrDefault<Image>().pict;
            //at.User = app.User;
            //at.Comments = app.Comments;
            //at.Images = app.Images;
            //at.SectorOfferings = app.SectorOfferings;
            //at.Technologies = app.Technologies;
            //at.UserTypes = app.UserTypes;
            return at;
        }

        public static ResultType toWCFType(GetAppDetails_Result result)
        {
            ResultType rt = new ResultType();
            rt.appId = result.appId;
            rt.commentId = Convert.ToInt32(result.commentId);
            rt.imageId = Convert.ToInt32(result.imageId);
            rt.sectorId = result.sectorId;
            rt.techId = result.techId;
            rt.userId = Convert.ToInt32(result.userId);
            return rt;
        }
    }
}