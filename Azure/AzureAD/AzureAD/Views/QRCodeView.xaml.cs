using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using AzureAD.Services;

namespace AzureAD.Views
{
    public partial class QRCodeView : ContentPage
    {
        IQRCode iQrCode = DependencyService.Get<IQRCode>();

        public QRCodeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 開始掃瞄的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected async void btnScan_Click(object sender, EventArgs e)
        {
            lblResult.Text = await iQrCode.ScanAsync();
        }
    }
}
