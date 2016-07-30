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

namespace XamarinNotification.Dorid.Services
{
    using Gcm;
    using WindowsAzure.Messaging;

    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]

    public class Constans : GcmBroadcastReceiverBase<GcmService>
    {
        public static string[] SENDER_IDS = { "[專案編號]" };
        public const string HUB_NAME = "[Notification Hub名稱]";
        public const string HUB_LISTEN_CONN = "[包含Listen權限的Notification Hub連線字串]";
    }

    [Service]
    public class GcmService : GcmServiceBase
    {
        static NotificationHub hub;
        static string Tag;

        public static void Register(Context Context)
        {
            GcmClient.Register(Context, Constans.SENDER_IDS);
        }

        public static void Unregister(Context Context)
        {
            GcmClient.UnRegister(Context);
        }

        public GcmService() : base(Constans.SENDER_IDS)
        {
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            hub = new NotificationHub(Constans.HUB_NAME, Constans.HUB_LISTEN_CONN, context);

            if (hub != null)
            {

                try
                {
                    hub.UnregisterAll(registrationId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                var tags = new List<string>() { };

                try
                {
                    var hubRegistration = hub.Register(registrationId, tags.ToArray());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            if (hub != null)
                hub.Unregister();
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Console.WriteLine("Received Notification");

            if (intent != null || intent.Extras != null)
            {

                var keyset = intent.Extras.KeySet();

                foreach (var key in intent.Extras.KeySet())
                {
                    string strMessage = intent.Extras.GetString(key);
                    Console.WriteLine("Key: {0}, Value: {1}", key, strMessage);
                }
            }

            var msg = new StringBuilder();
            string messageText = intent.Extras.GetString("message");
            if (!string.IsNullOrEmpty(messageText))
            {
                createNotification("New hub message!", messageText);
            }
            else
            {
                createNotification("Unknown message details", msg.ToString());
            }
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            return true;
        }

        protected override void OnError(Context context, string errorId)
        {
        }

        void createNotification(string title, string desc)
        {
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            var uiIntent = new Intent(this, typeof(MainActivity));
            var notification = new Notification(Android.Resource.Drawable.SymActionEmail, title);

            notification.Flags = NotificationFlags.AutoCancel;
            notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, 0));

            notificationManager.Notify(1, notification);
            dialogNotify(title, desc);
        }

        protected void dialogNotify(String title, String message)
        {

            MainActivity.instance.RunOnUiThread(() => {
                AlertDialog.Builder dlg = new AlertDialog.Builder(MainActivity.instance);
                AlertDialog alert = dlg.Create();
                alert.SetTitle(title);
                alert.SetButton("Ok", delegate {
                    alert.Dismiss();
                });
                alert.SetMessage(message);
                alert.Show();
            });
        }
    }
}