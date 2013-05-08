using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace JudgeApp.DataModel
{
    [Table("CarModel")]
    public class CarModel : ServiceRef.CarModelType
    {
        [PrimaryKey]
        public new int CarModelID { get { return base.CarModelID; } set { base.CarModelID = value; } }
    }

     [Table("Car")]
    public class Car:ServiceRef.CarType
    {
         [PrimaryKey]
         public new int CarID { get { return base.CarID; } set { base.CarID = value; } }
    }
}
