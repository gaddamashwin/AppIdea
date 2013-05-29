using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        /// <summary>
        /// 
        /// </summary>
        public static string SQLitePath = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\ApJudge1.db";
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
            if (data.Count <= 1)
            {
                
                await _db.CreateTablesAsync(new Type[] { typeof(Car), typeof(CarModel)});
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
            var db = new SQLiteAsyncConnection(Helper.SQLitePath);

            var r = await Helper.appService.GetCarsAsync();
            await db.InsertAllAsync(r.Select(i => Helper.CopyProperties<Car>(i)));

            var r2 = await Helper.appService.GetCarModelsAsync();
            await db.InsertAllAsync(r2.Select(i => Helper.CopyProperties<CarModel>(i)));
        }
    }

    public class AppIdeaDataSource
    {
        private static IEnumerable<Car> cars;
        private static IEnumerable<CarModel> carmodels;
        private static List<CarCarModels> carModelCars;
        /// <summary>
        /// All the cars
        /// </summary>
        /// <returns></returns>
        private static async Task<IEnumerable<Car>> Cars()
        {
            if (cars == null)
            {
                var db = new SQLiteAsyncConnection(Helper.SQLitePath);
                cars = await db.QueryAsync<Car>("select * from Car");
                if (!cars.Any())
                {
                    var tmpcars = await Helper.appService.GetCarsAsync();
                    cars = tmpcars.Select(i => Helper.CopyProperties<Car>(i));
                    await db.InsertAllAsync(cars);
                }
            }
            return cars;
        }
        /// <summary>
        /// All CarModels
        /// </summary>
        /// <returns></returns>
        private static async Task<IEnumerable<CarModel>> CarModels()
        {
            if (carmodels == null)
            {
                var db = new SQLiteAsyncConnection(Helper.SQLitePath);
                carmodels = await db.QueryAsync<CarModel>("SELECT * FROM CarModel");
                if (!carmodels.Any())
                {
                    var tmpcarmodels = await Helper.appService.GetCarModelsAsync();
                    carmodels = tmpcarmodels.Select(i => Helper.CopyProperties<CarModel>(i));
                    await db.InsertAllAsync(carmodels);
                }
            }
            return carmodels;
        }

        /// <summary>
        /// Data Source for Grouped Items
        /// </summary>
        /// <returns>List of all car models with top 8 cars</returns>
        public static async Task<IEnumerable<CarCarModels>> GetCarModelsandCars()
        {
            if (carModelCars == null || carModelCars.Count() == 0)
            {
                var data = await Cars();
                var data2 = await CarModels();

                carModelCars = new List<CarCarModels>();
                foreach (var item in data.GroupBy(i => i.CarID))
                {
                    CarCarModels sector = Helper.CopyProperties<CarCarModels>(data.Where(i => i.CarID == item.Key).FirstOrDefault());
                    sector.AppItems = (from sec in data
                                      join app in data2 on sec.CarID equals app.CarID
                                      where sec.CarID == sector.CarID
                                      select app);
                    sector.AppItems = sector.AppItems.Select(app => { app.CarNameWithModel =  sector.CarName + " " + app.CarModelName; return app; });
                    sector.count = item.Count().ToString();
                    carModelCars.Add(sector);
                }
            }
            return carModelCars;
        }

        /// <summary>
        /// Data Source for Group Detail
        /// </summary>
        /// <param name="sectorID">ID of the Group</param>
        /// <returns>All the App Data for the group</returns>
        public static CarCarModels GetCar(int carID)
        {
            return carModelCars.Where(i => i.CarID == carID).FirstOrDefault();
        }

        /// <summary>
        /// Data source for Item Details(ItemDetailPage.xaml)
        /// </summary>
        /// <param name="AppID">AppID of the selected item</param>
        /// <returns>AppData for the AppID is returned</returns>
        public static async Task<CarModelcar> GetCarModel(int carModelID)
        {
            var data = await CarModels();
            var result = data.Where(i => i.CarModelID == carModelID).Select(i => Helper.CopyProperties<CarModelcar>(i)).FirstOrDefault();

            var data2 = await Cars();
            result.car = (from appdetails in data2
                                where appdetails.CarID.Equals(result.CarID)
                                select appdetails).FirstOrDefault();
            return result;
        }
    }
}
