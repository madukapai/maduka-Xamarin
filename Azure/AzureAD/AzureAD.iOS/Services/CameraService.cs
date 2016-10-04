using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using AzureAD.Services;
using AzureAD.Models;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureAD.iOS.Services.CameraService))]
namespace AzureAD.iOS.Services
{
    public class CameraService : ICamera
    {
        public Task<CameraResult> TakePictureAsync()
        {
            throw new NotImplementedException();
        }
    }
}