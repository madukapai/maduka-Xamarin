using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using AzureAD.Services;
using System.Threading.Tasks;

[assembly: Dependency(typeof(AzureAD.iOS.Services.QRCodeService))]
namespace AzureAD.iOS.Services
{
    public class QRCodeService : IQRCode
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
