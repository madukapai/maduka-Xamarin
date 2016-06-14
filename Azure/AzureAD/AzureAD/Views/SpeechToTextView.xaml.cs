using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using AzureAD.Services;

namespace AzureAD.Views
{
    public partial class SpeechToTextView : ContentPage
    {
        ISpeechToText speech = DependencyService.Get<ISpeechToText>();

        public SpeechToTextView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 點選開始辨識的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected async void btnSpeech_Clicked(object sender, EventArgs e)
        {
            var SpeechResult = await speech.SpeechToTextAsync();
            if (SpeechResult != null)
                lblText.Text = SpeechResult.Text;
        }
    }
}
