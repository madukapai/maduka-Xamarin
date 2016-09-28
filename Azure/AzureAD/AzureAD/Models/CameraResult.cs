using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AzureAD.Models
{
    public class CameraResult
    {
        public ImageSource Picture { get; set; }

        public string FilePath { get; set; }
    }
}
