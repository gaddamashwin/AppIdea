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
        //[PrimaryKey]
        //public new int CarModelID { get { return base.CarModelID; } set { base.CarModelID = value; } }
        [PrimaryKey, AutoIncrement]
        public Int32 PkCarModelId { get; set; }

        public string CarNameWithModel { get; set; }

        public int CarShowID { get { return Helper.CarShowId; } set { } }
    }

    [Table("Car")]
    public class Car:ServiceRef.CarType
    {
         //[PrimaryKey]
         //public new int CarID { get { return base.CarID; } set { base.CarID = value; } }
        [PrimaryKey, AutoIncrement]
        public Int32 PkCarId { get; set; }

         public int CarShowID { get { return Helper.CarShowId; } set { } }
    }

    [Table("CarShow")]
    public class CarShow : ServiceRef.CarShowType
    {
        [PrimaryKey]
        public new int CarShowID { get { return base.CarShowID; } set { base.CarShowID = value; } }
    }

    [Table("CarJudgement")]
    public class CarJudgement : ServiceRef.CarJudgementType
    {
        [PrimaryKey, AutoIncrement]
        public int CarJudgementTypeID { get;set; }
    }

    public class CarCarModels : Car, INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private IEnumerable<CarModel> _AppItems;
        public IEnumerable<CarModel> AppItems
        {
            get
            {
                return _AppItems;
            }
            set
            {
                if (value != this._AppItems)
                {
                    this._AppItems = value;
                    NotifyPropertyChanged();
                }

            }
        }

        //private IEnumerable<CarModel> _TopAppItems;
        //public IEnumerable<CarModel> TopAppItems
        //{
        //    get
        //    {
        //        return _TopAppItems;
        //    }
        //    set
        //    {
        //        if (value != this._TopAppItems)
        //        {
        //            this._TopAppItems = value;
        //            NotifyPropertyChanged();
        //        }

        //    }
        //}

        public string count
        {
            get;
            set;
        }
    }

    public class CarModelcar : CarModel, INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Car _car;
        public Car car
        {
            get
            {
                return _car;
            }
            set
            {
                if (value != this._car)
                {
                    this._car = value;
                    NotifyPropertyChanged();
                }

            }
        }

        public CarJudgement carJudgement { get; set; }
    }

}
