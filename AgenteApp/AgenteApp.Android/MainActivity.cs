using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AgenteApp.Views;
using AgenteApp.Presenters;
using AgenteApp.Modelos;

namespace AgenteApp.Droid
{
	[Activity (Label = "AgenteApp.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, IInicioSesionVista
	{
        private InicioSesionPresentador presenter;
        public string NombreUsuario
        {
            get
            {
                return FindViewById<EditText>(Resource.Id.usuarioEditText).Text;
            }
            set
            {
                FindViewById<EditText>(Resource.Id.usuarioEditText).Text = value;
            }
        }
        public string Contrasena
        {
            get
            {
                return FindViewById<EditText>(Resource.Id.contrasenaEditText).Text;
            }
            set
            {
                FindViewById<EditText>(Resource.Id.contrasenaEditText).Text = value;
            }
        }

        public void MostrarMenu(Usuario usuario)
        {
            //Abrir Activity de menu
            
        }

        public void IniciarSesion()
        {
            presenter.IniciarSesion();
        }

        public void MostrarMensaje(string message)
        {
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }

        protected override void OnCreate (Bundle bundle)
		{
            presenter = new InicioSesionPresentador(this);

            base.OnCreate (bundle);

			
			SetContentView (Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.loginButton).Click += MainActivity_Click;

        }

        private void MainActivity_Click(object sender, EventArgs e)
        {
            IniciarSesion();
        }
    }
}


