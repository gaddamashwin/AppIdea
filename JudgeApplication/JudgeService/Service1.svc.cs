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
        public IEnumerable<CarType> GetCars()
        {
            JudgeEntities ent = new JudgeEntities();
            return ent.Cars.ToList<Car>().ConvertAll<CarType>(i => CopyProperties<CarType>(i));
        }

        public IEnumerable<CarModelType> GetCarModels()
        {
            JudgeEntities ent = new JudgeEntities();
            return ent.CarModels.ToList<CarModel>().ConvertAll<CarModelType>(i => CopyProperties<CarModelType>(i));
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
    }
}
