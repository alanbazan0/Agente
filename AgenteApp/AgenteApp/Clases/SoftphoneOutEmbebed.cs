using System;
using Linphone;
using Windows.System.Threading;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace AgenteApp.Clases
{
    class SoftphoneOutEmbebed
    {
        public Core LinphoneOutCore { get; set; }
        public SoftphoneOutEmbebed()
        {
            try
            {
                string rc_path = null;
                LinphoneWrapper.setNativeLogHandler();

                Core.SetLogLevelMask(0xFF);
                CoreListener listener = Factory.Instance.CreateCoreListener();
                listener.OnGlobalStateChanged = OnGlobalOut;
                LinphoneOutCore = Factory.Instance.CreateCore(listener, rc_path, null);
                LinphoneOutCore.NetworkReachable = true;
                LinphoneOutCore.VideoCaptureEnabled = false;
                LinphoneOutCore.VideoActivationPolicy.AutomaticallyAccept = false;
            }
            catch { }
        }

        private void LinphoneCoreIterateOut(ThreadPoolTimer timer)
        {
            while (true)
            {
#if WINDOWS_UWP
                CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                () => {
                    LinphoneOutCore.Iterate();
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

        private void OnGlobalOut(Core lc, GlobalState gstate, string message)
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
            ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer(LinphoneCoreIterateOut, period);
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
