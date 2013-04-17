using AppIdeaStore.ServiceRef;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
            //public async Task<IEnumerable<DcAppDetail>> AppDetails()
            //{
            //    if (_AppDetails == null)
            //    {
            //        var data = await DataCollection.ListAppDetails();
            //        _AppDetails = from appdetails in data
            //                        where appdetails.appId.Equals(base.appId)
            //                        select appdetails;
            //    }
            //    return _AppDetails; 
            //}

            //public async Task<IEnumerable<DcAppDetails2Data>> AppDetails2()
            //{
            //    if (_AppDetails2 == null)
            //    {
            //        var data = await DataCollection.ListApp2Details();
            //        _AppDetails2 = from appdetails in data
            //                       where appdetails.appId.Equals(base.appId)
            //                       select appdetails;
            //    }
            //    return _AppDetails2;
            //}

            public IEnumerable<DcAppDetail> AppDetails
            {
                get
                {
                    return _AppDetails;
                    //return AppItemsAsync();
                }
                set
                {
                    if (value != this._AppDetails)
                    {
                        this._AppDetails = value;
                        //NotifyPropertyChanged();
                    }

                }
            }

            public IEnumerable<DcAppDetails2Data> AppDetails2
            {
                get
                {
                    return _AppDetails2;
                    //return AppItemsAsync();
                }
                set
                {
                    if (value != this._AppDetails2)
                    {
                        this._AppDetails2 = value;
                        //NotifyPropertyChanged();
                    }

                }
            }

        }

        public class SectorAppData : DcSector, INotifyPropertyChanged
        {
            //public event PropertyChangedEventHandler PropertyChanged;
            event PropertyChangedEventHandler PropertyChanged;
            private IEnumerable<DcApp> _AppItems;

            // This method is called by the Set accessor of each property. 
            // The CallerMemberName attribute that is applied to the optional propertyName 
            // parameter causes the property name of the caller to be substituted as an argument. 
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public IEnumerable<DcApp> AppItems
            {
                get
                {
                    // Just call the method
                    return _AppItems;
                    //return AppItemsAsync();
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

            //private async Task<IEnumerable<DcApp>> AppItemsAsync()
            //{
            //    if (_AppItems == null)
            //    {
            //        var data2 = await DataCollection.ListApps();
            //        var data = await  DataCollection.ListAppSectors();
            //        _AppItems = from sec in data
            //                    join app in data2 on sec.appId equals app.appId
            //                    where sec.sectorId == base.sectorId
            //                    select app;
            //    }
            //    return _AppItems;
             
            //}

            private IEnumerable<DcApp> _TopAppItems;
            public IEnumerable<DcApp> TopAppItems
            {
                get
                {
                    //var data = this.AppItems;
                    //return data.Take(8); 
                    //var task = TopAppItemsAsync();
                    ////task.Start();
                    //while (!task.IsCompleted) { }
                    return _TopAppItems;
                }
                set
                {
                    if (value != this._TopAppItems)
                    {
                        this._TopAppItems = value;
                        NotifyPropertyChanged();
                    }
                   
                }
            }

            //public async Task<IEnumerable<DcApp>> TopAppItemsAsync()
            //public async Task TopAppItemsAsync()
            //{
            //    if (_AppItems == null)
            //    {
            //        var data2 = await DataCollection.ListApps();
            //        var data = await DataCollection.ListAppSectors();
            //        _AppItems = from sec in data
            //                    join app in data2 on sec.appId equals app.appId
            //                    where sec.sectorId == base.sectorId
            //                    select app;
            //    }
            //    //return _AppItems.Take(8);
            //}

            public string count
            {
                get;
                set;
            }
        }
    }

