using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using AzureAD.Views;

namespace AzureAD
{
    public class App : Application
    {


        public App()
        {
            // 在下方選擇要嘗試的功能，並取消註解

            // 啟用AzureAD的登入畫面
            // MainPage = new AzureADView();

            // 啟用QRCode的畫面
            // MainPage = new QRCodeView();

            // 啟用語音辨識的畫面
            // MainPage = new SpeechToTextView();

            // 啟用照相的畫面
            MainPage = new CameraView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}