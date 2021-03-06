﻿using System;
using Linphone;
using Windows.UI.Core;
using Windows.System.Threading;
using Windows.ApplicationModel.Core;



namespace AgenteApp.Clases
{
    public class SoftphoneEmbebed 
    {
        public Core LinphoneCore { get; set; }
        public CoreListener listener { get; set; }
        public SoftphoneEmbebed()
        {
            try
            {
                string rc_path = null;
                LinphoneWrapper.setNativeLogHandler();

                Core.SetLogLevelMask(0xFF);
                listener = Factory.Instance.CreateCoreListener();
                listener.OnGlobalStateChanged = OnGlobal;
                LinphoneCore = Factory.Instance.CreateCore(listener, rc_path, null);
                LinphoneCore.NetworkReachable = true;
                LinphoneCore.VideoCaptureEnabled = false;
                LinphoneCore.VideoActivationPolicy.AutomaticallyAccept = false;
            }
            catch { }


        }

        private void LinphoneCoreIterate(ThreadPoolTimer timer)
        {
            while (true)
            {
#if WINDOWS_UWP
                CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                () => {
                    LinphoneCore.Iterate();
                });
#else
                Device.BeginInvokeOnMainThread(() =>
                {
                    LinphoneCore.Iterate();
                });
                Thread.Sleep(50);
#endif
            }
        }

        private void OnGlobal(Core lc, GlobalState gstate, string message)
        {
#if WINDOWS_UWP
            Console.WriteLine("Global state changed: " + gstate);
#else
            Console.WriteLine("Global state changed: " + gstate);
#endif
        }

        public void OnStart()
        {
            // Handle when your app starts
#if WINDOWS_UWP
            TimeSpan period = TimeSpan.FromSeconds(1);
            ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer(LinphoneCoreIterate, period);
#else
            Thread iterate = new Thread(LinphoneCoreIterate);
            iterate.IsBackground = false;
            iterate.Start();
#endif
        }

        public void OnSleep()
        {
            // Handle when your app sleeps
        }

        public void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
