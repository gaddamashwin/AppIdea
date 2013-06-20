using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace JudgeApp.DataModel
{
    public class sqlite_master
    {
        public string name { get; set; }
    }

    public class Helper
    {
        private static string loginUserName;
        public static async Task<string> LoginUserName()
        { 
            if (string.IsNullOrEmpty(loginUserName))
            {
                loginUserName = await Windows.System.UserProfile.UserInformation.GetDisplayNameAsync();
            }
            return loginUserName;
        }
        //public static BitmapImage ByteArrayToImage(byte[] pict)
        //{
        //    if (pict != null)
        //    {
        //        BitmapImage image1 = new BitmapImage();
        //        InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
        //        ms.WriteAsync(pict.AsBuffer()).GetResults();
        //        ms.FlushAsync().AsTask().Wait();
        //        ms.Seek(0);
        //        image1.SetSource(ms);
        //        return image1;
        //    }
        //    else return null;
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static T CopyProperties<T>(object src) where T : new()
        {
            var dst = new T();
            if (src != null)
            {
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
            }
            return dst;
        }
        private static ServiceRef.Service1Client svc;
        public static int CarShowId;
        public static ServiceRef.Service1Client appService
        {
            get
            {
                return svc;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string SQLitePath = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\ApJudge8.db";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task InitObjects()
        {
            if (svc == null)
            {
                System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                binding.MaxReceivedMessageSize = int.MaxValue;
                //svc = new ServiceRef.Service2Client(binding, new System.ServiceModel.EndpointAddress("http://USCMPUJMITTAL8.us.deloitte.com/WcfService/Service2.svc"));10.9.183.121
                svc = new ServiceRef.Service1Client(binding, new System.ServiceModel.EndpointAddress("http://localhost:44815/Service1.svc"));
            }

            var _db = new SQLiteAsyncConnection(Helper.SQLitePath);
            //await DeleteTables();
            var data = await _db.QueryAsync<sqlite_master>("SELECT name FROM sqlite_master WHERE type='table';");
            if (data.Count <= 3)
            {
                await _db.CreateTablesAsync(new Type[] { typeof(Car), typeof(CarModel), typeof(CarShow), typeof(CarJudgement) });
                await SyncTables();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task DeleteTables()
        {
            var db = new SQLiteAsyncConnection(SQLitePath);
            var r1 = await db.DropTableAsync<Car>();
            var r2 = await db.DropTableAsync<CarModel>();
            var r3 = await db.DropTableAsync<CarShow>();
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task ClearData()
        {
            await DeleteTables();
            //DataCollection.clearCollection();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task SyncTables()
        {
            //var db = new SQLiteAsyncConnection(Helper.SQLitePath);

            //var r = await Helper.appService.GetCarsAsync(Helper.CarShowId);
            //await db.InsertAllAsync(r.Select(i => Helper.CopyProperties<Car>(i)));

            //var r2 = await Helper.appService.GetCarModelsAsync(Helper.CarShowId);
            //await db.InsertAllAsync(r2.Select(i => Helper.CopyProperties<CarModel>(i)));
        }
    }

}
