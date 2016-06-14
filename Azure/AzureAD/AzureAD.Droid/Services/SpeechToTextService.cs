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
using AzureAD.Droid.Services;
using AzureAD.Models;
using System.Threading.Tasks;
using Android.Content.PM;
using Android.Speech;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SpeechToTextService))]
namespace AzureAD.Droid.Services
{
    public class SpeechToTextService :ISpeechToText
    {

        private static TaskCompletionSource<SpeechToTextResult> objResult;

        public Task<SpeechToTextResult> SpeechToTextAsync()
        {
            string rec = PackageManager.FeatureMicrophone;

            if (rec == "android.hardware.microphone")
            {
                var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
                voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "�л��X���O");
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
                var activity = (Activity)Forms.Context;
                activity.StartActivityForResult(voiceIntent, (int)Enums.ActiveResultCode.SpeechToText);
            }

            objResult = new TaskCompletionSource<SpeechToTextResult>();
            return objResult.Task;
        }

        /// <summary>
        /// ���o���ѵ��G���ʧ@
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="strSpeechText"></param>
        public static void OnResult(Result resultCode, Intent data)
        {
            if (resultCode == Result.Canceled)
            {
                objResult.TrySetResult(null);
                return;
            }

            if (resultCode != Result.Ok)
            {
                objResult.TrySetException(new Exception("Unexpected error"));
                return;
            }

            var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
            string strSpeechText = "No Speech";

            if (matches.Count != 0)
                strSpeechText = matches[0];

            SpeechToTextResult res = new SpeechToTextResult();
            res.Text = strSpeechText;
            objResult.TrySetResult(res);
        }
    }
}