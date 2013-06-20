//using JudgeApp.Data;

using JudgeApp.DataModel;
using JudgeApp.ServiceRef;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace JudgeApp
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class ItemDetailPage : JudgeApp.Common.LayoutAwarePage
    {
        public ItemDetailPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override async void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var item = await JudgeDataSource.GetCarModel((int)navigationParameter);
            this.DefaultViewModel["Group"] = item;
            this.DefaultViewModel["Items"] = await JudgeDataSource.GetCarJudgement(item.CarModelID);
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            //var selectedItem = (SampleDataItem)this.flipView.SelectedItem;
            //pageState["SelectedItem"] = selectedItem.UniqueId;
        }

        private async void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            await JudgeDataSource.SaveCarJudgement(judgementItems.DataContext);
        }
        /// <summary>
        /// This is the click handler for the 'CaptureButton' button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CapturePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Using Windows.Media.Capture.CameraCaptureUI API to capture a photo
                CameraCaptureUI dialog = new CameraCaptureUI();
                Size aspectRatio = new Size(16, 9);
                dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;

                StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
                if (file != null)
                {
                    var r = await file.OpenReadAsync();
                    r.ReadAsync(
                    //BitmapImage bitmapImage = new BitmapImage();
                    //using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                    //{
                    //    bitmapImage.SetSource(fileStream);
                    //}
                    //CapturedPhoto.Source = bitmapImage;
                    //ResetButton.Visibility = Visibility.Visible;

                    // Store the file path in Application Data
                    //appSettings[photoKey] = file.Path;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
