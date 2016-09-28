using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using AzureAD.Services;

namespace AzureAD.Views
{
    public partial class CameraView : ContentPage
    {
        public CameraView()
        {
            InitializeComponent();
        }

        ICamera iCamera = DependencyService.Get<ICamera>();

        /// <summary>
        /// 啟用照相機的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected async void btnCamera_Click(object sender, EventArgs e)
        {
            Models.CameraResult objResult = await iCamera.TakePictureAsync();
            lblResult.Text = objResult.FilePath;
        }
    }
}
