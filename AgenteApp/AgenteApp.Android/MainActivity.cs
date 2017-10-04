using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AgenteApp.Views;
using AgenteApp.Presenters;

namespace AgenteApp.Droid
{
	[Activity (Label = "AgenteApp.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ILoginView
	{
        private LoginPresenter presenter;
        public string Username
        {
            get
            {
                return FindViewById<EditText>(Resource.Id.userEditText).Text;
            }
            set
            {
                FindViewById<EditText>(Resource.Id.userEditText).Text = value;
            }
        }
        public string Password
        {
            get
            {
                return FindViewById<EditText>(Resource.Id.passwordEditText).Text;
            }
            set
            {
                FindViewById<EditText>(Resource.Id.passwordEditText).Text = value;
            }
        }

        public void LoadMenu()
        {
            //Abrir Activity de menu
        }

        public void Login()
        {
            presenter.Login();
        }

        public void ShowMessage(string message)
        {
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }

        protected override void OnCreate (Bundle bundle)
		{
            presenter = new LoginPresenter(this);

            base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.loginButton).Click += MainActivity_Click;

        }

        private void MainActivity_Click(object sender, EventArgs e)
        {
            Login();
        }
    }
}


