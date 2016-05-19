using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AzureAD.Services;

[assembly: Dependency(typeof(AzureAD.UWP.Services.QRCodeService))]
namespace AzureAD.UWP.Services
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
