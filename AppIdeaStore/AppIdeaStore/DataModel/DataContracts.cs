using AppIdeaStore.ServiceRef;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace AppIdeaStore.DataModel
{
    [Table("DcSectorData")]
        public class DcSectorData : DcSector
        {
            [PrimaryKey, AutoIncrement]
            public Int32 PkId { get; set; }
        }

        [Table("DcAppDetails2Data")]
        public class DcAppDetails2Data : DcAppDetails2
        {
            [PrimaryKey, AutoIncrement]
            public Int32 PkId { get; set; }
        }

        [Table("DcAppData")]
        public class DcAppData : DcApp
        {
            [PrimaryKey, AutoIncrement]
            public Int32 PkId { get; set; }

            public BitmapImage MyImage
            {
                get
                {
                    return Helper.ByteArrayToImage(pict);
                }
            }

        }

        [Table("DcAppDetailData")]
        public class DcAppDetailData : DcAppDetail
        {
            [PrimaryKey, AutoIncrement]
            public Int32 PkId { get; set; }

            public BitmapImage MyImage
            {
                get
                {
                    return Helper.ByteArrayToImage(pict);
                }
            }

        }

        public class AppData : DcAppData
        {
            private IEnumerable<DcAppDetail> _AppDetails;
            private IEnumerable<DcAppDetails2Data> _AppDetails2;

            public async Task<IEnumerable<DcAppDetail>> AppDetails()
            {
                if (_AppDetails == null)
                {
                    var data = await DataCollection.ListAppDetails();
                    _AppDetails = from appdetails in data
                                    where appdetails.appId.Equals(base.appId)
                                    select appdetails;
                }
                return _AppDetails; 
            }

            public async Task<IEnumerable<DcAppDetails2Data>> AppDetails2()
            {
                if (_AppDetails2 == null)
                {
                    var data = await DataCollection.ListApp2Details();
                    _AppDetails2 = from appdetails in data
                                   where appdetails.appId.Equals(base.appId)
                                   select appdetails;
                }
                return _AppDetails2;
            }
        }

        public class SectorAppData : DcSector
        {
            private IEnumerable<DcApp> _AppItems;

            public IEnumerable<DcApp> AppItems
            {
                get
                {
                    // Just call the method
                    return AppItemsAsync().Result;
                }
            }

            private async Task<IEnumerable<DcApp>> AppItemsAsync()
            {
                if (_AppItems == null)
                {
                    var data2 = DataCollection.ListApps().Result;
                    var data = await  DataCollection.ListAppSectors();
                    _AppItems = from sec in data
                                join app in data2 on sec.appId equals app.appId
                                where sec.sectorId == base.sectorId
                                select app;
                }
                return _AppItems;
             
            }

            public IEnumerable<DcApp> TopAppItems
            {
                get
                {
                    // Just call the method
                    //return TopAppItemsAsync().Result;
                    var data = this.AppItems;
                    return data.Take(8); 
                }
            }

            //public async Task<IEnumerable<DcApp>> TopAppItemsAsync()
            //{
            //    //var data = this.AppItems;
            //    //return data.Take(8); 
            //}

            public string count
            {
                get;
                set;
            }
        }
    }

