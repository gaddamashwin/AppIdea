using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace JudgeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public IEnumerable<CarShowType> GetShows()
        {
            JudgeEntities ent = new JudgeEntities();
            return ent.CarShows.Where(show => show.IsCompleted == false).ToList<CarShow>().ConvertAll<CarShowType>(i => CopyProperties<CarShowType>(i));
        }

        public IEnumerable<CarType> GetCars(int carShowID)
        {
            JudgeEntities ent = new JudgeEntities();
            //return ent.Cars.ToList<Car>().ConvertAll<CarType>(i => CopyProperties<CarType>(i));
            return (from carShow in ent.CarShowCarModels
                         join carModel in ent.CarModels on carShow.CarModelID equals carModel.CarModelID
                         join car in ent.Cars on carModel.CarID equals car.CarID
                         where carShow.CarShowID == carShowID
                    select car).Distinct<Car>().ToList<Car>().ConvertAll<CarType>(i => CopyProperties<CarType>(i)); 
        }

        public IEnumerable<CarModelType> GetCarModels(int carShowID)
        {
            JudgeEntities ent = new JudgeEntities();
            //return ent.CarModels.ToList<CarModel>().ConvertAll<CarModelType>(i => CopyProperties<CarModelType>(i));
            return (from carShow in ent.CarShowCarModels
                    join carModel in ent.CarModels on carShow.CarModelID equals carModel.CarModelID
                    where carShow.CarShowID == carShowID
                    select carModel).Distinct<CarModel>().ToList<CarModel>().ConvertAll<CarModelType>(i => CopyProperties<CarModelType>(i)); 
        }

        public IEnumerable<CarJudgementType> GetJudgements(string userName)
        {
            JudgeEntities ent = new JudgeEntities();
            return ent.CarJudgements.Where(j => j.JudgedByJudgedDetails == userName).ToList<CarJudgement>().ConvertAll<CarJudgementType>(i => CopyProperties<CarJudgementType>(i));
        }

        private static T CopyProperties<T>(object src) where T : new()
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


        public void SaveJudgement(CarJudgementType carJudgement)
        {
            JudgeEntities ent = new JudgeEntities();
            var result= CopyProperties<CarJudgement>(carJudgement);
            var existingCar = ent.CarJudgements.Where(jud => jud.CarModelId == carJudgement.CarModelId && jud.CarShowId == carJudgement.CarShowId).FirstOrDefault();
            if (existingCar != null) ent.CarJudgements.Remove(existingCar);
            ent.CarJudgements.Add(result);
            ent.SaveChanges();
        }
    }
}
