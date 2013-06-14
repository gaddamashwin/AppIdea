using JudgeApp.ServiceRef;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeApp.DataModel
{
    public class JudgeDataSource
    {
        private static IEnumerable<Car> cars;
        private static IEnumerable<CarModel> carmodels;
        private static IEnumerable<CarShow> carShows;
        private static List<CarCarModels> carModelCars;
        private static int carDataforShowID;
        private static int carModelDataforShowID;
        /// <summary>
        /// All the cars
        /// </summary>
        /// <returns></returns>
        private static async Task<IEnumerable<Car>> Cars()
        {
            if (cars == null || carDataforShowID != Helper.CarShowId)
            {
                carDataforShowID = Helper.CarShowId;
                var db = new SQLiteAsyncConnection(Helper.SQLitePath);
                var cars2 = await db.QueryAsync<Car>("select * from Car");
                cars = await db.QueryAsync<Car>("select * from Car where CarShowID = " + Helper.CarShowId);
                if (!cars.Any())
                {
                    var tmpcars = await Helper.appService.GetCarsAsync(Helper.CarShowId);
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
            if (carmodels == null || carModelDataforShowID != Helper.CarShowId)
            {
                carModelDataforShowID = Helper.CarShowId;
                var db = new SQLiteAsyncConnection(Helper.SQLitePath);
                carmodels = await db.QueryAsync<CarModel>("SELECT * FROM CarModel where CarShowID = " + Helper.CarShowId);
                if (!carmodels.Any())
                {
                    var tmpcarmodels = await Helper.appService.GetCarModelsAsync(Helper.CarShowId);
                    carmodels = tmpcarmodels.Select(i => Helper.CopyProperties<CarModel>(i));
                    await db.InsertAllAsync(carmodels);
                }
            }
            return carmodels;
        }

        /// <summary>
        /// All CarShows
        /// </summary>
        /// <returns></returns>
        private static async Task<IEnumerable<CarShow>> CarShows()
        {
            if (carShows == null)
            {
                var db = new SQLiteAsyncConnection(Helper.SQLitePath);
                carShows = await db.QueryAsync<CarShow>("SELECT * FROM CarShow");
                if (!carShows.Any())
                {
                    var tmpcarmodels = await Helper.appService.GetShowsAsync();
                    carShows = tmpcarmodels.Select(i => Helper.CopyProperties<CarShow>(i));
                    await db.InsertAllAsync(carShows);
                }
            }
            return carShows;
        }

        /// <summary>
        /// Data Source for Grouped Items
        /// </summary>
        /// <returns>List of all car models with top 8 cars</returns>
        public static async Task<IEnumerable<CarCarModels>> GetCarModelsandCars()
        {
            if (carModelCars == null || carModelCars.Count() == 0 || 
                carModelDataforShowID != Helper.CarShowId ||
                carDataforShowID != Helper.CarShowId)
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
        public static async Task<IEnumerable<CarShow>> GetCarShows()
        {
            if (carShows == null) await CarShows();
            return carShows;
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


        public static async Task<CarJudgement> GetCarJudgement(int CarModelID)
        {
            var judge = await Helper.appService.GetJudgementsAsync(await Helper.LoginUserName());
            var judgement = Helper.CopyProperties<CarJudgement>(judge.Where(i => (i.CarModelId == CarModelID) && (i.CarShowId == Helper.CarShowId)).FirstOrDefault());
            judgement.CarModelId = CarModelID;
            judgement.CarShowId = Helper.CarShowId;
            return judgement;
        }

        public static async Task SaveCarJudgement(object judgmentReuslts)
        {
            CarJudgementType judgementResults = Helper.CopyProperties<CarJudgementType>(judgmentReuslts);
            judgementResults.JudgedDateJudgedDetails = DateTime.Now;
            judgementResults.JudgedByJudgedDetails = await Helper.LoginUserName();
            //carShows = await db.QueryAsync<CarShow>("SELECT * FROM CarJudgement");
            await Helper.appService.SaveJudgementAsync(judgementResults);
        }

    }
}
