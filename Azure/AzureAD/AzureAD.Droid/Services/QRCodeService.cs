using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using AzureAD.Services;
using Xamarin.Forms;
using System.Threading.Tasks;

[assembly: Dependency(typeof(AzureAD.Droid.Services.QRCodeService))]
namespace AzureAD.Droid.Services
{
    class QRCodeService : IQRCode
    {
        public async Task<string> ScanAsync()
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>()
            {
                ZXing.BarcodeFormat.QR_CODE, ZXing.BarcodeFormat.CODE_39
            };

            var scanResults = await scanner.Scan(options);

            if (scanResults != null)
                return scanResults.Text;
            else
                return "";
        }
    }
}