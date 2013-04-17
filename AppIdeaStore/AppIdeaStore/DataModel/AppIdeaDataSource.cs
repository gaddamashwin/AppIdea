using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppIdeaStore.Common;
using AppIdeaStore.ServiceRef;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System.Runtime.Serialization;
using System.Collections;
using System.Reflection;
using SQLite;

namespace AppIdeaStore.DataModel
{
        public class sqlite_master
        {
            public string name { get; set; }
        }

        public class Helper
        {
            public static BitmapImage ByteArrayToImage(byte[] pict)
            {
                if (pict != null)
                {
                    BitmapImage image1 = new BitmapImage();
                    InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
                    ms.WriteAsync(pict.AsBuffer()).GetResults();
                    ms.FlushAsync().AsTask().Wait();
                    ms.Seek(0);
                    image1.SetSource(ms);
                    return image1;
                }
                else return null;
            }
            public static T CopyProperties<T>(object src) where T : new()
            {
                var dst = new T();
                IEnumerable<PropertyInfo> srcProperties = src.GetType().GetRuntimeProperties();
                dynamic dstType = dst.GetType();

                if (srcProperties == null | dstType.GetProperties() == null)
                {
                    return dst;
                }

                foreach (PropertyInfo srcProperty in srcProperties)
                {
                    PropertyInfo dstProperty = dstType.GetProperty(srcProperty.Name);

                    if (dstProperty != null)
                    {
                        if (dstProperty.CanWrite == true)
                        {
                            dstProperty.SetValue(dst, srcProperty.GetValue(src, null), null);
                        }
                    }
                }
                return dst;
            }
            private static ServiceRef.Service1Client svc;
            public static ServiceRef.Service1Client appService
            {
                get
                {
                    return svc;
                }
            }
            //private static SQLiteAsyncConnection _db;
            //public static SQLiteAsyncConnection db
            //{
            //    get{
            //        var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\AppDataDemo.db";
            //        _db = new SQLiteAsyncConnection(path);
            //        return _db;
            //    }
            //}

            public static string SQLitePath = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\AppDataD.db";

            public static async Task InitObjects()
            {
                var _db = new SQLiteAsyncConnection(Helper.SQLitePath);
                var data = await _db.QueryAsync <sqlite_master>("SELECT name FROM sqlite_master WHERE type='table';");
                if (data.Count <= 4)
                {
                    var resut = await _db.CreateTablesAsync(new Type[] { typeof(DcSectorData), typeof(DcAppDetails2Data), typeof(DcAppData), typeof(DcAppDetailData) });
                }

                if (svc == null)
                {
                    System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                    binding.MaxReceivedMessageSize = int.MaxValue;
                    //svc = new ServiceRef.Service2Client(binding, new System.ServiceModel.EndpointAddress("http://USCMPUJMITTAL8.us.deloitte.com/WcfService/Service2.svc"));10.9.183.121
                    svc = new ServiceRef.Service1Client(binding, new System.ServiceModel.EndpointAddress("http://USCMPUJMITTAL8.us.deloitte.com/WcfService/Service1.svc"));
                }
            }

            public static async Task deleteTables()
            {
                var db = new SQLiteAsyncConnection(SQLitePath);
                var r1 = await db.DropTableAsync<DcSectorData>();
                var r2 = await db.DropTableAsync<DcAppDetails2Data>();
                var r3 = await db.DropTableAsync<DcAppData>();
                var r4 = await db.DropTableAsync<DcAppDetailData>();
            }

            public static async Task ClearData()
            {
                await deleteTables();
                DataCollection.clearCollection();
            }
        }

        public class DataCollection
        {
            public static Windows.Storage.ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            public static List<SectorAppData> _sectorGroups;
            private static IEnumerable<DcSectorData> _ListAppSectors;
            private static IEnumerable<DcAppDetails2Data> _ListApp2Details;
            private static IEnumerable<DcAppDetailData> _ListAppDetails;
            private static IEnumerable<DcAppData> _ListApps;

            public static async Task<IEnumerable<DcAppData>> ListApps()
            {
                if (_ListApps == null)
                {
                    var db = new SQLiteAsyncConnection(Helper.SQLitePath);
                    //var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\AppDataDemo.db";
                    //var _db1 = new SQLiteAsyncConnection(path);
                    _ListApps = await db.QueryAsync<DcAppData>("SELECT * FROM DcAppData");
                    if (!_ListApps.Any())
                    {
                        _ListApps = Helper.appService.GetAppIdeaAsync().Result.Select(i => Helper.CopyProperties<DcAppData>(i));
                        await db.InsertAllAsync(_ListApps);
                    }
                }
                return _ListApps;
            }
            public static async Task<IEnumerable<DcAppDetailData>> ListAppDetails()
            {
                if (_ListAppDetails == null)
                {
                    var db = new SQLiteAsyncConnection(Helper.SQLitePath);
                    _ListAppDetails = db.QueryAsync<DcAppDetailData>("SELECT * FROM DcAppDetailData").Result;
                    if (!_ListAppDetails.Any())
                    {
                        var appDetail = await Helper.appService.GetAppDetailAsync();
                        _ListAppDetails = appDetail.Select(i => Helper.CopyProperties<DcAppDetailData>(i));
                        await db.InsertAllAsync(_ListAppDetails);
                    }
                }
                return _ListAppDetails;
            }

            public static async Task<IEnumerable<DcAppDetails2Data>> ListApp2Details()
            {
                    if (_ListApp2Details == null)
                    {
                        var db = new SQLiteAsyncConnection(Helper.SQLitePath);
                        _ListApp2Details = db.QueryAsync<DcAppDetails2Data>("SELECT * FROM DcAppDetails2Data").Result;
                        if (!_ListApp2Details.Any())
                        {
                            var appDetail = await Helper.appService.GetAppDetails2Async();
                            _ListApp2Details = appDetail.Select(i => Helper.CopyProperties<DcAppDetails2Data>(i));
                            await db.InsertAllAsync(_ListApp2Details);
                        }
                    }
                    return _ListApp2Details;
            }
            
            public static async Task<IEnumerable<DcSectorData>> ListAppSectors()
            {
                if (_ListAppSectors == null)
                {
                    var db = new SQLiteAsyncConnection(Helper.SQLitePath);
                    _ListAppSectors = await db.QueryAsync<DcSectorData>("SELECT * FROM DcSectorData");
                    if (!_ListAppSectors.Any())
                    {
                        var r = await Helper.appService.GetAppSectorAsync();
                        var r2 = await DataCollection.ListApps();
                        _ListAppSectors = r.Select(i => Helper.CopyProperties<DcSectorData>(i));
                        await db.InsertAllAsync(_ListAppSectors);
                    }
                }
              
                return _ListAppSectors;
            }

            public static void clearCollection()
            {
                if (_sectorGroups != null) { _sectorGroups.Clear(); _sectorGroups = null; }
                if (_ListAppSectors != null) { _ListAppSectors.ToList().Clear(); _ListAppSectors = null; }
                if (_ListApp2Details != null) { _ListApp2Details.ToList().Clear(); _ListApp2Details = null; }
                if (_ListAppDetails != null) { _ListAppDetails.ToList().Clear(); _ListAppDetails = null; }
                if (_ListApps != null) { _ListApps.ToList().Clear(); _ListApps = null; }
            }
        }

        public class AppIdeaDataSource
        {
            public static async Task<IEnumerable<SectorAppData>> GetSectorGroups()
            {
                if (DataCollection._sectorGroups == null || DataCollection._sectorGroups.Count() == 0)
                {
                    var data = await DataCollection.ListAppSectors();
                    var data2 = await DataCollection.ListApps();

                    DataCollection._sectorGroups = new List<SectorAppData>();
                    foreach (var item in data.GroupBy(i => i.sectorId))
                    {
                        SectorAppData sector = Helper.CopyProperties<SectorAppData>(data.Where(i => i.sectorId == item.Key).FirstOrDefault());
                        
                        sector.AppItems = from sec in data
                                    join app in data2 on sec.appId equals app.appId
                                    where sec.sectorId == sector.sectorId
                                    select app;
                        sector.TopAppItems = sector.AppItems.Take(8);

                        sector.count = item.Count().ToString();
                        DataCollection._sectorGroups.Add(sector);
                    }
                }
                return DataCollection._sectorGroups;
            }

            public static SectorAppData GetGroup(int sectorID)
            {
                return DataCollection._sectorGroups.Where(i => i.sectorId == sectorID).FirstOrDefault();
            }

            public static async Task<AppData> GetAppData(int AppID)
            {
                var data = await DataCollection.ListApps();
                var result = data.Where(i => i.appId == AppID).Select(i => Helper.CopyProperties<AppData>(i)).FirstOrDefault();

                var data2 = await DataCollection.ListAppDetails();
                result.AppDetails = from appdetails in data2
                                where appdetails.appId.Equals(result.appId)
                                select appdetails;

                var data3 = await DataCollection.ListApp2Details();
                result.AppDetails2 = from appdetails in data3
                               where appdetails.appId.Equals(result.appId)
                               select appdetails;

                return result;
            }
        }
        
        
    }




