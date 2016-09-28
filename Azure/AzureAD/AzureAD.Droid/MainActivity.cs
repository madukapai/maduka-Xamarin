using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.Content;
using Android.Speech;

namespace AzureAD.Droid
{
    [Activity(Label = "AzureAD", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok)
            {
                // 如果回傳的requestCode是Enums.ActiveResultCode.SpeechToText, 就將結果回傳至SpeechToText的OnResult
                if (requestCode == (int)Enums.ActiveResultCode.SpeechToText)
                {
                    AzureAD.Droid.Services.SpeechToTextService.OnResult(resultCode, data);
                }
                else if (requestCode == (int)Enums.ActiveResultCode.Camera)
                {
                    // 照像命令
                    AzureAD.Droid.Services.CameraService.OnResult(resultCode);
                }
                else
                {
                    AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
                }
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}

